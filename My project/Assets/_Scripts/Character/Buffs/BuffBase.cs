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
		//버프 이펙트 발생
    }
}
