using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class Gloomshroom : BasePlant
    {
        [Header("#GloomshroomDate")]
        public float attackRange;
        public float attackBigRange;
        public override void Fire()
        {
            if (nextAttackTime <= GameManager.instance.gameTime && !isEXAttack)
            {
                AudioManager.instance.PlaySfx(SfxType.BubblesShot);
                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                BubblesAttack();

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

                AudioManager.instance.PlaySfx(SfxType.BubblesShot);
                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
            }
        }

        public void BubblesAttack()
        {
            GameObject bubbles = PoolManager.instance.Get((int)PerfabsName.Bubbles);
            if(elemType == ElemType.None)
            {
                bubbles.GetComponent<BaseBullet>().Init(MovementMode.None, gameObject, attackRange);
            }
            else
            {
                if (curElemEnergy >= maxElemEnergy / 4)
                {
                    bubbles.GetComponent<BaseBullet>().Init(MovementMode.None, gameObject, attackRange, elemType);
                    ConsumeElemEnergy(maxElemEnergy / 4);
                }
            }
        }

        public void BigBubblesAttack()
        {
            GameObject bigBubbles = PoolManager.instance.Get((int)PerfabsName.BigBubbles);
            bigBubbles.GetComponent<BaseBullet>().Init(MovementMode.None, gameObject, attackBigRange);
        }

        public void BackEXAttack()
        {
            isEXAttack = false;
            anim.SetBool("IsEXAttack", isEXAttack);
        }
    }
}
