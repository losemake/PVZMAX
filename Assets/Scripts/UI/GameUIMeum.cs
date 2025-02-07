using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace QFramework.PVZMAX
{
    public class GameUIMeum : UIGamePanal, IController
    {
        [Header("#Component")]
        public Image Header_P1;
        public Image Header_P2;

        public Slider Health_P1;
        public Slider Health_P2;

        public Slider Energy_P1;
        public Slider Energy_P2;

        public Slider ElemEnergy_P1;
        public Slider ElemEnergy_P2;

        public Image ElemEnergyImageBg_P1;
        public Image ElemEnergyImage_P1;

        public Image ElemEnergyImageBg_P2;
        public Image ElemEnergyImage_P2;

        private SceneState state;

        public Image BackGround;
        public Text CountDown;
        private int time;

        public Image Win_P1;
        public Image Win_P2;
        public Text Tie;

        public Text Time;

        [Header("#Resources")]
        public Sprite[] HeaderImages;
        public Color[] ElemEnergyColor;
        public Sprite[] ElemEnergyImages;

        void Awake()
        {
            this.RegisterEvent<GameUIHeaderInit>(e => {
                if(e.playerNum == PlayerNum.Player_1)
                {
                    Header_P1.sprite = HeaderImages[(int)e.plant];
                }

                if(e.playerNum == PlayerNum.Player_2)
                {
                    Header_P2.sprite = HeaderImages[(int)e.plant];
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<ChangeHealthEvent>(e => {
                if (Health_P1 != null)
                {
                    Health_P1.value = this.GetModel<GameModel>().Player1.GetHealthPer();
                }
                if(Health_P2 != null)
                {
                    Health_P2.value = this.GetModel<GameModel>().Player2.GetHealthPer();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<ChangeEnergyEvent>(e => {
                if (Energy_P1 != null)
                {
                    Energy_P1.value = this.GetModel<GameModel>().Player1.GetEnergyPer();
                }
                if (Energy_P2 != null)
                {
                    Energy_P2.value = this.GetModel<GameModel>().Player2.GetEnergyPer();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<ChangeElemEnergyEvent>(e => {
                if (ElemEnergy_P1 != null)
                {
                    ElemEnergy_P1.value = this.GetModel<GameModel>().Player1.GetElemEnergyPer();

                    if(this.GetModel<GameModel>().Player1.elemType != ElemType.None)
                    {
                        ElemEnergy_P1.fillRect.GetComponent<Image>().color = ElemEnergyColor[(int)this.GetModel<GameModel>().Player1.elemType - 1];

                        if (ElemEnergyImageBg_P1 != null) ElemEnergyImageBg_P1.gameObject.SetActive(true);
                        if (ElemEnergyImage_P1 != null) ElemEnergyImage_P1.sprite = ElemEnergyImages[(int)this.GetModel<GameModel>().Player1.elemType - 1];
                    }
                    else
                    {
                        if (ElemEnergyImageBg_P1 != null) ElemEnergyImageBg_P1.gameObject.SetActive(false);
                    }
                }
                if (ElemEnergy_P2 != null)
                {
                    ElemEnergy_P2.value = this.GetModel<GameModel>().Player2.GetElemEnergyPer();

                    if (this.GetModel<GameModel>().Player2.elemType != ElemType.None)
                    {
                        ElemEnergy_P2.fillRect.GetComponent<Image>().color = ElemEnergyColor[(int)this.GetModel<GameModel>().Player2.elemType - 1];

                        if (ElemEnergyImageBg_P1 != null) ElemEnergyImageBg_P1.gameObject.SetActive(true);
                        if (ElemEnergyImage_P2 != null) ElemEnergyImage_P2.sprite = ElemEnergyImages[(int)this.GetModel<GameModel>().Player2.elemType - 1];
                    }
                    else
                    {
                        if (ElemEnergyImageBg_P2 != null) ElemEnergyImageBg_P2.gameObject.SetActive(false);
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<ChangeSceneStateEvent>(e => {
                
                SetScene(GameManager.instance.GetSceneState());
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void LateUpdate()
        {
            float remainTime = Mathf.Max(0, GameManager.instance.maxGameTime - GameManager.instance.gameTime);
            int min = Mathf.FloorToInt(remainTime / 60);
            int sec = Mathf.FloorToInt(remainTime % 60);

            Time.text = string.Format("{0:D2}:{1:D2}", min, sec);
        }

        public SceneState GetState()
        {
            return state;
        }

        public void SetScene(SceneState state)
        {
            this.state = state;

            switch (state)
            {
                case SceneState.Start:

                    if (BackGround != null) BackGround.gameObject.SetActive(true);
                    if (CountDown != null) CountDown.gameObject.SetActive(true);
                    if (Win_P1 != null) Win_P1.gameObject.SetActive(false);
                    if (Win_P2 != null) Win_P2.gameObject.SetActive(false);
                    if (Tie != null) Tie.gameObject.SetActive(false);

                    time = 4;
                    InvokeRepeating("CountDownFn", 0.0f, 1.0f);

                    break;

                case SceneState.Game:

                    if (BackGround != null) BackGround.gameObject.SetActive(false);

                    CancelInvoke("CountDownFn");

                    break;

                case SceneState.End:

                    if (BackGround != null) BackGround.gameObject.SetActive(true);
                    if (CountDown != null) CountDown.gameObject.SetActive(false);
                    if (Win_P1 != null) Win_P1.gameObject.SetActive(false);
                    if (Win_P2 != null) Win_P2.gameObject.SetActive(false);
                    if (Tie != null) Tie.gameObject.SetActive(false);

                    float HealthP1 = this.GetModel<GameModel>().Player1.currentHealth;
                    float HealthP2 = this.GetModel<GameModel>().Player2.currentHealth;
                    
                    if(HealthP1 == HealthP2)
                    {
                        if (Tie != null) Tie.gameObject.SetActive(true);
                    }else if(HealthP1 > HealthP2)
                    {
                        if (Win_P1 != null) Win_P1.gameObject.SetActive(true);
                    }else
                    {
                        if (Win_P2 != null) Win_P2.gameObject.SetActive(true);
                    }

                    AudioManager.instance.PlaySfx(SfxType.UIWin);

                    Invoke("AgainGame", 3.5f);

                    break;
            }
        }

        public void CountDownFn()
        {
            time--;

            if (time > 0)
            {
                CountDown.text = time.ToString();
            }
            else
            {
                CountDown.text = "¿ªÊ¼";
            }
        }

        public void AgainGame()
        {
            this.SendCommand(new LoadScene("SelectScene"));
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}