using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : MonoBehaviour
{
    [SerializeField]
    GameObject statModifier;

    Dictionary<string, ModifierBlock> blocks = new Dictionary<string, ModifierBlock>();

    public void AddStat(Stats _type)
    {
        statModifier.SetActive(true);
        GameObject go = Instantiate(statModifier, statModifier.transform.parent);
        //get componenet mod block
        ModifierBlock modBlock = go.GetComponent<ModifierBlock>();
        //and it to the Dictionary with the key of the stat type
        blocks.Add(_type.ToString(), modBlock);

        statModifier.SetActive(false);
    }

    public void UpdateModifier(Stats _type, int _value)
    {
        //get the stat mod from the Dictionary and then give it the _value
        string type = _type.ToString();
        if (blocks.ContainsKey(type)) 
        {
            blocks[type].SetValue(_value);
        }
    }
}
