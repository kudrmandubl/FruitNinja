using UnityEngine;

public class Slicer : MonoBehaviour
{
    public Score Score;
    public Health Health;
    public GameEnder GameEnder;
    public SlicerComboChecker SlicerComboChecker;
    public SlowMotion SlowMotion;

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
        CheckSandClocks(other);
        CheckHeart(other);
    }

    private void CheckFriut(Collider other)
    {
        Fruit fruit = other.GetComponent<Fruit>();
        if (fruit == null) // ��� ����� ���� �������� if(!fruit)
        {
            return;
        }

        fruit.Slice(_direction, transform.position, SliceForce);

        SlicerComboChecker.IncreaseComboStep();
        int scoreByFruit = 1 * SlicerComboChecker.GetComboMultiplier();
        Score.AddScore(scoreByFruit);
    }

    private void CheckBomb(Collider other) 
    {
        Bomb bomb = other.GetComponent<Bomb>();
        if (bomb == null) // ��� ����� ���� �������� if(!bomb)
        {
            return;
        }

        Destroy(bomb.gameObject);

        SlicerComboChecker.StopCombo();
        Health.RemoveHealth();
        CheckHealthEnd(Health.GetCurrentHealth());
    }

    private void CheckSandClocks(Collider other)
    {
        SandClocks sandClocks = other.GetComponent<SandClocks>();
        if (sandClocks == null)
        {
            return;
        }

        float slowDuration = sandClocks.SlowDuration; 
        Destroy(sandClocks.gameObject);

        SlicerComboChecker.IncreaseComboStep();
        SlowMotion.StartSlow(slowDuration);
    }

    private void CheckHeart(Collider other)
    {
        Heart heart = other.GetComponentInParent<Heart>();
        if (heart == null)
        {
            return;
        }

        int healthForHeart = heart.HealthForHeart;
        Destroy(heart.gameObject);

        SlicerComboChecker.IncreaseComboStep();
        Health.AddHeath(healthForHeart);
    }


    private void CheckHealthEnd(int health)
    {
        if(health > 0)
        {
            return;
        }
        StopGame();
    }

    private void StopGame()
    {
        GameEnder.EndGame();
    }
}
