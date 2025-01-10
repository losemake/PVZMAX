using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class RangeButtet : BaseBullet 
    {
        private float range;

        public override void Init(MovementMode mode, GameObject sender, float range)
        {
            base.Init(mode, sender, range);
            this.transform.parent = sender.transform;
            this.range = range;
        }

        public void Attack()
        {
            RaycastHit2D[] Targets = RangeAttack(range, LayerMask.GetMask("Player"));

            foreach (RaycastHit2D target in Targets)
            {
                if (target.transform.gameObject == sender)
                    continue;
                Debug.Log(target.transform.name);
            }
        }
    }
}
