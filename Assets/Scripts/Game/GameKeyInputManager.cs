using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        void FixedUpdate()
        {
            GameMode mode = this.GetModel<GameModel>().MGameMode.Value;
            if (mode == GameMode.BATTLE)
            {
                if (GameManager.instance.GetSceneState() != SceneState.Game)
                    return;

                float dirPlayer1 = Input.GetAxis("HorizontalPlayer1");
                this.SendCommand(new BattlePlantMoveCommand(PlayerNum.Player_1, dirPlayer1));

                float dirPlayer2 = Input.GetAxis("HorizontalPlayer2");
                this.SendCommand(new BattlePlantMoveCommand(PlayerNum.Player_2, dirPlayer2));
            }
        }
        void Update()
        {
            GameMode mode = this.GetModel<GameModel>().MGameMode.Value;

            if (mode == GameMode.SELECT)
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

                if (Input.GetKeyDown(KeyCode.J))
                {
                    this.SendCommand(new SelectPlantConfirmCommand(PlayerNum.Player_1));
                }
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    this.SendCommand(new SelectPlantConfirmCommand(PlayerNum.Player_2));
                }
            }else if(mode == GameMode.BATTLE)
            {
                if (GameManager.instance.GetSceneState() != SceneState.Game)
                    return;

                // 跳跃指令
                if (Input.GetKeyDown(KeyCode.W))
                {
                    this.SendCommand(new BattlePlantJumpCommand(PlayerNum.Player_1));
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    this.SendCommand(new BattlePlantJumpCommand(PlayerNum.Player_2));
                }
                // 攻击指令
                if (Input.GetKeyDown(KeyCode.J))
                {
                    this.SendCommand(new BattlePlantFireCommand(PlayerNum.Player_1));
                }
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    this.SendCommand(new BattlePlantFireCommand(PlayerNum.Player_2));
                }
                // 技能指令
                if (Input.GetKeyDown(KeyCode.K))
                {
                    this.SendCommand(new BattlePlantSkillCommand(PlayerNum.Player_1));
                }
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    this.SendCommand(new BattlePlantSkillCommand(PlayerNum.Player_2));
                }
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}
