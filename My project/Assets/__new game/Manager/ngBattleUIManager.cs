using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ngBattleUIManager : MonoBehaviour
{
    public static ngBattleUIManager instace;

    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    Canvas BattleUI;

    // �ʿ��� UI Ʋ
    //
    // �� ui ���(ü�¹� �� �̸�), ��� ui ���(ü�¹� �� �̸�), �� ü��, ��� ü��
    //
    //
    // �� action
    // action: skillList, ��� ���� UI
    // 

    GameObject actionUI_Skill;
    TextMeshProUGUI playerMonsterName;
    Slider PlayerMonsterHPBar;
    TextMeshProUGUI playerMonsterHPText;
    List<GameObject> SkillList = new();


    GameObject actionUI_Target;
    List<GameObject> targetList = new();


    GameObject Enemy_StatusUI;
    TextMeshProUGUI enemyMonsterName;
    Slider enemyHP;
    TextMeshProUGUI enemyMonsterHPText;


    // getchild �� ã�ƿ� �� ����
    // ������ ���ؾ� ��
    // �� �������� ȣ��Ǳ� ������ ������� �ص� �ɵ�
    // gameobjcet.getchild(1).getchild(1) �̷������� ���� �� �������� ������

    public void InitializeUI(Canvas playerUI)
    {
        BattleUI = playerUI;

        actionUI_Skill = playerUI.transform.GetChild(0).gameObject;

        actionUI_Target = playerUI.transform.GetChild(1).gameObject;

        Enemy_StatusUI = playerUI.transform.GetChild(2).gameObject;

        //playerMonsterName = actionUI_Skill.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    }

    public void ActionSkillListInitialize()
    {
        playerMonsterName = actionUI_Skill.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        PlayerMonsterHPBar = actionUI_Skill.transform.GetChild(2).gameObject.GetComponent<Slider>();

        playerMonsterHPText = actionUI_Skill.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < actionUI_Skill.transform.GetChild(4).childCount - 1; i++)
        {
            SkillList.Add(actionUI_Skill.transform.GetChild(4).GetChild(i).gameObject);
        }
    }

    public void ActionSkillListUpdate()
    {

    }

    public void ActionTargetListInitialize()
    {
        for (int i = 0; i < actionUI_Target.transform.GetChild(1).childCount - 1; i++)
        {
            targetList.Add(actionUI_Target.transform.GetChild(1).GetChild(i).gameObject);
        }
    }

    public void ActionTargetListUpdate()
    {

    }

    public void EnemyStatusInitialize()
    {
        enemyMonsterName = Enemy_StatusUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        enemyHP = Enemy_StatusUI.transform.GetChild(2).gameObject.GetComponent<Slider>();

        enemyMonsterHPText = Enemy_StatusUI.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

    }

    public void EnemyStatusUpdate()
    {

    }
}
