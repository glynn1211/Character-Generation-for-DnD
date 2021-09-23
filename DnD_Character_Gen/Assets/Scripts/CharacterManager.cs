using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    Character character = new Character();

    [SerializeField]
    StatManager statManager;

    [SerializeField]
    GameObject selector;

    [SerializeField]
    SavingThrowManager throwManager;

    List<Selector> selectors = new List<Selector>();

    void Start()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Races[] raceData = DataLoader.GetRaces();
        DnDClass[] classData = DataLoader.GetClasses();

        //create the race selection
        List<string> options = new List<string>();

        for (int i = 0; i < raceData.Length; i++)
        {
            options.Add(raceData[i].type);
        }

        selectors.Add(CreateSelector("Race", HandleRaceSelect, options.ToArray()));

        options.Clear();

        //create the class selector
        for (int i = 0; i < classData.Length; i++)
        {
            options.Add(classData[i].type);
        }

        selectors.Add(CreateSelector("Class", HandleClassSelect, options.ToArray()));

        options.Clear();

        selector.SetActive(false);

        Vector2 selectorSize = selector.GetComponent<RectTransform>().sizeDelta;
        selector.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(selectorSize.x * selectors.Count, selectorSize.y);
    }

    private Selector CreateSelector(string _selectorName, Action<string> _callback, string[] _values)
    {
        GameObject newSelector = Instantiate(selector, selector.transform.parent);
        Selector s = newSelector.GetComponent<Selector>();
        s.Init(_selectorName, _callback, _values);
        return s;
    }

    private void HandleClassSelect(string charClass)
    {
        Debug.Log(charClass);
        character.CharClass = charClass;
        throwManager.UpdateSaves(DataLoader.GetDnDClass(charClass).savingThrows);
       // Debug.Log(DataLoader.GetDnDClass(charClass).savingThrows[0]);    
    }
    private void HandleRaceSelect(string charRace)
    {
        Debug.Log(charRace);
        character.CharRace = charRace;
        Races race = DataLoader.GetRace(charRace);
        if (race != null)
        {
            statManager.SetRace(race);
        }
    }
}
