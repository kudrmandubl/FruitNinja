using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private int _score;

    public void AddScore(int value)
    {
        SetScore(_score + value);
    }

    private void Start()
    {
        FillComponents();
        SetScore(0);
    }
    private void FillComponents()
    {
        _scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void SetScore(int value)
    {
        _score = value;
        SetScoreText(value);
    }

    private void SetScoreText(int value)
    {
        _scoreText.text = value.ToString();
    }
}
