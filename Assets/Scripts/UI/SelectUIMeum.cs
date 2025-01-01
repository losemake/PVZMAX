using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QFramework.PVZMAX 
{
    /// <summary>
    /// 选择UI界面
    /// </summary>
    public class SelectUIMeum : UIGamePanal, IController
    {
        [Header("#UIPrefab")]
        public UIGamePanal SettingsUI;
        
        [Header("#Component")]
        public Button SettingsBtn;
        public Button BackBtn;
        public RectTransform ScrollBar_1P;
        public Image Avatar_Image_1P;
        public Image ScrollBar_Image_1P_1;
        public Image ScrollBar_Image_1P_2;

        public RectTransform ScrollBar_2P;
        public Image Avatar_Image_2P;
        public Image ScrollBar_Image_2P_1;
        public Image ScrollBar_Image_2P_2;

        [Header("#Resources")]
        public Sprite[] ScrollBarImages;
        public Sprite[] AvatarImages;

        [Header("#Date")]
        [SerializeField]
        private float mSpeed;

        void Awake()
        {
            if(SettingsBtn != null)
            {
                SettingsBtn.onClick.AddListener(() =>
                {
                    if (SettingsUI != null)
                    {
                        SettingsUI.OnShow();
                    }
                });
            }

            if(BackBtn != null)
            {
                BackBtn.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("StartScene");
                });
            }

            this.RegisterEvent<GameSelectEvent>(e =>
            {
                if(e.playerNum == PlayerNum.Player_1)
                {
                    Avatar_Image_1P.sprite = AvatarImages[(int)this.GetModel<GameModel>().PlantPrefab_1P.Value];
                    ScrollBar_Image_1P_1.sprite = ScrollBarImages[(int)this.GetModel<GameModel>().PlantPrefab_1P.Value];
                    ScrollBar_Image_1P_2.sprite = ScrollBarImages[(int)this.GetModel<GameModel>().PlantPrefab_1P.Value];
                }
                else if (e.playerNum == PlayerNum.Player_2)
                {
                    Avatar_Image_2P.sprite = AvatarImages[(int)this.GetModel<GameModel>().PlantPrefab_2P.Value];
                    ScrollBar_Image_2P_1.sprite = ScrollBarImages[(int)this.GetModel<GameModel>().PlantPrefab_2P.Value];
                    ScrollBar_Image_2P_2.sprite = ScrollBarImages[(int)this.GetModel<GameModel>().PlantPrefab_2P.Value];
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Update()
        {
            ScrollBarMove(ScrollBar_1P, 420.0f, 1260.0f);
            ScrollBarMove(ScrollBar_2P, 1500.0f, 660.0f);
        }

        /// <summary>
        /// 水平移动
        /// </summary>
        void ScrollBarMove(RectTransform scrollBar ,float startPos ,float endPos)
        {
            float direction = Mathf.Sign(startPos - endPos);

            if (Mathf.Sign(scrollBar.position.x - endPos) != direction)
            {
                scrollBar.Translate(new Vector2(startPos - endPos, 0.0f));
            }
            
            scrollBar.Translate(new Vector2(-direction * mSpeed * Time.deltaTime, 0.0f));
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}