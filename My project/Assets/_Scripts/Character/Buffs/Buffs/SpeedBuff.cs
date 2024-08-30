using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedBuff", menuName = "_ScriptableObject/Buffs/SpeedBuff")]
public class SpeedBuff : BuffBase
{
    public override void BuffOrDebuffEffect(PlayerStatus _player)
    {
        _player.lastSpd = _player.lastSpd + (int)(_player.lastSpd / 5) ;
    }
}