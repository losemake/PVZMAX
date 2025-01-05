using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    public class Nut : BasePlant
    {

        public override void Fire()
        {
            Debug.Log("坚果攻击");
        }

        public override void Skill()
        {
            Debug.Log("坚果大招");
        }
    }

}
