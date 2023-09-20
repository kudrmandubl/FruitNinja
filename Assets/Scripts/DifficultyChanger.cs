using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    public int DifficultyUpScoreStep = 30;
    public int MaxDifficult = 20;

    private int _difficult = 1;
    private int _lastDifficultyUpScore = 0;

    public float CalculateRandomSpawnDelay(float minDelay, float maxDelay)
    {
        // Получаем случайную задержку
        float randomDelay = Random.Range(minDelay, maxDelay);
        // Коэффициент сложности. От 0 до 1. Становится меньше, с увеличением сложности.
        float difficultyCoef = (float)(MaxDifficult - _difficult) / MaxDifficult;
        // Получаем разницу между случайным и минимальным значением Задержки
        float delayDelta = randomDelay - minDelay;
        // Разницу умножаем на коэффициент и получаем уменьшение значения Задержки от Максимального к Минимальному с увеличением сложности
        // В итоге диапазон значения задержки уменьшается с увеличением сложности
        return minDelay + delayDelta * difficultyCoef;
    }

    public float CalculateBombWeight(float minWeight, float maxWeight)
    {
        // Коэффициент сложности. От 0 до 1. Становится больше, с увеличением сложности.
        float difficultyCoef = (float) _difficult / MaxDifficult;
        // Получаем разницу между максимальным и минимальным значением Веса
        float weightDelta = maxWeight - minWeight;
        // Разницу умножаем на коэффициент и получаем увеличение значения Веса от Минимального к Максимальном с увеличением сложности
        return minWeight + weightDelta * difficultyCoef;
    }

    public void Restart()
    {
        _difficult = 1;
        _lastDifficultyUpScore = 0;
    }


    public void SetDifficultByScore(int score)
    {
        if(score > _lastDifficultyUpScore + DifficultyUpScoreStep && _difficult < MaxDifficult)
        {
            _lastDifficultyUpScore += DifficultyUpScoreStep;
            _difficult++;
        }
    }
}
