namespace QFramework.PVZMAX 
{
    public class AudioApp : Architecture<AudioApp>
    {
        protected override void Init()
        {
            // 工具
            this.RegisterUtility<IStorage>(new Storage());

            //数据
            this.RegisterModel(new AudioModel());
        }
    }
}
