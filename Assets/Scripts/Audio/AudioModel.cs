namespace QFramework.PVZMAX
{
    /// <summary>
    /// 音乐和音效的数据
    /// </summary>
    public class AudioModel : AbstractModel 
    {
        public BindableProperty<float> BgmVolume = new BindableProperty<float>(0.0f);
        public BindableProperty<float> SfxVolume = new BindableProperty<float>(0.0f);

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();

            BgmVolume.Value = storage.LoadFloat(nameof(BgmVolume), 0);
            BgmVolume.RegisterWithInitValue(value =>
            {
                storage.SaveFloat(nameof(BgmVolume), value);
            });

            SfxVolume.Value = storage.LoadFloat(nameof(SfxVolume), 0);
            SfxVolume.RegisterWithInitValue(value =>
            {
                storage.SaveFloat(nameof(SfxVolume), value);
            });
        }
    }
}
