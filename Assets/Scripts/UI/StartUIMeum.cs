using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QFramework.PVZMAX
{
    /// <summary>
    /// 开始UI界面
    /// </summary>
    public class StartUIMeum : UIGamePanal, IController
    {
        [Header("#UIPrefab")]
        public UIGamePanal SettingsUI;

        [Header("#Component")]
        public Button LocalPlayBtn;
        public Button OnlinePlayBtn;
        public Button SettingsBtn;

        void Awake()
        {
            if (LocalPlayBtn != null)
            {
                LocalPlayBtn.onClick.AddListener(() =>
                {
                    this.SendCommand(new LoadScene("SelectScene"));
                });
            }

            if (OnlinePlayBtn != null)
            {
                OnlinePlayBtn.onClick.AddListener(() =>
                {
                    Debug.Log("联机游玩开启");
                });
            }

            if (SettingsBtn != null)
            {
                SettingsBtn.onClick.AddListener(() =>
                {
                    if(SettingsUI != null)
                    {
                        SettingsUI.OnShow();
                    }
                });
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}