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

                BasePlant plant = target.transform.gameObject.GetComponent<BasePlant>();
                if (plant != null)
                {
                    plant.SetHealth(-damage);

                    Vector2 dir = target.transform.position - transform.position;
                    plant.Impulse(dir.normalized * force);
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
            if (collision.CompareTag("Platform"))
            {
                if (anim != null)
                {
                    isBreak = true;
                    anim.SetTrigger("Break");
                }
                AudioManager.instance.PlaySfx(SfxType.SunExplodeEx);
            }
            else if (collision.CompareTag("Player"))
            {
                if (sender != collision.gameObject)
                {
                    if (anim != null)
                    {
                        isBreak = true;
                        anim.SetTrigger("Break");
                    }
                    AudioManager.instance.PlaySfx(SfxType.SunExplodeEx);
                }
            }
        }
    }
}
