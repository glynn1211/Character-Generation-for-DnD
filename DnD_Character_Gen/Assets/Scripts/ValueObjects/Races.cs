using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Races
{
    public string type;
    public bool playerChoice;
    public int numOfStatPoints;
    public StatBoost[] statBoost;
    public Ability[] abilities;
}
[Serializable]
public class StatBoost
{
    public Stats type;
    public int boostAmount;
}
