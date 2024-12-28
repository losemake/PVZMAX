namespace QFramework.PVZMAX 
{
    public class AudioApp : Architecture<AudioApp>
    {
        protected override void Init()
        {
            // ����
            this.RegisterUtility<IStorage>(new Storage());

            //����
            this.RegisterModel(new AudioModel());
        }
    }
}
