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
}