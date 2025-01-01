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
        }

        private void InitSelectModeInfo()
        {
            if(this.GetModel<GameModel>().MGameMode.Value == GameMode.SELECT)
            {
                this.SendCommand(new SelectPlantPrefabCommand(PlayerNum.Player_1, PlantPrefabs.Peashooter));
                this.SendCommand(new SelectPlantPrefabCommand(PlayerNum.Player_2, PlantPrefabs.Peashooter));
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}


