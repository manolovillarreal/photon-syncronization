﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour{

    public static UIManager Instance = null;

    [SerializeField]
    private bool initUI = true;

    public GameObject PanelConnect;
    public GameObject LabelProgress;
    public GameObject PanelLobby;



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
    }
    public  void GoToLobby()
    {
        Clear();
        PanelLobby.SetActive(true);
    }
    public void ShowProgress()
    {
        Clear();
        LabelProgress.SetActive(true);
    }
    public void GotoRoom()
    {
        Clear();        
    }

}
