using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageConroller : MonoBehaviour
{
    BoxCollider bc;
    SkillDamagePercentage skillInfo;

    int damage;
    int attackTime;
    PlayerController player;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        skillInfo = GetComponent<SkillDamagePercentage>();
        player = GetComponentInParent<PlayerController>();
    }


    // ���ڱ� �ƹ��͵� �ȵǴ� ��Ȳ�� �߻���
    // ü���� 0���Ϸ� �������� �� ��� ���� ������ Ȯ���غ���

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ihittable>(out Ihittable hitMob))
        {
            hitMob.Hit_Call(damage, player);
        }
    }
}
