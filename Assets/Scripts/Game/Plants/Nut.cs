using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class Nut : BasePlant
    {
        [Header("#NutDate")]
        public float explodeRange;
        public override void Fire()
        {
            
            if (nextAttackTime <= GameManager.instance.gameTime && !isEXAttack)
            {
                Debug.Log("坚果攻击");

                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                ShooterNut();
            }
        }

        public override void Skill()
        {
            Debug.Log("坚果大招");
            if (!isEXAttack)
            {
                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
            }
        }
        public void ShooterNut()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.NutBullet);
            pea.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject);

            AudioManager.instance.PlaySfx(SfxType.NutDash);
        }
        public void ExplodeAttack()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.NutExplode);
            pea.GetComponent<BaseBullet>().Init(MovementMode.None, gameObject, explodeRange);
        }

        public void ExplodeAudio()
        {
            AudioManager.instance.PlaySfx(SfxType.NutExplode);
        }

        public void BackEXAttack()
        {
            isEXAttack = false;
            anim.SetBool("IsEXAttack", isEXAttack);
            Debug.Log("关闭动画");
        }
    }

}
