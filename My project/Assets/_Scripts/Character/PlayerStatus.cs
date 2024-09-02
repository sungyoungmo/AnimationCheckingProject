using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int rootAtk; // 버프 적용 전, 스텟 업그레이드를 통해 변하는 공격력   / 100
    public int lastAtk; // 버프 적용 후, 최종적으로 계산된 실제 공격력        /100

    public float atk_spd;   // 공격 속도(애니메이션에 입혀 공격 속도 조정)
    public float lastAtk_spd;   // 버프 적용 후, 최종적으로 계산된 실제 공격 속도

    public float rootSpd;   // 이동 속도(애니메이션과 연동해 이동속도 조절)
    public float lastSpd;   // 버프 적용 후, 최종적으로 계산된 실제 이동 속도

    public int rootDefArmor;    // hit 때 데미지 조절을 위한 방어력 계수
    public int lastDefArmor;    // 버프 적용 후, 최종적으로 계산된 실제 방어력

    public int maxHp;    // 체력 / 100
    public int currentHp;   // 현재 체력 / 100
    public int hpGenerate;  // 5초당 체력 회복량

    public int exp; // 경험치(레벨 업 시에 포인트를 사용해 스킬 데미지 및 쿨타임 업그레이드 가능)

    public bool isDead; // 플레이어가 죽었나(버프 지급 여부 판단에 사용할 예정)

    public byte buffs_Or_Debuffs;  // 플레이어가 가진 버프들
}