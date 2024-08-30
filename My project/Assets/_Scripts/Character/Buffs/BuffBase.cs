using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffOrDebuffType
{
	AttckDamage = 1,
	MoveSpeed,
	AttackSpeed,
	DefenseArmorUp,

	Bleeding = 9,
	Stuck,
	Slow
}

[CreateAssetMenu(fileName = "NewBuff", menuName = "_ScriptableObject/Buffs/BuffBase")]
public abstract class BuffBase : ScriptableObject
{
	public float buffOrDebuffDuration;

	public BuffOrDebuffType _BuffOrDebuffType;

	// 버프 매니저에서 buffbase.bufforde~ 를 호출하면 그에 맞는 버프나 디버프 발생
	public abstract void BuffOrDebuffEffect(PlayerStatus _player);
}
