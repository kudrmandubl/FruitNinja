using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    [Range(MinDifficult, MaxDifficult)] public int Difficult = 1;

    private const int MinDifficult = 1;
    private const int MaxDifficult = 3;

    public float CalculateRandomSpawnDelay(float minDelay, float maxDelay)
    {
        float randomDelay = Random.Range(minDelay, maxDelay);
        return minDelay + (randomDelay - minDelay) * ((float)(MaxDifficult - Difficult) / (MaxDifficult - MinDifficult));
    }

    public float CalculateBombChance(float minChance, float maxChance)
    { 
        return minChance + (maxChance - minChance) *  ((float) (Difficult - MinDifficult ) / (MaxDifficult - MinDifficult));
    }

}
