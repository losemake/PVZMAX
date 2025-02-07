using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class RefreshManager : MonoBehaviour
    {
        private Transform[] spawnPoints;

        private float spawnTime = 20.0f;
        void Awake()
        {
            spawnPoints = GetComponentsInChildren<Transform>();
        }

        void OnEnable()
        {
            InvokeRepeating("FallingSpawn", spawnTime / 2, spawnTime);
            InvokeRepeating("PlatformSpawn", spawnTime, spawnTime);
        }

        void OnDestroy()
        {
            CancelInvoke("FallingSpawn");
        }
        private void FallingSpawn()
        {
            GameObject fallingObject = PoolManager.instance.Get(Random.Range((int)PerfabsName.BlueBuff, (int)PerfabsName.YellowBuff + 1));
            fallingObject.GetComponent<RefreshObject>().Init(spawnPoints[Random.Range(1, 4)].position);
        }

        private void PlatformSpawn()
        {
            GameObject fallingObject = PoolManager.instance.Get(Random.Range((int)PerfabsName.WindBuff, (int)PerfabsName.ThunderBuff + 1));
            fallingObject.GetComponent<RefreshObject>().Init(spawnPoints[Random.Range(4, 6)].position);
        }
    }
}