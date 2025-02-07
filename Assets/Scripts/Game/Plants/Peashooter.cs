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
                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                ShooterPea();

                this.SetEnergy(5);
            }
        }

        public override void Skill() 
        {
            if(!isEXAttack)
            {
                if (currentEnergy < maxEnergy)
                    return;
                this.SetEnergy(-maxEnergy);

                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
                Invoke("BackEXAttack", timeDuration);
            }
        }

        public override void TurnTo(float dir)
        {
            if (isEXAttack) return;
            base.TurnTo(dir);
        }

        public void ShooterPea()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.PeaBullet);

            if(elemType == ElemType.None)
            {
                pea.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject, gunPoint.position, gameObject.transform.rotation);
            }
            else
            {
                if(curElemEnergy >= maxElemEnergy / 4)
                {
                    pea.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject, gunPoint.position, gameObject.transform.rotation, elemType);
                    ConsumeElemEnergy(maxElemEnergy / 4);
                }
            }
            
            AudioManager.instance.PlaySfx(SfxType.PeaShoot);
        }

        public void BackEXAttack()
        {
            isEXAttack = false;
            anim.SetBool("IsEXAttack", isEXAttack);
        }
    }

}
