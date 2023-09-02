using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text _scoreText;
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
        _scoreText = GetComponentInChildren<Text>();
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
