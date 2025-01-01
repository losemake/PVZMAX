namespace QFramework.PVZMAX
{
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
            }else if(num == PlayerNum.Player_2)
            {
                this.GetModel<GameModel>().PlantPrefab_1P.Value = plantNum;
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
            if (num == PlayerNum.Player_1)
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
            else if (num == PlayerNum.Player_2)
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
}