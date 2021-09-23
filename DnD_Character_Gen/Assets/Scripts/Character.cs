using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    string charClass;
    string charRace;
    int str;
    int dex;
    int con;
    int intel;
    int wis;
    int cha;

    public string CharClass { get { return charClass; } set { charClass = value; } }
    public string CharRace { get { return charRace; } set { charRace = value; } }
    public int Str { get { return str; } set { str = value; } }
    public int Dex { get { return dex; } set { dex = value; } }
    public int Con { get { return con; } set { con = value; } }
    public int Intel { get { return intel; } set { intel = value; } }
    public int Wis { get { return wis; } set { wis = value; } }
    public int Cha { get { return cha; } set { cha = value; } }
}
