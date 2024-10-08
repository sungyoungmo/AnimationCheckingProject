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
    GameObject actionUI_Target;

    GameObject StatusUI;
    Scrollbar enemyHP;
    Scrollbar myHP;

    TextMeshProUGUI enemyName;
    TextMeshProUGUI myName;


    // getchild 로 찾아올 거 같음
    // 순서를 정해야 함
    // 층 기준으로 호출되기 떄문에 순서대로 해도 될듯
    // gameobjcet.getchild(1).getchild(1) 이런식으로 들어가야 함 안쪽으로 들어가려면

    public void InitializeUI(Canvas playerUI)
    {
        BattleUI = playerUI;

        playerUI.transform.GetChild(0);

    }




}
