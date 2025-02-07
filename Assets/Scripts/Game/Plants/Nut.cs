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
                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                ShooterNut();

                this.SetEnergy(5);
            }
        }

        public override void Skill()
        {
            if (!isEXAttack)
            {
                if (currentEnergy < maxEnergy)
                    return;
                this.SetEnergy(-maxEnergy);

                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
            }
        }
        public void ShooterNut()
        {
            GameObject nut = PoolManager.instance.Get((int)PerfabsName.NutBullet);

            if (elemType == ElemType.None)
            {
                nut.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject);
            }
            else
            {
                if (curElemEnergy >= maxElemEnergy / 4)
                {
                    nut.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject, elemType);
                    ConsumeElemEnergy(maxElemEnergy / 4);
                }
            }
                
            AudioManager.instance.PlaySfx(SfxType.NutDash);
        }
        public void ExplodeAttack()
        {
            GameObject explode = PoolManager.instance.Get((int)PerfabsName.NutExplode);
            explode.GetComponent<BaseBullet>().Init(MovementMode.None, gameObject, explodeRange);
        }

        public void ExplodeAudio()
        {
            AudioManager.instance.PlaySfx(SfxType.NutExplode);
        }

        public void BackEXAttack()
        {
            isEXAttack = false;
            anim.SetBool("IsEXAttack", isEXAttack);
        }
    }

}
