using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace QFramework.PVZMAX 
{
    
    public enum PlantPrefabs
    {
        Peashooter,
        Sunflower,
        Gloomshroom,
        Nut,
        END
    }

    public enum GameMode 
    {
        START,
        SELECT,
        BATTLE
    }

    public enum PlayerNum 
    {
        Player_1,
        Player_2,
    }


    public class GameManager : MonoBehaviour, IController
    {
        public static GameManager instance;

        public BasePlant[] plants;
        public Transform playerInit_1P;
        public Transform playerInit_2P;
        void Awake()
        {
            //生成检查，防止多次生成
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            this.RegisterEvent<GameModeChangedEvent>(e =>
            {
                InitSelectModeInfo();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Start()
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }
        void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0)
            {
                this.SendCommand(new SetGameModeCommand(GameMode.START));
            }
            else if (scene.buildIndex == 1)
            {
                this.SendCommand(new SetGameModeCommand(GameMode.SELECT));
            }
            else if(scene.buildIndex == 2)
            {
                this.SendCommand(new SetGameModeCommand(GameMode.BATTLE));
            }
        }

        private void InitSelectModeInfo()
        {
            GameMode mode = this.GetModel<GameModel>().MGameMode.Value;
            if (mode == GameMode.SELECT)
            {
                this.SendCommand(new SelectPlantPrefabCommand(PlayerNum.Player_1, PlantPrefabs.Peashooter));
                this.SendCommand(new SelectPlantPrefabCommand(PlayerNum.Player_2, PlantPrefabs.Peashooter));
            }else if(mode == GameMode.BATTLE)
            {
                if (playerInit_1P == null) playerInit_1P = GameObject.Find("PlayerInit_1P").transform;
                if (playerInit_2P == null) playerInit_2P = GameObject.Find("PlayerInit_2P").transform;

                this.SendCommand(new BattleInitCommand());
            }
        }

        public BasePlant PlantInit(PlantPrefabs index, Vector3 pos, Quaternion rot)
        {
            return Instantiate(plants[(int)index], pos, rot);
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}


