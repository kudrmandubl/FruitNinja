using UnityEngine;
using TMPro;


public class GameEnder : MonoBehaviour
{
    public Score Score;
    public Health Health;
    public FruitSpawner FruitSpawner;
    public Randomizer Randomizer;

    public GameObject GameScreen;
    public GameObject GameEndScreen;

    public TextMeshProUGUI GameEndScoreText;
    public TextMeshProUGUI BestScoreText;

    public void EndGame()
    {
        FruitSpawner.Stop();
        RefreshScores();
        SwitchScreens(false); 
    }

    public void RestartGame()
    {
        Randomizer.Restart();
        Score.Restart();
        Health.Restart();
        FruitSpawner.Restart();
        SwitchScreens(true);
    }

    private void Start()
    {
        SwitchScreens(true);
    }

    private void SwitchScreens(bool isGame)
    {
        GameScreen.SetActive(isGame);
        GameEndScreen.SetActive(!isGame);
    }

    private void RefreshScores()
    {
        int score = Score.GetScore();
        int oldBestScore = Score.GetBestScore();
        bool isNewBestScore = CheckNewBestScore(score, oldBestScore);
        SetActiveGameEndScoreText(!isNewBestScore);
        if (isNewBestScore)
        {
            Score.SetBestScore(score);
            SetNewBestScoreText(score);
        }
        else
        {
            SetGameEndScoreText(score);
            SetOldBestScoreText(oldBestScore);
        }
    }

    private bool CheckNewBestScore(int score, int oldBestScore)
    {
        return score > oldBestScore;
    }

    private void SetGameEndScoreText(int value)
    {
        GameEndScoreText.text = $"Ты набрал {value}!";
    }

    private void SetOldBestScoreText(int value)
    {
        BestScoreText.text = $"Лучший результат {value}";
    }

    private void SetNewBestScoreText(int value)
    {
        BestScoreText.text = $"Новый рекорд: {value}!";
    }

    private void SetActiveGameEndScoreText(bool value)
    {
        GameEndScoreText.gameObject.SetActive(value);
    }
}
