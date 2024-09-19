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

    LayerMask targetLayer;


    List<Ihittable> hitMobList = new();

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        skillInfo = GetComponent<SkillDamagePercentage>();
        player = GetComponentInParent<PlayerController>();

        if (player.gameObject.layer == 10)
        {
            targetLayer = 10;
        }
        else if (player.gameObject.layer == 11)
        {
            targetLayer = 11;
        }

            
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
        if (other.TryGetComponent<Ihittable>(out Ihittable hitMob) && other.gameObject.layer == targetLayer)
        {
            if (!hitMobList.Contains(hitMob))
            {
                hitMob.Hit_Call(damage, player);
                hitMobList.Add(hitMob);
            }
        }
    }
}
