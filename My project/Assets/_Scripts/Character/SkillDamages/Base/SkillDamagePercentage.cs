using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamagePercentage : MonoBehaviour
{
    // �Ϲ� ���ݿ� ����� ����Ʈ
    [System.Serializable]
    public class Skill_Common_DamaeP_Per_Level
    {
        public int skillLevel;
        public List<Skill_Common_DamageP_Per_AttTime> skillDamagePerAttTime;
    }

    [System.Serializable]
    public struct Skill_Common_DamageP_Per_AttTime
    {
        public int attackTime;
        public float damageP;
    }


    // �Ϲ� ���� ���� ��� ��ų�� �� ����Ʈ
    [System.Serializable]
    public struct Skill_Uncommon_DamageP_Per_Level
    {
        public int skillLevel;
        public float damageP;
        public float skillCooltime;
    }

    int LeftClick_SkillLevel = 1;
    int RightClick_SkillLevel = 1;

    PlayerStatus ps;

    // 100 110 120
    // 120 130 150
    // ~ �̷�������
    //public List<float> leftClick_DamageP;
    public List<Skill_Common_DamaeP_Per_Level> leftClick_DamageP;

    public List<Skill_Uncommon_DamageP_Per_Level> rightClick_DamageP;

    private void Start()
    {
        ps = GetComponentInParent<PlayerStatus>();
    }

    public int LeftClickDamage(int attackTimes)
    {
        int damage = (int)(ps.lastAtk * leftClick_DamageP[LeftClick_SkillLevel].skillDamagePerAttTime[attackTimes].damageP);

        return damage;
    }

    public int RightClickDamage()
    {
        int damage = (int)(ps.lastAtk * rightClick_DamageP[RightClick_SkillLevel].damageP);

        return damage;
    }
}