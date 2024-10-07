using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct SkillOrder
{
    public ngMonsterSkill skill { get; private set; }
    public ngMonsterController target { get; private set; }

    public void Initialize(ngMonsterSkill skill, ngMonsterController target)
    {
        this.skill = skill;
        this.target = target;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<ngMonsterController> playerList = new();

    public Dictionary<ngMonsterController, SkillOrder> skill_playerOrdered = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (skill_playerOrdered.Count == 2)
        {
            StartCoroutine(matchCoroutine());

            skill_playerOrdered.Clear();
        }
    }


    public void PlayerAdd()
    {
        ngMonsterController[] players = FindObjectsOfType<ngMonsterController>();

        foreach (var player in players)
        {
            if (!playerList.Contains(player))
            {
                playerList.Add(player);
            }
        }
    }

    public void CastOrder(ngMonsterController casterViewID, ngMonsterSkill castingSkill, int targetViewID)
    {
        //castingSkill.damage

        foreach (var target in playerList)
        {
            if (target.photonView.ViewID == targetViewID)
            {
                SkillOrder skillOrder = new();

                skillOrder.Initialize(castingSkill, target);

                skill_playerOrdered.Add(casterViewID, skillOrder);

                break;
            }
        }
    }


    IEnumerator matchCoroutine()
    {
        // 속도를 통해 비교해서 딕셔너리 재정렬
        // 이 정렬 특성 같은 값에 대한 순서는 보존되어서 정렬됨 1,3이 같은 값이면 3은 무조건 1 아래에 있음
        // 순서 랜덤은 미구현

        var sortedBySPD = skill_playerOrdered.OrderByDescending(monster => monster.Key.monsterInfo.lastSPD);

        yield return new WaitForSeconds(1.0f);

        foreach (var order in sortedBySPD)
        {
            // 코루틴을 사용하여 순서에 따라 애니메이션 및 데미지 실행
            yield return new WaitForSeconds(1.0f);

            // 스킬 애니메이션 실행
            order.Key.anim.SetTrigger(order.Value.skill.skill_Anim_ParameterName);
            order.Value.target.anim.SetTrigger("GetHit");

            yield return new WaitForSeconds(1.0f);

            // 스킬 데미지 실행
            order.Value.target.monsterInfo.currentHP -= order.Value.skill.damage;

            order.Value.skill.currentSP -= 1;

            // 죽었을 시
            if (order.Value.target.monsterInfo.currentHP <= 0)
            {
                order.Value.target.anim.SetTrigger("IsDead");

                yield return new WaitForSeconds(3.0f);


                // 매치 끝나게 만들기
            }


            // 버프 및 디버프는 나중에 구현
        }

    }
}
