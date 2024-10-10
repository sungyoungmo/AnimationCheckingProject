using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct Skill_Slot
{
    public Button Button_Skill;

    public TextMeshProUGUI Text_SkillName;

    public TextMeshProUGUI Text_SkillPoint;

    public TextMeshProUGUI Text_SKillType;

    public Skill_Slot(Button button_Skill, TextMeshProUGUI text_SkillName, TextMeshProUGUI text_SkillPoint, TextMeshProUGUI text_SKillType)
    {
        Button_Skill = button_Skill;
        Text_SkillName = text_SkillName;
        Text_SkillPoint = text_SkillPoint;
        Text_SKillType = text_SKillType;
    }
}

public class ngBattleUIManager : MonoBehaviour
{
    public static ngBattleUIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    List<Skill_Slot> SkillSlot = new();


    GameObject actionUI_Target;
    List<Button> targetListButton = new();
    List<GameObject> targetList = new();


    GameObject Enemy_StatusUI;
    TextMeshProUGUI enemyMonsterName;
    Slider enemyHP;
    TextMeshProUGUI enemyMonsterHPText;

    ngMonsterController myMonster;

    // getchild 로 찾아올 거 같음
    // 순서를 정해야 함
    // 층 기준으로 호출되기 떄문에 순서대로 해도 될듯
    // gameobjcet.getchild(1).getchild(1) 이런식으로 들어가야 함 안쪽으로 들어가려면

    public void Initialize(Canvas playerUI, ngMonsterController playerMonster)
    {
        BattleUI = playerUI;

        actionUI_Skill = playerUI.transform.GetChild(0).gameObject;

        actionUI_Target = playerUI.transform.GetChild(1).gameObject;

        Enemy_StatusUI = playerUI.transform.GetChild(2).gameObject;

        foreach (var player in GameManager.instance.playerList)
        {
            if (player == playerMonster)
            {
                myMonster = player;
            }
        }

        ActionSkillListInitialize();

        ActionTargetListInitialize();

        EnemyStatusInitialize();
    }

    public void ActionSkillListInitialize()
    {
        playerMonsterName = actionUI_Skill.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        PlayerMonsterHPBar = actionUI_Skill.transform.GetChild(2).gameObject.GetComponent<Slider>();

        playerMonsterHPText = actionUI_Skill.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < actionUI_Skill.transform.GetChild(4).childCount; i++)
        {
            SkillList.Add(actionUI_Skill.transform.GetChild(4).GetChild(i).gameObject);
        }

        foreach (var skill in SkillList)
        {
            SkillSlot.Add
                (
                new Skill_Slot
                    (
                    skill.transform.GetChild(0).GetComponent<Button>(),
                    skill.transform.GetChild(1).GetComponent<TextMeshProUGUI>(),
                    skill.transform.GetChild(2).GetComponent<TextMeshProUGUI>(),
                    skill.transform.GetChild(3).GetComponent<TextMeshProUGUI>()
                    )
                );
        }
    }

    public void ActionSkillListUpdate()
    {
        playerMonsterName.text = myMonster.monsterInfo.monsterName;

        PlayerMonsterHPBar.value = myMonster.monsterInfo.currentHP / myMonster.monsterInfo.maxHP;

        playerMonsterHPText.text = $"{myMonster.monsterInfo.currentHP} / {myMonster.monsterInfo.maxHP}";

        for (int i = 0; i < myMonster.monsterInfo.skillList.Count; i++)
        {
            SkillSlot[i].Button_Skill.interactable = true;
            SkillSlot[i].Text_SkillName.text = myMonster.monsterInfo.skillList[i].skillName;
            SkillSlot[i].Text_SkillPoint.text = $"{myMonster.monsterInfo.skillList[i].currentSP} / {myMonster.monsterInfo.skillList[i].maxSP}";
            SkillSlot[i].Text_SKillType.text= myMonster.monsterInfo.skillList[i].skill_Elemtal_Type.ToString();
        }

        for (int i = SkillSlot.Count - 1; i >= myMonster.monsterInfo.skillList.Count; i--)
        {
            SkillSlot[i].Button_Skill.interactable = false;
            SkillSlot[i].Text_SkillName.text = null;
            SkillSlot[i].Text_SkillPoint.text = null;
            SkillSlot[i].Text_SKillType.text = null;
        }

    }

    public void ActionTargetListInitialize()
    {
        for (int i = 0; i < actionUI_Target.transform.GetChild(1).childCount; i++)
        {
            // 리스트의 아래 있는 버튼을 리스트로 받아오기
            targetList.Add(actionUI_Target.transform.GetChild(1).GetChild(i).gameObject);

            targetListButton.Add(targetList[i].transform.GetChild(0).GetComponent<Button>());


            //targetList.Add(actionUI_Target.transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>());
        }

        Debug.Log(targetListButton.Count);
    }

    public void ActionTargetListUpdate()
    {
        Debug.Log($"tl : {targetList.Count} tlb: {targetListButton.Count} pl: {GameManager.instance.playerList.Count}");
        

        for (int i = 0; i < GameManager.instance.playerList.Count; i++)
        {
            //targetList[i].transform.GetChild(1).GetComponent<Text>().text = GameManager.instance.playerList[i].GetComponent<ngMonsterController>().monsterInfo.monsterName;
            //targetList[i].transform.GetChild(1)

            targetListButton[i].transform.GetChild(0).GetComponentInChildren<Text>().text =
                $"{GameManager.instance.playerList[i].GetComponent<ngMonsterController>().monsterInfo.monsterName} {(GameManager.instance.playerList[i] == myMonster ? "(나)" : "(상대)")}";

        }

    }

    public void EnemyStatusInitialize()
    {
        enemyMonsterName = Enemy_StatusUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        enemyHP = Enemy_StatusUI.transform.GetChild(2).gameObject.GetComponent<Slider>();

        enemyMonsterHPText = Enemy_StatusUI.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

    }

    public void EnemyStatusUpdate()
    {
        Debug.Log($"ESU.pl: {GameManager.instance.playerList.Count}");

        foreach (var item in GameManager.instance.playerList)
        {
            if (item != myMonster)
            {
                enemyMonsterName.text = item.monsterInfo.monsterName;

                enemyHP.value = item.monsterInfo.currentHP / item.monsterInfo.maxHP;

                enemyMonsterHPText.text = $"{item.monsterInfo.currentHP} / {item.monsterInfo.maxHP}";
            }
        }
    }

    public void TurnStartUIUpdate()
    {
        ActionSkillListUpdate();
        ActionTargetListUpdate();
        EnemyStatusUpdate();
    }

}
