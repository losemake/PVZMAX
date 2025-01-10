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
                Debug.Log("向日葵攻击");

                nextAttackTime = GameManager.instance.gameTime + attackCooldown;
                ShooterSun();
            }
        }

        public override void Skill()
        {
            Debug.Log("向日葵大招");
            if (!isEXAttack)
            {
                isEXAttack = true;
                anim.SetBool("IsEXAttack", isEXAttack);
            }
        }

        public void ShooterSun()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.Sun);
            pea.GetComponent<BaseBullet>().Init(MovementMode.Gravity, gameObject, proSunPoint.position, gameObject.transform.rotation);
        }

        public void CallBigSun()
        {
            GameObject pea = PoolManager.instance.Get((int)PerfabsName.BigSun);
            Vector3 point = gameObject.transform.position;
            point.y += 10.0f;
            pea.GetComponent<BaseBullet>().Init(MovementMode.Agravity, gameObject, point, gameObject.transform.rotation);

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
