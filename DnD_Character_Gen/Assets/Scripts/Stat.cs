using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    [SerializeField]
    Button minus;
    [SerializeField]
    Button plus;
    [SerializeField]
    TextMeshProUGUI statNumber;
    [SerializeField]
    TextMeshProUGUI type;
    StatManager manager;
    int currentStatValue = 8;
    int bonusValue = 0;

    Stats statType;

    Action<Stats, int> onStatChange;
    void Start()
    {
        UpdateStat();
        minus.onClick.AddListener(HandleMinus);
        plus.onClick.AddListener(HandlePlus);
    }
    public void SetCallBack(Stats _type, StatManager _manager, Action<Stats, int> _changeValue)
    {
        statType = _type;
        onStatChange = _changeValue;
        type.text = _type.ToString();
        manager = _manager;
    }
    private void HandlePlus()
    {
        if(currentStatValue < 15)
        {
            if (manager.CheckCurrentAssignablePoints())
            {
                currentStatValue++;
                UpdateStat();
            }
        }
    }
    public int GetValue()
    {
        return (currentStatValue + bonusValue);
    }
    private void HandleMinus()
    {
        if(currentStatValue > 8)
        {
            currentStatValue--;
            manager.AddtoAssignableStats();
            UpdateStat();
        }
    }
    public void RemoveBoost()
    {
        currentStatValue--;
    }
    public void AddRaceBonus(int boostAmount)
    {
        bonusValue += boostAmount;
        UpdateStat();
    }
    public void RemoveRaceBonus(int boostAmount)
    {
        bonusValue -= boostAmount;
        UpdateStat();
    }
    private void UpdateStat()
    {
        statNumber.text = (currentStatValue + bonusValue).ToString();
        if (onStatChange != null)
        {
            onStatChange(statType, (currentStatValue + bonusValue));
        }
    }
}
