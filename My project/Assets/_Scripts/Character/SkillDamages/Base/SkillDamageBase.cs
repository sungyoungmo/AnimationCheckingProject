using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageBase : MonoBehaviour
{
    // ���� �� ������, Ÿ�� �� �������� ������ �ϴϱ�
    // 3,3 ���� ������ �� ��ų �ƽ������� 3
    // �����غ���

    // 100 110 120
    // 120 130 150
    // ~ �̷�������
    public List<float> leftClick_DamageP;

    public List<float> rightClick_DamageP;

    public int LeftClick_Max_AttackTime()
    {
        return leftClick_DamageP.Count;
    }


}
