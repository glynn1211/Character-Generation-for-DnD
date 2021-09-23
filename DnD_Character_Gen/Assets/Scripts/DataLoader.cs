using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private static DataLoader dataLoader;
    public static DataLoader DataModel { get { return dataLoader; } }
    public AppData appData;

    static Races[] races;
    static DnDClass[] classes;
    static Dictionary<string, DnDClass> _classes = new Dictionary<string, DnDClass>();
    static Dictionary<string, Races> _races = new Dictionary<string, Races>();

    // Start is called before the first frame update
    void Awake()
    {
        if(dataLoader != null && dataLoader != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            dataLoader = this;
        }
        LoadData();
    }

    private void LoadData()
    {
        appData = JsonUtility.FromJson<AppData>(File.ReadAllText(Application.streamingAssetsPath + "/Classes.json"));
        races = appData.Races;
        classes = appData.Classes;
        foreach (DnDClass _class in classes)
        {
            _classes.Add(_class.type, _class);
        }

        foreach (Races _race in races)
        {
            _races.Add(_race.type, _race);
        }
    }

    public static DnDClass[] GetClasses()
    {
        return classes;
    }

    public static Races[] GetRaces()
    {
        return races;
    }

    public static Races GetRace(string _type)
    {
       if(_races.ContainsKey(_type))
        {
            return _races[_type];
        }
        return null;
    }

    public static DnDClass GetDnDClass(string _type)
    {
        if(_classes.ContainsKey(_type))
        {
            return _classes[_type];
        }
        return null;
    }
}
