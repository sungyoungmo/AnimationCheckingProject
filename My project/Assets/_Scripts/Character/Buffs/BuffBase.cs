using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
	AttckDamage,
	MoveSpeed,
	AttackSpeed,
	DefenseArmorUp
}

public class BuffBase : MonoBehaviour
{
	public float buffDuration;
	


	public virtual void BuffEffect()
    {
		//���� ����Ʈ �߻�
    }
}
