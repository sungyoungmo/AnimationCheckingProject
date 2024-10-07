using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ngMonsterName", menuName = "Monster_ScriptableObject/ngMonster")]
public class ngMonster : ScriptableObject
{
    public ngMonsterType ownType;

    public int maxHP;

    public int rootATK;

    public int rootSPD;

    public int rootDEF;

    public List<ngMonsterSkill> skillList;

    [Space(20)]
    [HideInInspector]
    public int currentHP;

    [HideInInspector]
    public int lastATK;
    
    [HideInInspector]
    public int lastSPD;
    
    [HideInInspector]
    public int lastDEF;

    [HideInInspector]
    public bool isDead = false;

    [HideInInspector]
    public byte buffs;

    [HideInInspector]
    public byte debuffs;

    private void Awake()
    {
        currentHP = maxHP;
        lastATK = rootATK;
        lastSPD = rootSPD;
        lastDEF = rootDEF;
    }
}
