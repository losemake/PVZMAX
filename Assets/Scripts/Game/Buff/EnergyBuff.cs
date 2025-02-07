using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class EnergyBuff : BuffBase
    {
        private float energyValue = 30;

        public void Init(GameObject owner, float value)
        {
            energyValue = value;
            base.Init(owner);
        }

        public override void OnAdd()
        {
            BasePlant plant = owner.GetComponent<BasePlant>();

            if (plant == null)
                return;

            plant.SetEnergy(energyValue);
        }
    }
}

