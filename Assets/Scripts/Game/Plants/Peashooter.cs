using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX 
{
    public class Peashooter : BasePlant
    {
        [Header("#PeashooterFireDate")]
        public float timeDuration;

        [Header("#PeashooterOffsetPoint")]
        public Transform gunPoint;
        public override void Fire() 
        {
            if(nextAttackTime <= GameManager.instance.gameTime && !isEXAttack)
            {
                Debug.Log("Íã¶¹ÉäÊÖ¹¥»÷");
                
                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                ShooterPea();
            }
        }

        public override void Skill() 
        {
            Debug.Log("Íã¶¹ÉäÊÖ´óÕÐ");
            if(!isEXAttack)
            {
                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
                Invoke("BackEXAttack", timeDuration);
            }
        }

        public override void TurnTo()
        {
            if (isEXAttack) return;
            base.TurnTo();
        }

        public void ShooterPea()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.PeaBullet);
            pea.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject, gunPoint.position, gameObject.transform.rotation);
            AudioManager.instance.PlaySfx(SfxType.PeaShoot);
        }

        public void BackEXAttack()
        {
            isEXAttack = false;
            anim.SetBool("IsEXAttack", isEXAttack);
        }
    }

}
