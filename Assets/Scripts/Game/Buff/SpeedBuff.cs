using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class SpeedBuff : BuffBase
    {
        private float speedValue;

        public override void Init(GameObject owner)
        {
            speedValue = 3.0f;
            durTime = 5.0f;
            base.Init(owner);
        }
        public void Init(GameObject owner, float value)
        {
            speedValue = value;
            durTime = 5.0f;
            base.Init(owner);
        }

        public override void OnAdd()
        {
            BasePlant plant = owner.GetComponent<BasePlant>();

            if (plant == null)
                return;

            plant.SetSpeed(speedValue);
        }

        public override void OnRemove()
        {
            BasePlant plant = owner.GetComponent<BasePlant>();

            if (plant == null)
                return;

            plant.SetSpeed(-speedValue);
        }
    }
}