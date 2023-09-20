using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    public int DifficultyUpScoreStep = 30;
    public int MaxDifficult = 20;

    private int _difficult = 1;
    private int _lastDifficultyUpScore = 0;

    public float CalculateRandomSpawnDelay(float minDelay, float maxDelay)
    {
        // �������� ��������� ��������
        float randomDelay = Random.Range(minDelay, maxDelay);
        // ����������� ���������. �� 0 �� 1. ���������� ������, � ����������� ���������.
        float difficultyCoef = (float)(MaxDifficult - _difficult) / MaxDifficult;
        // �������� ������� ����� ��������� � ����������� ��������� ��������
        float delayDelta = randomDelay - minDelay;
        // ������� �������� �� ����������� � �������� ���������� �������� �������� �� ������������� � ������������ � ����������� ���������
        // � ����� �������� �������� �������� ����������� � ����������� ���������
        return minDelay + delayDelta * difficultyCoef;
    }

    public float CalculateBombWeight(float minWeight, float maxWeight)
    {
        // ����������� ���������. �� 0 �� 1. ���������� ������, � ����������� ���������.
        float difficultyCoef = (float) _difficult / MaxDifficult;
        // �������� ������� ����� ������������ � ����������� ��������� ����
        float weightDelta = maxWeight - minWeight;
        // ������� �������� �� ����������� � �������� ���������� �������� ���� �� ������������ � ������������ � ����������� ���������
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
