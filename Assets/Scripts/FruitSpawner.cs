using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject FruitPrefab0;
    public GameObject FruitPrefab1;
    public GameObject FruitPrefab2;

    public GameObject BombPrefab;

    public float MinDelay = 0.2f;
    public float MaxDelay = 0.9f;
    public float AngleRangeZ = 20;
    public float LifeTime = 7f;
    public float MinForce = 15f;
    public float MaxForce = 25f;

    public float BombChance = 0.1f;

    private float _currentDelay = 0;
    private bool _isActive = true;
    private Collider _spawnZone;

    public void Stop()
    {
        _isActive = false;
    }

    public void Restart()
    {
        _isActive = true;
        SetNewDelay();
    }

    private void Start()
    {
        FillComponents();
        SetNewDelay();
    }

    private void FillComponents()
    {
        _spawnZone = GetComponent<Collider>();
    }

    private void SetNewDelay()
    {
        _currentDelay = Random.Range(MinDelay, MaxDelay);
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }
        MoveDelay();
    }

    private void MoveDelay()
    {
        _currentDelay -= Time.deltaTime;
        if(_currentDelay < 0)
        {
            float random = Random.value;
            if (random < BombChance)
            {
                SpawnBomb();
            }
            else
            {
                SpawnFruit();
            }
            SetNewDelay();
        }
    }

    private void SpawnFruit()
    {
        SpawnObject(GetRandomFruitPrefab());
    }

    private void SpawnBomb()
    {
        SpawnObject(BombPrefab);
    }

    private void SpawnObject(GameObject prefab)
    {
        Vector3 startPosition = GetRandomSpawnPosition();
        Quaternion startRotation = Quaternion.Euler(0f, 0f, Random.Range(-AngleRangeZ, AngleRangeZ));
        GameObject newObject = Instantiate(prefab, startPosition, startRotation);
        Destroy(newObject, LifeTime);
        AddForce(newObject);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 pos;
        pos.x = Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x);
        pos.y = Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y);
        pos.z = Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z);
        return pos;
    }

    private GameObject GetRandomFruitPrefab()
    {
        int r = Random.Range(0, 3);
        if(r == 0)
        {
            return FruitPrefab0;
        }
        else if (r == 1)
        {
            return FruitPrefab1;
        }
        else 
        {
            return FruitPrefab2;
        }
    }

    private void AddForce(GameObject obj)
    {
        float force = Random.Range(MinForce, MaxForce);
        obj.GetComponent<Rigidbody>().AddForce(obj.transform.up * force, ForceMode.Impulse);

    }
}
