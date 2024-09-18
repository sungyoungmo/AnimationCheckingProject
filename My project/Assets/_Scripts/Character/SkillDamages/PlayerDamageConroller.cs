using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageConroller : MonoBehaviour
{
    BoxCollider bc;
    SkillDamagePercentage skillInfo;

    int damage = 10;
    int attackTime;
    PlayerController player;

    List<Ihittable> hitMobList = new();

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        skillInfo = GetComponent<SkillDamagePercentage>();
        player = GetComponentInParent<PlayerController>();
    }

    public void EnableCollider()
    {
        bc.enabled = true;
        int comboCount = player.charMove.anim.GetInteger(player.charMove.ComboCount);
        damage = skillInfo.LeftClickDamage(comboCount);
        hitMobList.Clear();
    }

    public void DisableCollider()
    {
        bc.enabled = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ihittable>(out Ihittable hitMob))
        {
            if (!hitMobList.Contains(hitMob))
            {
                hitMob.Hit_Call(damage, player);
                hitMobList.Add(hitMob);
            }
        }
    }
}
