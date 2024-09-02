using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackDamageBuff", menuName = "_ScriptableObject/Buffs/DamageBuff")]
public class AttackDamageBuff : BuffBase
{
    public override void BuffOrDebuffEffect(PlayerStatus _player)
    {
        _player.lastAtk = _player.lastAtk + _player.lastAtk / 5;
    }

    public override void BuffOrDebuffEffectClear(PlayerStatus _player)
    {
        _player.lastAtk = _player.rootAtk;
    }
}
