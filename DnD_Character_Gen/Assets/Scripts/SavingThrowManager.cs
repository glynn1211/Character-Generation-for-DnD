using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavingThrowManager : MonoBehaviour
{
    [SerializeField]
    GameObject savingInstance;

    [SerializeField]
    TextMeshProUGUI type;

    List<SavingBlock> savingBlocks = new List<SavingBlock>();
    private void Start()
    {
        foreach (Stats stat in System.Enum.GetValues(typeof(Stats)))
        {
            type.text = stat.ToString();
            GameObject go = Instantiate(savingInstance, savingInstance.transform.parent.transform);
            savingBlocks.Add(go.GetComponent<SavingBlock>().SaveBlockType(stat));
        }
        savingInstance.SetActive(false);
    }

    public void UpdateSaves(Stats[] stats)
    {
        foreach (SavingBlock block in savingBlocks)
        {
            block.SetData(stats);
        }
        //foreach SavingBlock pass it stats data
    }

}
