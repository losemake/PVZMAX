using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX 
{
    public class NutBullet : BaseBullet
    {
        [Header("#NutBulletDate")]
        private float face;

        public override void Init(MovementMode mode, GameObject sender)
        {
            base.Init(mode, sender);
            face = sender.transform.forward.z;
            curMoveSpeed.x = 0.0f;
        }
        public override void Init(MovementMode mode, GameObject sender, Vector3 pos, Quaternion rot)
        {
            base.Init(mode, sender, pos, rot);
            face = sender.transform.forward.z;
            curMoveSpeed.x = 0.0f;
        }

        public override void Init(MovementMode mode, GameObject sender, ElemType type)
        {
            base.Init(mode, sender, type);
            face = sender.transform.forward.z;
            curMoveSpeed.x = 0.0f;

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
            face = sender.transform.forward.z;
            curMoveSpeed.x = 0.0f;

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

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (!gameObject.activeSelf)
                return;

            if (collision.CompareTag("Area"))
            {
                Destroy();
            }
            else if(collision.CompareTag("Platform"))
            {
                curMoveSpeed = Vector3.zero;
                curMoveSpeed.y = moveSpeed.y;
            }
        }
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                curMoveSpeed = Vector3.zero;
                curMoveSpeed.x = moveSpeed.x * face;
            }
            else if (collision.CompareTag("Player"))
            {
                if (sender != collision.gameObject)
                {
                    BasePlant plant = collision.gameObject.GetComponent<BasePlant>();
                    if (plant != null)
                    {
                        plant.SetHealth(-damage);

                        Vector2 dir = collision.transform.position - transform.position;
                        if(curMoveSpeed.x == 0) dir.x = 0;
                        if(curMoveSpeed.y == 0) dir.y = 0;
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
                                buff.Init(collision.gameObject, -2.0f);
                                BuffManager.instance.AddBuff(buff);
                                break;
                            case ElemType.Flame:
                                plant.SetHealth(-30);
                                break;
                        }
                    }
                    Destroy();
                }
            }
        }
    }
}

