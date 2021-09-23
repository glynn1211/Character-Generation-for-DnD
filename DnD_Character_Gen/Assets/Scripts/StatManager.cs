using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{

    [SerializeField]
    StatModifier statModifier;

    [SerializeField]
    GameObject statInstance;
    List<Stat> stats = new List<Stat>();
    int maxpoints = 21;
    int currentpoints;
    int speciaPoints;
    Races currentRace = null;

    private void Start()
    {
        currentpoints = maxpoints;
        foreach (Stats stat in System.Enum.GetValues(typeof(Stats)))
        {
            statModifier.AddStat(stat);
            AddNewStat(stat);
        }
        statInstance.SetActive(false);
    }

    void AddNewStat(Stats _type)
    {
        stats.Add(CreateStat(_type));
    }

    private void StateChange(int _value, Stats _type)
    {

    }

    public void SetRace(Races raceType)
    {
        if(currentRace != null)
        {
            if(!currentRace.playerChoice)
            {
                for (int i = 0; i < currentRace.statBoost.Length; i++)
                {
                    stats[(int)currentRace.statBoost[i].type].RemoveRaceBonus(currentRace.statBoost[i].boostAmount);
                }
            }
            else
            {
                for (int i = 0; i < currentRace.numOfStatPoints; i++)
                {
                    for (int j = 0; j < stats.Count; j++)
                    {
                        if(stats[j].GetValue() > 8)
                        {
                            stats[j].RemoveBoost();
                            break;  
                        }
                    }
                }  
            }
        }
        currentRace = raceType;
        if(!currentRace.playerChoice)
        {
            for (int i = 0; i < raceType.statBoost.Length; i++)
            {
                stats[(int)raceType.statBoost[i].type].AddRaceBonus(raceType.statBoost[i].boostAmount);
            } 
        }
        else
        {
            currentpoints += currentRace.numOfStatPoints;
        }
    }

    public void AddtoAssignableStats()
    {
        currentpoints++;   
    }



    public bool CheckCurrentAssignablePoints()
    {
        if(currentpoints > 0)
        {
            currentpoints--;
            return true;
        }
        else
        {
            return false;
        }
    }

    //All stat blocks will when updated update there modifier value with this callback
    //this calls to the modifers and says hey this is your type and its now this value
    public void UpdateStat(Stats _type, int _value) 
    {
        statModifier.UpdateModifier(_type, _value);
    }

    Stat CreateStat(Stats _type)
    {
        GameObject go = Instantiate(statInstance, statInstance.transform.parent.transform);
        Stat sb = go.GetComponent<Stat>();
        sb.SetCallBack(_type, this, UpdateStat);
        return sb;
    }

    public int GetStat(Stats _type)
    {
        return stats[(int)_type].GetValue();
    }
}
