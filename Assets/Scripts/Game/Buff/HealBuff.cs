using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class HealBuff : BuffBase
    {
        private float healValue = 30;
        
        public void Init(GameObject owner, float value)
        {
            healValue = value;
            base.Init(owner);
        }

        public override void OnAdd()
        {
            BasePlant plant = owner.GetComponent<BasePlant>();

            if (plant == null)
                return;

            plant.SetHealth(healValue);
        }
    }
}