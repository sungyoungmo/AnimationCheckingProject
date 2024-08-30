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

	// ���� �Ŵ������� buffbase.bufforde~ �� ȣ���ϸ� �׿� �´� ������ ����� �߻�
	public abstract void BuffOrDebuffEffect(PlayerStatus _player);
}
