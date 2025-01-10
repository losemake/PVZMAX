using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace QFramework.PVZMAX
{   
    /// <summary>
    /// …Ë÷√UIΩÁ√Ê
    /// </summary>
    public class SettingsUIMeum : UIGamePanal, IController
    {
        [Header("#Component")]
        public Slider BgmSettingSlider;
        public Slider SfxSettingSlider;
        public Button CloseBtn;

        void Awake()
        {
            if (BgmSettingSlider != null)
            {
                BgmSettingSlider.onValueChanged.AddListener((value) =>
                {
                    this.SendCommand(new SetBgmVolumeCommand(value));
                });
            }

            if(SfxSettingSlider != null)
            {
                SfxSettingSlider.onValueChanged.AddListener((value) =>
                {
                    this.SendCommand(new SetSfxVolumeCommand(value));
                });
            }

            if(CloseBtn !=  null)
            {
                CloseBtn.onClick.AddListener(() =>
                {
                    AudioManager.instance.PlaySfx(SfxType.UISwitch);
                    OnHide();
                });
            }
        }

        protected override void OnOpen()
        {
            BgmSettingSlider.value = this.GetModel<AudioModel>().BgmVolume.Value;
            SfxSettingSlider.value = this.GetModel<AudioModel>().SfxVolume.Value;
        }

        public IArchitecture GetArchitecture()
        {
            return AudioApp.Interface;
        }
    }
}
