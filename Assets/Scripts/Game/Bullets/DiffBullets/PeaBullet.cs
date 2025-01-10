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

        protected override void Move()
        {
            if (isBreak)
                return;

            base.Move();
        }
        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (!gameObject.active || !collision.CompareTag("Area"))
                return;
            Debug.Log("离开屏幕外");
            Destroy();
        }
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                Debug.Log("碰撞平台");
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
                    Debug.Log("碰撞玩家");
                    if (anim != null)
                    {
                        anim.SetTrigger("Break");
                        AudioManager.instance.PlaySfx(SfxType.PeaBreak);
                        isBreak = true;
                    }
                }
            }
        }
    }
}