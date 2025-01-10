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
            Debug.Log("Àë¿ªÆÁÄ»Íâ");
            Destroy();
        }
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (sender != collision.gameObject)
                {
                    Debug.Log("Åö×²Íæ¼Ò");
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