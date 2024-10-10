using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ngMonsterSkillName", menuName = "Monster_ScriptableObject/ngMonsterSkill")]
public class ngMonsterSkill : ScriptableObject
{
    public string skillName;

    public string skillText;

    public Skilltype skill_Type;

    public monsterType skill_Elemtal_Type;

    public int damage;

    public int maxSP;

    [HideInInspector]
    public int currentSP;

    public GetBuff getBuff;

    public TakeDebuff takeDebuff;

    public string skill_Anim_ParameterName;

    private void Awake()
    {
        currentSP = maxSP;
    }

    //public ngMonsterSkillCast skillCasting;
}

public enum Skilltype
{
    ATK,
    Buff,
    Debuff
}

public enum buffs
{
   NONE,
   ATK_UP,
   DEF_UP
}

public enum debuffs
{
    NONE,
    ATK_DOWN,
    DEF_DOWN
}

[System.Serializable]
public class GetBuff
{
    public buffs buff;
    public int duration;
}

[System.Serializable]
public class TakeDebuff
{
    public debuffs debuff;
    public int duration;
}