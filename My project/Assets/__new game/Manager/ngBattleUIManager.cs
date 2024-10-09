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

    // 필요한 UI 틀
    //
    // 내 ui 배경(체력바 및 이름), 상대 ui 배경(체력바 및 이름), 내 체력, 상대 체력
    //
    //
    // 내 action
    // action: skillList, 대상 선택 UI
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


    // getchild 로 찾아올 거 같음
    // 순서를 정해야 함
    // 층 기준으로 호출되기 떄문에 순서대로 해도 될듯
    // gameobjcet.getchild(1).getchild(1) 이런식으로 들어가야 함 안쪽으로 들어가려면

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
