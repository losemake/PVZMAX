using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class Sunflower : BasePlant
    {

        public override void Fire()
        {
            Debug.Log("向日葵攻击");
        }

        public override void Skill()
        {
            Debug.Log("向日葵大招");
        }
    }

}
