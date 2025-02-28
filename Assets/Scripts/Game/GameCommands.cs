
using System.Diagnostics;
using UnityEngine.SceneManagement;

namespace QFramework.PVZMAX
{
    public class ChangeBgmCommand : AbstractCommand
    {
        private readonly BgmType bgmType;

        public ChangeBgmCommand(BgmType Type)
        {
            this.bgmType = Type;
        }

        protected override void OnExecute()
        {
            AudioManager.instance.SetBgm(bgmType);
        }
    }
    public class LoadScene : AbstractCommand 
    {
        private readonly string name;

        public LoadScene(string name)
        {
            this.name = name;
        }
        protected override void OnExecute()
        {
            SceneManager.LoadScene(name);
        }
    }

    public class SetGameModeCommand : AbstractCommand 
    {
        private readonly GameMode mode;

        public SetGameModeCommand(GameMode mode)
        {
            this.mode = mode;
        }
        protected override void OnExecute()
        {
            this.GetModel<GameModel>().MGameMode.Value = mode;
            this.SendEvent<GameModeChangedEvent>();
        }
    }

    public class SelectPlantPrefabCommand : AbstractCommand
    {
        private readonly PlayerNum num;
        private readonly PlantPrefabs plantNum;

        public SelectPlantPrefabCommand(PlayerNum num , PlantPrefabs plantNum)
        {
            this.num = num;
            this.plantNum = plantNum;
        }
        protected override void OnExecute()
        {
            if(num == PlayerNum.Player_1)
            {
                this.GetModel<GameModel>().PlantPrefab_1P.Value = plantNum;
                this.GetModel<GameModel>().Player1_Confirm.Value = false;
            }else if(num == PlayerNum.Player_2)
            {
                this.GetModel<GameModel>().PlantPrefab_2P.Value = plantNum;
                this.GetModel<GameModel>().Player2_Confirm.Value = false;
            }

            this.SendEvent(new GameSelectEvent(num));
        }
    }

    public class ChangePlantPrefabCommand : AbstractCommand
    {
        private readonly PlayerNum num;
        private readonly ActionDir dir;

        public ChangePlantPrefabCommand(PlayerNum num, ActionDir dir)
        {
            this.num = num;
            this.dir = dir;
        }
        protected override void OnExecute()
        {
            int prefabNum = (int)PlantPrefabs.END;
            if (num == PlayerNum.Player_1 && !this.GetModel<GameModel>().Player1_Confirm.Value)
            {
                if(dir == ActionDir.Left)
                {
                    int plantId = (int)this.GetModel<GameModel>().PlantPrefab_1P.Value;
                    plantId = (plantId + 4 - 1) % 4;
                    this.GetModel<GameModel>().PlantPrefab_1P.Value = (PlantPrefabs)plantId;
                }
                else if(dir == ActionDir.Right)
                {
                    int plantId = (int)this.GetModel<GameModel>().PlantPrefab_1P.Value;
                    plantId = (plantId + 1) % 4;
                    this.GetModel<GameModel>().PlantPrefab_1P.Value = (PlantPrefabs)plantId;
                }
            }
            else if (num == PlayerNum.Player_2 && !this.GetModel<GameModel>().Player2_Confirm.Value)
            {
                if (dir == ActionDir.Left)
                {
                    int plantId = (int)this.GetModel<GameModel>().PlantPrefab_2P.Value;
                    plantId = (plantId + prefabNum - 1) % prefabNum;
                    this.GetModel<GameModel>().PlantPrefab_2P.Value = (PlantPrefabs)plantId;
                }
                else if (dir == ActionDir.Right)
                {
                    int plantId = (int)this.GetModel<GameModel>().PlantPrefab_2P.Value;
                    plantId = (plantId + 1) % prefabNum;
                    this.GetModel<GameModel>().PlantPrefab_2P.Value = (PlantPrefabs)plantId;
                }
            }

            this.SendEvent(new GameSelectEvent(num));
        }
    }

