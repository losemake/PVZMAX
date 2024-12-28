using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    /// <summary>
    /// 数据本地存储(Unity类)
    /// </summary>
    public interface IStorage : IUtility 
    {
        void SaveInt(string key, int value);
        int LoadInt(string key, int defaultValue);
        void SaveFloat(string key, float value);
        float LoadFloat(string key, float defaultValue);
    }

    public class Storage : IStorage
    {
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public float LoadFloat(string key, float defaultValue)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
    }

}