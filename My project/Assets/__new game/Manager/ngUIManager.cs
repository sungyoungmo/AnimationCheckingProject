using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ngUIManager : MonoBehaviour
{
    public static ngUIManager instance;

    public GameObject currentUI;

    public GameObject LoadingUI;
    public GameObject LobbyUI;
    public GameObject MatchingUI;

    public TextMeshProUGUI loadingUI_LoadingText;
    public TextMeshProUGUI matchingUI_matchingText;
    public TextMeshProUGUI matchingUI_matchingTimer;

    public Slider loadingUI_LoadingSlider;

    bool timerOn = false;

    float time;

    int sec;
    int min;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        currentUI = LoadingUI;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (timerOn)
        {
            time += Time.deltaTime;

            min = (int)(time / 60);
            sec = (int)(time % 60);

            matchingUI_matchingTimer.text = ($"{min}m : {sec}s");
        }
        
    }


    public void MatchStartButtonClick()
    {
        ngPhotonManager.instance.randMatchingStart();

        UIChangeInto(MatchingUI);

        timerOn = true;
        time = 0;
    }

    public void CancelButtonClick()
    {
        ngPhotonManager.instance.LeftRoom();

        UIChangeInto(LobbyUI);

        timerOn = false;
    }

    public void ExitButtonClick()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif


    }

    public void UITextChange(GameObject UI, string txt)
    {
        //UI.GetComponentInChildren
    }


    public void UIChangeInto(GameObject nowUI)
    {
        currentUI.SetActive(false);
        currentUI = nowUI;
        currentUI.SetActive(true);
    }
}
