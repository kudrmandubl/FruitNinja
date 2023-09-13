using UnityEngine;
using Random = System.Random;

public class Randomizer : MonoBehaviour
{
    public int Seed = 1;

    private Random _random;

    public int GetRandomInt(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }

    public float GetRandomFloat(float minValue, float maxValue)
    {
        return minValue + (float) _random.NextDouble() * (maxValue - minValue);
    }

    public float GetRandomFloat()
    {
        return (float) _random.NextDouble();
    }

    public void Restart()
    {
        CreateRandom();
    }
    private void Awake()
    {
        CreateRandom();
    }

    private void CreateRandom()
    {
        _random = new Random(Seed);
    }

}
