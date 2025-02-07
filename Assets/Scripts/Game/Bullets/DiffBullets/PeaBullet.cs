using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX 
{
    public class PeaBullet : BaseBullet
    {
        [Header("#PeaBulletDate")]
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

            switch (this.type)
            {
                case ElemType.Wind:
                    curMoveSpeed = curMoveSpeed * 1.2f;
                    break;
                case ElemType.Ice:
                    curMoveSpeed = curMoveSpeed * 0.9f;
                    break;
            }
        }
        public override void Init(MovementMode mode, GameObject sender, Vector3 pos, Quaternion rot, ElemType type)
        {
            base.Init(mode, sender, pos, rot, type);
            isBreak = false;

            switch (this.type)
            {
                case ElemType.Wind:
                    curMoveSpeed = curMoveSpeed * 1.2f;
                    break;
                case ElemType.Ice:
                    curMoveSpeed = curMoveSpeed * 0.9f;
                    break;
            }
        }

        protected override void Move()
        {
            if (isBreak)
                return;

            base.Move();
        }
        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (!gameObject.activeSelf || !collision.CompareTag("Area"))
                return;
            
            Destroy();
        }
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                if (anim != null)
                {
                    anim.SetTrigger("Break");
                    AudioManager.instance.PlaySfx(SfxType.PeaBreak);
                    isBreak = true;
                }
            }
            else if (collision.CompareTag("Player"))
            {
                if (sender != collision.gameObject)
                {
                    if (anim != null)
                    {
                        anim.SetTrigger("Break");
                        isBreak = true;
                    }

                    BasePlant plant = collision.gameObject.GetComponent<BasePlant>();
                    if (plant != null)
                    {
                        plant.SetHealth(-damage);

                        Vector2 dir = collision.transform.position - transform.position;
                        dir.y = 0;
                        plant.Impulse(dir.normalized * force);

                        switch (this.type)
                        {
                            case ElemType.Wind:
                                plant.Impulse(dir.normalized * force);
                                break;
                            case ElemType.Water:
                                plant.ConsumeElemEnergy(plant.maxElemEnergy/4);
                                break;
                            case ElemType.Fire:
                                plant.SetHealth(-10);
                                break;
                            case ElemType.Thunder:
                                plant.SetEnergy(-30);
                                break;
                            case ElemType.Ice:
                                SpeedBuff buff = new SpeedBuff();
                                buff.Init(collision.gameObject, -2.0f);
                                BuffManager.instance.AddBuff(buff);
                                break;
                            case ElemType.Flame:
                                plant.SetHealth(-30);
                                break;
                        }
                    }

                    AudioManager.instance.PlaySfx(SfxType.PeaBreak);
                }
            }
        }
    }
}