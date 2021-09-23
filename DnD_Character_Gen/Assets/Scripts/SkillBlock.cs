using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlock : MonoBehaviour
{
    Skills skill;

    public SkillBlock SaveSkillType(Skills _type)
    {
        skill = _type;
        return this;
    }


   
}
