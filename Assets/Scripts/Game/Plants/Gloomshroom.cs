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
                Debug.Log("´óÅç¹½¹¥»÷");
                AudioManager.instance.PlaySfx(SfxType.BubblesShot);
                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                BubblesAttack();
            }
        }

        public override void Skill()
        {
            if (!isEXAttack)
            {
                Debug.Log("´óÅç¹½´óÕÐ");
                AudioManager.instance.PlaySfx(SfxType.BubblesShot);
                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
            }
        }

        public void BubblesAttack()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.Bubbles);
            pea.GetComponent<BaseBullet>().Init(MovementMode.None, gameObject, attackRange);
        }

        public void BigBubblesAttack()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.BigBubbles);
            pea.GetComponent<BaseBullet>().Init(MovementMode.None, gameObject, attackBigRange);
        }

        public void BackEXAttack()
        {
            isEXAttack = false;
            anim.SetBool("IsEXAttack", isEXAttack);
            Debug.Log("¹Ø±Õ¶¯»­");
        }
    }

}
