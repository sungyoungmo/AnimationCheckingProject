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
        // �ӵ��� ���� ���ؼ� ��ųʸ� ������
        // �� ���� Ư�� ���� ���� ���� ������ �����Ǿ ���ĵ� 1,3�� ���� ���̸� 3�� ������ 1 �Ʒ��� ����
        // ���� ������ �̱���

        var sortedBySPD = skill_playerOrdered.OrderByDescending(monster => monster.Key.monsterInfo.lastSPD);

        yield return new WaitForSeconds(1.0f);

        foreach (var order in sortedBySPD)
        {
            // �ڷ�ƾ�� ����Ͽ� ������ ���� �ִϸ��̼� �� ������ ����
            yield return new WaitForSeconds(1.0f);

            // ��ų �ִϸ��̼� ����
            order.Key.anim.SetTrigger(order.Value.skill.skill_Anim_ParameterName);
            order.Value.target.anim.SetTrigger("GetHit");

            yield return new WaitForSeconds(1.0f);

            // ��ų ������ ����
            order.Value.target.monsterInfo.currentHP -= order.Value.skill.damage;

            order.Value.skill.currentSP -= 1;

            // �׾��� ��
            if (order.Value.target.monsterInfo.currentHP <= 0)
            {
                order.Value.target.anim.SetTrigger("IsDead");

                yield return new WaitForSeconds(3.0f);


                // ��ġ ������ �����
            }


            // ���� �� ������� ���߿� ����
        }

    }
}
