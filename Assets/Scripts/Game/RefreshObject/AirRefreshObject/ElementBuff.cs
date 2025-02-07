using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class ElemmentBuff : RefreshObject
    {
        [Header("#BuffDate")]
        private ElemEnergyBuff buff = new ElemEnergyBuff();
        [SerializeField] private ElemType type;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                buff.Init(other.gameObject, type);
                BuffManager.instance.AddBuff(buff);

                Destroy();
            }
            else if (other.CompareTag("Platform"))
            {
                mode = MovementMode.None;
            }
        }
    }
}