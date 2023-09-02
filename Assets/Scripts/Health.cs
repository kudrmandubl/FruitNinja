using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int StartHealth = 3;

    private Text _healthText;
    private int _currentHealth;

    public void RemoveHealth()
    {
        SetHealth(_currentHealth - 1);
    }

    private void Start()
    {
        FillComponents();
        SetHealth(StartHealth);
    }

    private void FillComponents()
    {
        _healthText = GetComponentInChildren<Text>();
    }

    private void SetHealth(int value)
    {
        _currentHealth = value;
        SetHealthText(value);
        CheckHealthEnd(value);
    }

    private void SetHealthText(int value)
    {
        _healthText.text = value.ToString();
    }

    private void CheckHealthEnd(int health)
    {
        if (health <= 0)
        {
            HealthEnd();
        }
    }

    private void HealthEnd()
    {

    }
}
