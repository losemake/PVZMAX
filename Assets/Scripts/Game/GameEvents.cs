namespace QFramework.PVZMAX
{
    public struct GameModeChangedEvent { }
    

    public struct GameSelectEvent
    {
        public PlayerNum playerNum;

        public GameSelectEvent(PlayerNum playerNum)
        {
            this.playerNum = playerNum;
        }
    }

    public struct GameSelectConfirmEvent
    {
        public PlayerNum playerNum;

        public GameSelectConfirmEvent(PlayerNum playerNum)
        {
            this.playerNum = playerNum;
        }
    }

    public struct GameUIHeaderInit 
    {
        public PlayerNum playerNum;
        public PlantPrefabs plant;

        public GameUIHeaderInit(PlayerNum playerNum, PlantPrefabs plant)
        {
            this.playerNum = playerNum;
            this.plant = plant;
        }
    }

    public struct ChangeSceneStateEvent { }
    public struct ChangeHealthEvent { }
    public struct ChangeEnergyEvent { }
    public struct ChangeElemEnergyEvent { }
}