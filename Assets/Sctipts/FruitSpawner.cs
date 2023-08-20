using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject FruitPrefab;

    public float MinDelay = 0.2f;
    public float MaxDelay = 0.9f;
    public float AngleRangeZ = 20;
    public float LifeTime = 7f;
    public float MinForce = 15f;
    public float MaxForce = 25f;

    private float _currentDelay = 0;

    private void Start()
    {
        SetNewDelay();
    }

    private void SetNewDelay()
    {
        _currentDelay = Random.Range(MinDelay, MaxDelay);
    }

    private void Update()
    {
        MoveDelay();
    }

    private void MoveDelay()
    {
        _currentDelay -= Time.deltaTime;
        if(_currentDelay < 0)
        {
            SpawnFruit();
            SetNewDelay();
        }
    }

    private void SpawnFruit()
    {
        Quaternion startRotation = Quaternion.Euler(0f, 0f, Random.Range(-AngleRangeZ, AngleRangeZ));
        GameObject newFruit = Instantiate(FruitPrefab, transform.position, startRotation);
        Destroy(newFruit, LifeTime);
        AddForce(newFruit);
    }

    private void AddForce(GameObject fruit)
    {
        float force = Random.Range(MinForce, MaxForce);
        fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

    }
}
