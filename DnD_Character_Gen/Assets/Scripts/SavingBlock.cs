using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingBlock : MonoBehaviour
{
    //Create method that takes stat as a type so the block knows what type it is 
    Stats stat;
    [SerializeField]
    GameObject toggle;

    public SavingBlock SaveBlockType(Stats _type)
    {
        stat = _type;
        toggle.SetActive(false);
        return this;
    }

    //method to change tickbox state
   

    public void SetData(Stats[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if(stats[i] == stat)
            {
                TurnOn();
                return;
            }
        }
        TurnOff();

    }

    private void TurnOff()
    {
        toggle.SetActive(false);
    }

    private void TurnOn()
    {
        toggle.SetActive(true);
    }
}
