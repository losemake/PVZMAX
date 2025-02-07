using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public enum BuffName 
    {
        HealBuff,
        EnergyBuff,
        SpeedBuff,
    }

    /// <summary>
    /// buff管理类
    /// </summary>
    public class BuffManager : MonoBehaviour
    {
        public static BuffManager instance;

        public List<BuffBase> m_Buffs = new List<BuffBase>();
        private float m_Timer;

        void Awake()
        {
            //生成检查，防止多次生成
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnEnable()
        {
            m_Timer = -1;
        }

        /// <summary>
        /// 添加buff
        /// </summary>
        public void AddBuff(BuffBase buff)
        {
            if(buff.durTime == 0.0f)
            {
                buff.OnAdd();
                
                return;
            }

            if(m_Buffs.Count == 0) 
            {
                m_Buffs.Add(buff);
                m_Timer = m_Buffs[0].GetOverTime();

                buff.OnAdd();
                
                return;
            }

            BuffBase oldbuff = m_Buffs.Find(comBuff => comBuff.CompareEqualBuff(buff));

            if(oldbuff != null)
            {
                m_Buffs.Remove(oldbuff);
                oldbuff.RepeatedGetBuff();
                int index = LastLAEValueIndex(buff) + 1;
                m_Buffs.Insert(index, buff);
            }
            else
            {
                int index = LastLAEValueIndex(buff)+1;
                m_Buffs.Insert(index, buff);
                buff.OnAdd();
            }

            m_Timer = m_Buffs[0].GetOverTime();
        }

        private void Update()
        {
            ReFreshBuff();

            OnTimer();
        }

        public void ReFreshBuff()
        {
            for (int i = m_Buffs.Count - 1; i >= 0; i--)
            {
                m_Buffs[i].OnUpdate();
            }
        }

        public void OnTimer()
        {
            if (m_Timer == -1) return;

            if(m_Timer <= GameManager.instance.gameTime)
            {
                RemoveBuff();
            }
        }

        public void RemoveBuff()
        {
            while (m_Buffs.Count != 0 && m_Buffs[0].GetOverTime() <= GameManager.instance.gameTime)
            {
                m_Buffs[0].OnRemove();
                m_Buffs.RemoveAt(0);
            }

            if (m_Buffs.Count == 0)
                m_Timer -= 1;
            else
                m_Timer = m_Buffs[0].GetOverTime();
        }
        private int LastLAEValueIndex(BuffBase buff)
        {
            int l = 0, r = m_Buffs.Count - 1;
            while (l <= r)
            {
                int mid = l + (r - l) / 2;
                if (m_Buffs[mid].GetOverTime() > buff.GetOverTime())  //关键点,>
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid + 1;
                }
            }

            if (r < 0) return -1;

            return r;
        }

    }
}