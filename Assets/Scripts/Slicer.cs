using UnityEngine;

public class Slicer : MonoBehaviour
{
    public Score Score;
    public Health Health;

    public float SliceForce = 65;

    private const float MinSlicingSpeed = 0.01f;

    private Collider _slicerTrigger;
    private Camera _mainCamera;

    private Vector3 _direction;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _slicerTrigger = GetComponent<Collider>();
        _mainCamera = Camera.main;

        SetSlicing(false);
    }

    private void Update()
    {
        Slicing();
    }

    private void Slicing()
    {
        if (Input.GetMouseButton(0))
        {
            RefreshSlicing();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetSlicing(false);
        }
    }

    private void SetSlicing(bool value)
    {
        _slicerTrigger.enabled = value;
    }

    private void RefreshSlicing()
    {
        Vector3 targetPosition = GetTargetPosition();

        RefreshDirection(targetPosition);
        MoveSlicer(targetPosition);

        bool isSlicing = CheckMoreThanMinMove(_direction);
        SetSlicing(isSlicing);
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        return targetPosition;
    }

    private void RefreshDirection(Vector3 targetPosition)
    {
        _direction = targetPosition - transform.position;
    }

    private void MoveSlicer(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    private bool CheckMoreThanMinMove(Vector3 direction)
    {
        float slicingSpeed = direction.magnitude / Time.deltaTime;
        return slicingSpeed >= MinSlicingSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckFriut(other);
        CheckBomb(other);
    }

    private void CheckFriut(Collider other)
    {
        Fruit fruit = other.GetComponent<Fruit>();
        if (fruit == null) // тут можно было написать if(!fruit)
        {
            return;
        }

        fruit.Slice(_direction, transform.position, SliceForce);
        Score.AddScore(1);
    }

    private void CheckBomb(Collider other)
    {
        Bomb bomb = other.GetComponent<Bomb>();
        if (bomb == null) // тут можно было написать if(!bomb)
        {
            return;
        }

        Destroy(bomb.gameObject);
        Health.RemoveHealth();
    }

}
