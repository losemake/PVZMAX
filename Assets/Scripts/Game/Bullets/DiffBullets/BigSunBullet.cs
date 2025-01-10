using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX 
{
    public class BigSunBullet : BaseBullet
    {
        [Header("#BigSunBulletDate")]
        public float range;
        private bool isBreak;

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
                    isBreak = true;
                }
                AudioManager.instance.PlaySfx(SfxType.SunExplodeEx);
            }
            else if (collision.CompareTag("Player"))
            {
                if (sender != collision.gameObject)
                {
                    Debug.Log("碰撞玩家");
                    if (anim != null)
                    {
                        anim.SetTrigger("Break");
                        isBreak = true;
                    }
                    AudioManager.instance.PlaySfx(SfxType.SunExplodeEx);
                }
            }
        }
    }
}
