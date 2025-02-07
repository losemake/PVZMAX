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

        public override void Init(MovementMode mode, GameObject sender, float range, ElemType type)
        {
            base.Init(mode, sender, range, type);
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

                BasePlant plant = target.transform.gameObject.GetComponent<BasePlant>();
                if (plant != null)
                {
                    plant.SetHealth(-damage);

                    Vector2 dir = target.transform.position - transform.position;
                    plant.Impulse(dir.normalized * force);

                    switch (this.type)
                    {
                        case ElemType.Wind:
                            plant.Impulse(dir.normalized * force);
                            break;
                        case ElemType.Water:
                            plant.ConsumeElemEnergy(plant.maxElemEnergy / 4);
                            break;
                        case ElemType.Fire:
                            plant.SetHealth(-5);
                            break;
                        case ElemType.Thunder:
                            plant.SetEnergy(-10);
                            break;
                        case ElemType.Ice:
                            SpeedBuff buff = new SpeedBuff();
                            buff.Init(plant.gameObject, -2.0f);
                            BuffManager.instance.AddBuff(buff);
                            break;
                        case ElemType.Flame:
                            plant.SetHealth(-15);
                            break;
                    }
                }
            }
        }
    }
}
