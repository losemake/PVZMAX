using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class PinkBuff : RefreshObject
    {
        [Header("#BuffDate")]
        private HealBuff buff = new HealBuff();

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                buff.Init(other.gameObject);
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