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
        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (!gameObject.active)
                return;

            if (collision.CompareTag("Area"))
            {
                Debug.Log("离开屏幕外");
                Destroy();
            }
            else if(collision.CompareTag("Platform"))
            {
                Debug.Log("离开平台");
                curMoveSpeed = Vector3.zero;
                curMoveSpeed.y = moveSpeed.y;
            }
        }
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                Debug.Log("碰撞平台");
                curMoveSpeed = Vector3.zero;
                curMoveSpeed.x = moveSpeed.x * face;
            }
            else if (collision.CompareTag("Player"))
            {
                if (sender != collision.gameObject)
                {
                    Debug.Log("碰撞玩家");
                    Destroy();
                }
            }
        }
    }
}

