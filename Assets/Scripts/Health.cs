using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int StartHealth = 3;

    private TextMeshProUGUI _healthText;
    private int _currentHealth;

    public void RemoveHealth()
    {
        SetHealth(_currentHealth - 1);
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void Restart()
    {
        SetHealth(StartHealth);
    }

    private void Start()
    {
        FillComponents();
        SetHealth(StartHealth);
    }

    private void FillComponents()
    {
        _healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void SetHealth(int value)
    {
        _currentHealth = value;
        SetHealthText(value);
    }

    private void SetHealthText(int value)
    {
        _healthText.text = value.ToString();
    }

}
