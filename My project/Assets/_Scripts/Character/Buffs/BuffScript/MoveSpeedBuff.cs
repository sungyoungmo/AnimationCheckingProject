using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveSpeedBuff", menuName = "_ScriptableObject/Buffs/MoveSpeedBuff")]
public class MoveSpeedBuff : BuffBase
{
    public override void BuffOrDebuffEffect(PlayerStatus _player)
    {
        _player.lastSpd = _player.lastSpd + (int)(_player.lastSpd / 5) ;
        _player.gameObject.GetComponent<Animator>().SetFloat("MoveSpeed", _player.lastSpd / 100);
    }

    public override void BuffOrDebuffEffectClear(PlayerStatus _player)
    {
        _player.lastSpd = _player.rootSpd;
        _player.gameObject.GetComponent<Animator>().SetFloat("MoveSpeed", _player.rootSpd / 100);
    }
}