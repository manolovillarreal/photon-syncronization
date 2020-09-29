using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{

    public static UIManager Instance = null;

    [SerializeField]
    private bool initUI = true;

    public GameObject PanelConnect;
    public GameObject LabelProgress;
    public GameObject PanelLobby;
    public GameObject PanelMatch;
    public GameObject ButtonCancel;



    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        if (initUI)
        {
            Clear();
            PanelConnect.SetActive(true);            
        }
    }

    void Clear()
    {
        PanelConnect.SetActive(false);
        LabelProgress.SetActive(false);
        PanelLobby.SetActive(false);
        PanelMatch.SetActive(false);
        ButtonCancel.SetActive(false);
    }

    public   void Connecting()
    {
        Clear();
        ShowProgress("Connecting . . .");
    }
    public  void GoToLobby()
    {
        Clear();
        PanelLobby.SetActive(true);
    }
    public void ShowProgress(string text)
    {
        LabelProgress.GetComponent<Text>().text = text;
        LabelProgress.SetActive(true);
    }
    public void GotoRoom()
    {
        Clear();
        ShowProgress("Looking for Match . . .");
        ButtonCancel.SetActive(true);
    }
    public void ShowMatchPanel()
    {
        Clear();
        PanelMatch.SetActive(true);
    }

}
