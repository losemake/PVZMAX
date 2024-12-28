using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.PVZMAX
{
    /// <summary>
    /// 音频管理
    /// </summary>
    public class AudioManager : MonoBehaviour , IController
    {
        public static AudioManager instance;

        [Header("#BGM")]
        public AudioClip bgmClip;
        AudioSource bgmPlayer;

        [Header("#SFX")]
        public AudioClip[] sfxClips;
        AudioSource[] sfxPlayers;
        public int channels;        // 声道数
        public int channelIndex;           // 声道索引（加速检索空闲的声道）
        
        [Header("#Model")]
        private AudioModel mAudioModel;
        
        [Header("#Component")]
        public Scrollbar bar;
        public Scrollbar bar2;

        public enum SfxType { Dead, Hit, Levelup = 3, lose, Melee, Range = 7, Select, Win }
        void Awake()
        {
            instance = this;

            mAudioModel = this.GetModel<AudioModel>();

            Init();

            bar.value = mAudioModel.BgmVolume.Value;

            bar.onValueChanged.AddListener((value) =>
            {
                this.SendCommand(new SetBgmVolumeCommand(value));
            });

            bar2.value = mAudioModel.SfxVolume.Value;

            bar2.onValueChanged.AddListener((value) =>
            {
                this.SendCommand(new SetSfxVolumeCommand(value));
            });

            this.RegisterEvent<AudioBgmVolumeChangedEvent>(e =>
            {
                SetBgmVolume();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<AudioSfxVolumeChangedEvent>(e =>
            {
                SetSfxVolume();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            PlayBgm(true);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.W)) 
            {
                
                PlaySfx(SfxType.Dead);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlaySfx(SfxType.Hit);
            }
        }

        private void Init()
        {
            GameObject bgm = new GameObject("BgmPlayer");
            bgm.transform.parent = transform;
            bgmPlayer = bgm.AddComponent<AudioSource>();
            bgmPlayer.playOnAwake = false;                      //循环开关
            bgmPlayer.loop = true;
            bgmPlayer.volume = mAudioModel.BgmVolume.Value;
            bgmPlayer.clip = bgmClip;

            GameObject sfx = new GameObject("SfxPlayer");
            sfx.transform.parent = transform;
            sfxPlayers = new AudioSource[channels];

            for (int i = 0; i < sfxPlayers.Length; i++)
            {
                sfxPlayers[i] = sfx.AddComponent<AudioSource>();
                sfxPlayers[i].playOnAwake = false;
                sfxPlayers[i].volume = mAudioModel.SfxVolume.Value;
            }
        }

        public void SetBgmVolume()
        {
            bgmPlayer.volume = mAudioModel.BgmVolume.Value;
        }
        public void PlayBgm(bool isPlay)
        {
            if (isPlay)
            {
                bgmPlayer.Play();
            }
            else
            {
                bgmPlayer.Stop();
            }
        }

        public void SetSfxVolume()
        {
            for (int i = 0; i < sfxPlayers.Length; i++)
            {
                sfxPlayers[i].volume = mAudioModel.SfxVolume.Value;
            }
        }
        public void PlaySfx(SfxType type)
        {
            for (int i = 0; i < sfxPlayers.Length; i++)
            {
                int loopIndex = (i + channelIndex) % sfxPlayers.Length;

                if (sfxPlayers[loopIndex].isPlaying)
                    continue;

                int ranIndex = 0;
                /*if (type == SfxType.Hit || type == SfxType.Melee)
                {
                    ranIndex = Random.Range(0, 2);
                }*/

                channelIndex = loopIndex;
                Debug.Log(channelIndex);
                sfxPlayers[loopIndex].clip = sfxClips[(int)type + ranIndex];
                sfxPlayers[loopIndex].Play();
                break;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return AudioApp.Interface;
        }
    }
}