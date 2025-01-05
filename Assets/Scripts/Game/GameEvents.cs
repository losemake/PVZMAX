namespace QFramework.PVZMAX
{
    public struct GameModeChangedEvent
    {

    }

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
}