    public class SelectPlantConfirmCommand : AbstractCommand
    {
        private readonly PlayerNum num;
        public SelectPlantConfirmCommand(PlayerNum num)
        {
            this.num = num;
        }
        protected override void OnExecute()
        {
            if (num == PlayerNum.Player_1)
            {
                this.GetModel<GameModel>().Player1_Confirm.Value = true;
            }
            else if (num == PlayerNum.Player_2)
            {
                this.GetModel<GameModel>().Player2_Confirm.Value = true;
            }

            this.SendEvent(new GameSelectConfirmEvent(num));
        }
    }

    public class BattleInitCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<GameModel>().Player1 = GameManager.instance.PlantInit(this.GetModel<GameModel>().PlantPrefab_1P.Value, 
                                                 GameManager.instance.playerInit_1P.position, GameManager.instance.playerInit_1P.rotation);
            this.GetModel<GameModel>().Player2 = GameManager.instance.PlantInit(this.GetModel<GameModel>().PlantPrefab_2P.Value,
                                                 GameManager.instance.playerInit_2P.position, GameManager.instance.playerInit_2P.rotation);


            this.SendEvent(new GameUIHeaderInit(PlayerNum.Player_1, this.GetModel<GameModel>().PlantPrefab_1P.Value));
            this.SendEvent(new GameUIHeaderInit(PlayerNum.Player_2, this.GetModel<GameModel>().PlantPrefab_2P.Value));

            this.SendEvent<ChangeHealthEvent>();
            this.SendEvent<ChangeEnergyEvent>();
            this.SendEvent<ChangeElemEnergyEvent>();
        }
    }

    public class BattlePlantMoveCommand : AbstractCommand
    {
        private readonly PlayerNum num;
        private readonly float dir;

        public BattlePlantMoveCommand(PlayerNum num, float dir)
        {
            this.num = num;
            this.dir = dir;
        }
        protected override void OnExecute()
        {
            if (num == PlayerNum.Player_1)
            {
                this.GetModel<GameModel>().Player1.Move(dir);
                this.GetModel<GameModel>().Player1.TurnTo(dir);
            }else if (num == PlayerNum.Player_2)
            {
                this.GetModel<GameModel>().Player2.Move(dir);
                this.GetModel<GameModel>().Player2.TurnTo(dir);
            }
        }
    }

    public class BattlePlantJumpCommand : AbstractCommand
    {
        private readonly PlayerNum num;

        public BattlePlantJumpCommand(PlayerNum num)
        {
            this.num = num;
        }
        protected override void OnExecute()
        {
            if (num == PlayerNum.Player_1)
            {
                this.GetModel<GameModel>().Player1.Jump();
            }
            else if (num == PlayerNum.Player_2)
            {
                this.GetModel<GameModel>().Player2.Jump();
            }
        }
    }

    public class BattlePlantFireCommand : AbstractCommand
    {
        private readonly PlayerNum num;

        public BattlePlantFireCommand(PlayerNum num)
        {
            this.num = num;
        }
        protected override void OnExecute()
        {
            if (num == PlayerNum.Player_1)
            {
                this.GetModel<GameModel>().Player1.Fire();
            }
            else if (num == PlayerNum.Player_2)
            {
                this.GetModel<GameModel>().Player2.Fire();
            }
        }
    }

    public class BattlePlantSkillCommand : AbstractCommand
    {
        private readonly PlayerNum num;

        public BattlePlantSkillCommand(PlayerNum num)
        {
            this.num = num;
        }
        protected override void OnExecute()
        {
            if (num == PlayerNum.Player_1)
            {
                this.GetModel<GameModel>().Player1.Skill();
            }
            else if (num == PlayerNum.Player_2)
            {
                this.GetModel<GameModel>().Player2.Skill();
            }
        }
    }

    public class SceneStateChangeCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<ChangeSceneStateEvent>();
        }
    }
}