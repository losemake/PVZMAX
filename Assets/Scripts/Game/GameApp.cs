namespace QFramework.PVZMAX
{
    public class GameApp : Architecture<GameApp>
    {
        protected override void Init()
        {
            //����
            this.RegisterModel(new GameModel());
        }
    }
}
