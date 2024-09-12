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


    // 갑자기 아무것도 안되는 상황이 발생함
    // 체력이 0이하로 내려갔을 때 등등 조건 따져서 확인해보기

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ihittable>(out Ihittable hitMob))
        {
            hitMob.Hit_Call(damage, player);
        }
    }
}
