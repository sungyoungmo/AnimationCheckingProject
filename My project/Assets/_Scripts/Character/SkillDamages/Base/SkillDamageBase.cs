using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageBase : MonoBehaviour
{
    // 레벨 별 데미지, 타수 별 데미지를 따져야 하니까
    // 3,3 으로 만들어야 함 스킬 맥스레벨이 3
    // 생각해보기

    // 100 110 120
    // 120 130 150
    // ~ 이런식으로
    public List<float> leftClick_DamageP;

    public List<float> rightClick_DamageP;

    public int LeftClick_Max_AttackTime()
    {
        return leftClick_DamageP.Count;
    }


}
