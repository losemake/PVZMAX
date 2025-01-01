using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.PVZMAX
{
    /// <summary>
    /// ��Ч����
    /// </summary>
    public enum SfxType 
    { 
        Dead, 
        Hit, 
        Levelup = 3, 
        lose, 
        Melee, 
        Range = 7, 
        Select, 
        Win 
    }
    /// <summary>
    /// ��Ƶ����
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
        public int channels;                // ������
        public int channelIndex;            // �������������ټ������е�������
        
        [Header("#Model")]
        private AudioModel mAudioModel;

        void Awake()
        {
            //���ɼ�飬��ֹ�������
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            

            mAudioModel = this.GetModel<AudioModel>();

            Init();

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

        private void Init()
        {
            GameObject bgm = new GameObject("BgmPlayer");
            bgm.transform.parent = transform;
            bgmPlayer = bgm.AddComponent<AudioSource>();
            bgmPlayer.playOnAwake = false;                      //ѭ������
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