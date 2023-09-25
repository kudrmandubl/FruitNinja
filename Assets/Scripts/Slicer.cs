using UnityEngine;

public class Slicer : MonoBehaviour
{
    public Score Score;
    public Health Health;
    public GameEnder GameEnder;
    public SlicerComboChecker SlicerComboChecker;
    public SlowMotion SlowMotion;

    public AudioClip ScoreSound;
    public float ScoreSoundVolume = 0.45f;
    public AudioClip BombSound;
    public float BombSoundVolume = 0.3f;
    public AudioClip BonusSound;
    public float BonusSoundVolume = 0.7f;

    public Animation SplashAnimation;

    public float SliceForce = 65;

    private const float MinSlicingSpeed = 0.01f;

    private Collider _slicerTrigger;
    private Camera _mainCamera;
    private AudioSource _soundPlayer;

    private Vector3 _direction;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _slicerTrigger = GetComponent<Collider>();
        _mainCamera = Camera.main;
        _soundPlayer = GetComponentInChildren<AudioSource>();

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
        if (fruit == null) // тут можно было написать if(!fruit)
        {
            return;
        }

        fruit.Slice(_direction, transform.position, SliceForce);

        SlicerComboChecker.IncreaseComboStep();
        int scoreByFruit = 1 * SlicerComboChecker.GetComboMultiplier();
        Score.AddScore(scoreByFruit);
        _soundPlayer.PlayOneShot(ScoreSound, ScoreSoundVolume);
    }

    private void CheckBomb(Collider other) 
    {
        Bomb bomb = other.GetComponent<Bomb>();
        if (bomb == null) // тут можно было написать if(!bomb)
        {
            return;
        }

        Destroy(bomb.gameObject);

        SlicerComboChecker.StopCombo();
        Health.RemoveHealth();
        CheckHealthEnd(Health.GetCurrentHealth());
        _soundPlayer.PlayOneShot(BombSound, BombSoundVolume);
        SplashAnimation.Play(PlayMode.StopAll);
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
        _soundPlayer.PlayOneShot(BonusSound, BonusSoundVolume);
    }

    private void CheckHeart(Collider other)
    {
        Heart heart = other.GetComponentInParent<Heart>();
        if (heart == null)
        {
            return;
        }

        heart.ShowSliceParticles();

        int healthForHeart = heart.HealthForHeart;
        Destroy(heart.gameObject);

        SlicerComboChecker.IncreaseComboStep();
        Health.AddHeath(healthForHeart);
        _soundPlayer.PlayOneShot(BonusSound, BonusSoundVolume);
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
