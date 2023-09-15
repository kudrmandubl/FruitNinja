using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    public int DifficultyUpScoreStep = 30;
    public int MaxDifficult = 20;

    private int _difficult = 1;
    private int _lastDifficultyUpScore = 0;

    public float CalculateRandomSpawnDelay(float minDelay, float maxDelay)
    {
        float randomDelay = Random.Range(minDelay, maxDelay);
        return minDelay + (randomDelay - minDelay) * ((float)(MaxDifficult - _difficult) / MaxDifficult);
    }

    public float CalculateBombChance(float minChance, float maxChance)
    { 
        return minChance + (maxChance - minChance) *  ((float) _difficult / MaxDifficult);
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
