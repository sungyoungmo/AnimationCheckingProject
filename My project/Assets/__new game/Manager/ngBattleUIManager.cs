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
    GameObject actionUI_Target;

    GameObject StatusUI;
    Scrollbar enemyHP;
    Scrollbar myHP;

    TextMeshProUGUI enemyName;
    TextMeshProUGUI myName;


    // getchild �� ã�ƿ� �� ����
    // ������ ���ؾ� ��
    // �� �������� ȣ��Ǳ� ������ ������� �ص� �ɵ�
    // gameobjcet.getchild(1).getchild(1) �̷������� ���� �� �������� ������

    public void InitializeUI(Canvas playerUI)
    {
        BattleUI = playerUI;

        playerUI.transform.GetChild(0);

    }




}
