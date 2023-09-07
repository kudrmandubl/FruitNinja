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

    public void Restart()
    {
        SetScore(0);
    }

    public int GetScore()
    {
        return _score;
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
        _scoreText.text = "Очки: " + value;
    }
}
