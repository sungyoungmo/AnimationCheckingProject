using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorUpBuffScriptableObject", menuName = "_ScriptableObject/Buffs/ArmorUpBuff")]
public class DefenseArmorUpBuff : BuffBase
{
    public override void BuffOrDebuffEffect(PlayerStatus _player)
    {
        _player.lastDefArmor = _player.lastDefArmor + _player.rootDefArmor / 5;
    }

    public override void BuffOrDebuffEffectClear(PlayerStatus _player)
    {
        _player.lastDefArmor = _player.rootDefArmor;
    }
}
