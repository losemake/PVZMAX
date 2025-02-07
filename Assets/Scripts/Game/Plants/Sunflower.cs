using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class Sunflower : BasePlant
    {
        [Header("#SunOffsetPoint")]
        public Transform proSunPoint;
        public Transform exTextPoint;
        public override void Fire()
        {
            if (nextAttackTime <= GameManager.instance.gameTime && !isEXAttack)
            {
                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                ShooterSun();

                this.SetEnergy(10);
            }
        }

        public override void Skill()
        {
            if (!isEXAttack)
            {
                if (currentEnergy < maxEnergy / 2)
                    return;
                this.SetEnergy(-maxEnergy / 2);

                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
            }
        }

        public void ShooterSun()
        {
            GameObject sun = PoolManager.instance.Get((int)PerfabsName.Sun);
            if (elemType == ElemType.None)
            {
                sun.GetComponent<BaseBullet>().Init(MovementMode.Gravity, gameObject, proSunPoint.position, gameObject.transform.rotation);
            }
            else
            {
                if (curElemEnergy >= maxElemEnergy / 4)
                {
                    sun.GetComponent<BaseBullet>().Init(MovementMode.Gravity, gameObject, proSunPoint.position, gameObject.transform.rotation, elemType);
                    ConsumeElemEnergy(maxElemEnergy / 4);
                }
            }
        }

        public void CallBigSun()
        {
            GameObject bigSun = PoolManager.instance.Get((int)PerfabsName.BigSun);
            Vector3 point = gameObject.transform.position;
            point.y += 10.0f;
            bigSun.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject, point, gameObject.transform.rotation);

            GameObject textFx = PoolManager.instance.Get((int)PerfabsName.SunTextFx);
            textFx.GetComponent<Effect>().Init(exTextPoint.transform.position, exTextPoint.transform.rotation);

            AudioManager.instance.PlaySfx(SfxType.SunText);
        }
        public void BackEXAttack()
        {
            isEXAttack = false;
            anim.SetBool("IsEXAttack", isEXAttack);
        }
    }

}
