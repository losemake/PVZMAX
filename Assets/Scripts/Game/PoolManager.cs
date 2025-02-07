using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public enum PerfabsName 
    {
        PeaBullet,
        Sun,
        BigSun,
        Bubbles,
        BigBubbles,
        NutBullet,
        NutExplode,
        RunFx,
        JumpFx,
        FallFx,
        SunTextFx,
        BlueBuff,
        PinkBuff,
        YellowBuff,
        WindBuff,
        WaterBuff,
        FireBuff,
        ThunderBuff,
        IceBuff,
        FlameBuff,
    }

    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;

        public GameObject[] prefabs;
        List<GameObject>[] pools;


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            pools = new List<GameObject>[prefabs.Length];

            for (int i = 0; i < pools.Length; i++)
            {
                pools[i] = new List<GameObject>();
            }
        }

        public GameObject Get(int index)
        {
            GameObject select = null;

            foreach (GameObject pref in pools[index])
            {
                if (!pref.activeSelf)
                {
                    select = pref;
                    select.SetActive(true);
                    break;
                }
            }

            if (select == null)
            {
                select = Instantiate(prefabs[index], transform);
                pools[index].Add(select);
            }

            return select;
        }
    }

}