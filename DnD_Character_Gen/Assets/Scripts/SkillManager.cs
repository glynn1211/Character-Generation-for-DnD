using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    GameObject skillInstance;
    [SerializeField]
    TextMeshProUGUI type;
    List<SkillBlock> skillBlocks = new List<SkillBlock>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Skills skill in System.Enum.GetValues(typeof(Skills)))
        {
            type.text = skill.ToString();
            GameObject go = Instantiate(skillInstance, skillInstance.transform.parent.transform);
            skillBlocks.Add(go.GetComponent<SkillBlock>().SaveSkillType(skill));
        }
        skillInstance.SetActive(false);
    }

    // Update is called once per frame
    void UpdateSkills()
    {
        foreach (SkillBlock block in skillBlocks)
        {
            //block.SetData(skill);
        }
    }
}
