using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class ElemEnergyBuff : BuffBase
    {
        private ElemType elemType;

        public void Init(GameObject owner, ElemType type)
        {
            elemType = type;
            base.Init(owner);
        }

        public override void OnAdd()
        {
            BasePlant plant = owner.GetComponent<BasePlant>();

            if (plant == null)
                return;

            plant.ChangedElemEnergy(elemType);
        }
    }
}

