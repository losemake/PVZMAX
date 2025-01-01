namespace QFramework.PVZMAX
{
    public class GameApp : Architecture<GameApp>
    {
        protected override void Init()
        {
            //Êý¾Ý
            this.RegisterModel(new GameModel());
        }
    }
}
