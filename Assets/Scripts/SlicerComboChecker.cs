using UnityEngine;
using TMPro;

public class SlicerComboChecker : MonoBehaviour
{
    public GameObject ComboMultiplierRootGO;

    public float ComboIncreaseInterval = 1.1f;
    public int ComboMultiplierIncreseStep = 3;

    private TextMeshProUGUI _comboMultiplierText;

    private float _comboTimer;
    private int _comboStep;
    private int _comboMultiplier;

    public void IncreaseComboStep()
    {
        SetComboStep(_comboStep + 1);
    }

    public int GetComboMultiplier()
    {
        return _comboMultiplier;
    }

    public void StopCombo()
    {
        SetComboStep(0);
    }

    private void SetComboStep(int value)
    {
        _comboStep = value;
        CalculateComboMultiplier(value);
        DropComboTimer();
    }

    private void DropComboTimer()
    {
        _comboTimer = 0;
    }

    private void CalculateComboMultiplier(int comboStep)
    {
        _comboMultiplier = 1 + comboStep / ComboMultiplierIncreseStep;
        SetComboMultiplierText(_comboMultiplier);
        SetComboMultiplierShow(_comboMultiplier);
    }

    private void Start()
    {
        FillComponents();
        DropComboTimer();
        CalculateComboMultiplier(0);
    }

    private void FillComponents()
    {
        _comboMultiplierText = ComboMultiplierRootGO.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void SetComboMultiplierText(int value)
    {
        _comboMultiplierText.text = $"x{value}";
    }

    private void SetComboMultiplierShow(int value)
    {
        bool needShow = value > 1;
        ComboMultiplierRootGO.SetActive(needShow);
    }

    private void Update()
    {
        MoveTimer();
    }

    private void MoveTimer()
    {
        _comboTimer += Time.deltaTime;
        if(_comboTimer >= ComboIncreaseInterval)
        {
            StopCombo();
        }
    }



}
