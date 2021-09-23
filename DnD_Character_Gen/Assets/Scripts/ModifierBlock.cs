using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModifierBlock : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textUi;

    int lowerThreshHold = 10;
    int upperThreshHold = 11;

    int currentModifier = 0;

    public void SetValue(int _value)
    {
        currentModifier = GetValue(_value);
        textUi.text = currentModifier.ToString();
    }

    int GetValue(int _value) 
    {
        if (_value < lowerThreshHold)
        {
            //going to be a pos
            return Mathf.FloorToInt((_value - 10) * 0.5f);
        }
        if (_value > upperThreshHold)
        {
            //going to be a pos
            return Mathf.FloorToInt((_value - 10) * 0.5f);
        }
        return 0;
    }

    public int GetModifier() 
    {
        return currentModifier;
    }
}
