using UnityEngine;

namespace QFramework.PVZMAX 
{
    public static class Movement
    {
        public struct MovementDate
        {
            public Vector3 pos;
            public Vector3 speed;
            public MovementDate(Vector3 pos, Vector3 speed)
            {
                this.pos = pos;
                this.speed = speed;
            }
        }
        // 无组件运动
        public static Vector3 AgravityMove(Vector3 pos, Vector3 speed, float time)
        {
            pos += speed * time;
            return pos;
        }

        public static MovementDate GravityMove(Vector3 pos, Vector3 speed, float time)
        {
            speed.y -= 10 * time;
            pos += speed * time;

            return new MovementDate(pos, speed);
        }
    }
}


