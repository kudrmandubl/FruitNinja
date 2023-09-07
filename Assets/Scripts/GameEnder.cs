using UnityEngine;
using TMPro;


public class GameEnder : MonoBehaviour
{
    public Score Score;
    public Health Health;
    public FruitSpawner FruitSpawner;

    public GameObject GameScreen;
    public GameObject GameEndScreen;

    public TextMeshProUGUI GameEndScoreText;

    public void EndGame()
    {
        FruitSpawner.Stop();
        SetGameEndScoreText(Score.GetScore());
        SwitchScreens(false);
    }

    public void RestartGame()
    {
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

    private void SetGameEndScoreText(int value)
    {
        GameEndScoreText.text = $"Ты набрал {value} Очков!";
    }
}
