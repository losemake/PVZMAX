using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX 
{
    public enum ActionDir
    {
        Left,
        Right, 
        Top, 
        Bottom,
    }
    public class GameKeyInputManager : MonoBehaviour, IController
    {
        void Update()
        {
            if(this.GetModel<GameModel>().MGameMode.Value == GameMode.SELECT)
            {
                if(Input.GetKeyDown(KeyCode.A))
                {
                    this.SendCommand(new ChangePlantPrefabCommand(PlayerNum.Player_1, ActionDir.Left));
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    this.SendCommand(new ChangePlantPrefabCommand(PlayerNum.Player_1, ActionDir.Right));
                }
                if(Input.GetKeyDown(KeyCode.LeftArrow)) 
                {
                    this.SendCommand(new ChangePlantPrefabCommand(PlayerNum.Player_2, ActionDir.Left));
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    this.SendCommand(new ChangePlantPrefabCommand(PlayerNum.Player_2, ActionDir.Right));
                }
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}
