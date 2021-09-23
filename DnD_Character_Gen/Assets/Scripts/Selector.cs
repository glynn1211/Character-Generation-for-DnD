using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    int currentNum = -1;

    [SerializeField]
    TextMeshProUGUI tmpText;
    string currentSelection;


    [SerializeField]
    Button nextButton;

    [SerializeField]
    Button submitButton;

    int numberOfItems;

    Action<string> callback;

    string[] items;

    string startingItem;

    private void Start()
    {
        nextButton.onClick.AddListener(NextItem);
        submitButton.onClick.AddListener(SubmitItem);
    }

    private void SubmitItem()
    {
        if (tmpText.text != startingItem)
        {
            callback(currentSelection);
        }
    }

    public void Init(string _selectorName, Action<string> _submit, string[] _values)
    {
        tmpText.text = _selectorName;
        startingItem = _selectorName;
        numberOfItems = _values.Length;
        callback = _submit;
        items = _values;
    }

    private void NextItem()
    {
        if (currentNum >= numberOfItems - 1)
        {
            currentNum = 0;
        }
        else
        {
            currentNum++;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        currentSelection = items[currentNum];
        tmpText.text = currentSelection;
    }

}
