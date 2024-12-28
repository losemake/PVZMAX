namespace QFramework.PVZMAX
{
    public class SetBgmVolumeCommand : AbstractCommand 
    {
        private readonly float value;

        public SetBgmVolumeCommand(float value)
        {
            this.value = value;
        }
        protected override void OnExecute()
        {
            this.GetModel<AudioModel>().BgmVolume.Value = value;
            this.SendEvent<AudioBgmVolumeChangedEvent>();
        }
    }

    public class SetSfxVolumeCommand : AbstractCommand
    {
        private readonly float value;

        public SetSfxVolumeCommand(float value)
        {
            this.value = value;
        }
        protected override void OnExecute()
        {
            this.GetModel<AudioModel>().SfxVolume.Value = value;
            this.SendEvent<AudioSfxVolumeChangedEvent>();
        }
    }
}
