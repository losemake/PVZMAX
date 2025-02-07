using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX 
{
    public class SunBullet : BaseBullet
    {
        [Header("#SunBulletDate")]
        public float range;
        private bool isBreak;

        public override void Init(MovementMode mode, GameObject sender)
        {
            base.Init(mode, sender);
            isBreak = false;
        }
        public override void Init(MovementMode mode, GameObject sender, Vector3 pos, Quaternion rot)
        {
            base.Init(mode, sender, pos, rot);
            isBreak = false;
        }

        public override void Init(MovementMode mode, GameObject sender, ElemType type)
        {
            base.Init(mode, sender, type);
            isBreak = false;
        }
        public override void Init(MovementMode mode, GameObject sender, Vector3 pos, Quaternion rot, ElemType type)
        {
            base.Init(mode, sender, pos, rot, type);
            isBreak = false;
        }

        protected override void Move()
        {
            if (isBreak)
                return;

            base.Move();
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
                            plant.SetHealth(-10);
                            break;
                        case ElemType.Thunder:
                            plant.SetEnergy(-30);
                            break;
                        case ElemType.Ice:
                            SpeedBuff buff = new SpeedBuff();
                            buff.Init(plant.gameObject, -2.0f);
                            BuffManager.instance.AddBuff(buff);
                            break;
                        case ElemType.Flame:
                            plant.SetHealth(-30);
                            break;
                    }
                }
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (!gameObject.activeSelf || !collision.CompareTag("Area"))
                return;

            Destroy();
        }
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (sender != collision.gameObject)
                {
                    if (anim != null)
                    {
                        anim.SetTrigger("Break");
                        isBreak = true;
                    }
                    AudioManager.instance.PlaySfx(SfxType.SunExplode);
                }
            }
        }
    }
}