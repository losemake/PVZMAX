using UnityEngine;

namespace QFramework.PVZMAX 
{
    /// <summary>
    /// buff基类
    /// </summary>
    public abstract class BuffBase
    {
        /// <summary>
        /// Buff名称
        /// </summary>
        public BuffName name;

        /// <summary>
        /// 结束时间
        /// </summary>
        public float durTime;

        /// <summary>
        /// buff拥有者
        /// </summary>
        protected GameObject owner;

        /// <summary>
        /// 结束时间
        /// </summary>
        protected float overTime;
        
        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init(GameObject owner)
        {
            this.owner = owner;
            this.overTime = GameManager.instance.gameTime + durTime;
        }

        /// <summary>
        /// buff添加执行函数
        /// </summary>
        public virtual void OnAdd() { }

        /// <summary>
        /// buff每帧执行函数
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// buff结束执行函数
        /// </summary>
        public virtual void OnRemove() { }

        public bool CompareEqualBuff(BuffBase otherBuff)
        {
            return otherBuff.name == this.name && otherBuff.owner == this.owner;
        }

        public float GetOverTime()
        {
            return this.overTime;
        }
        public void RepeatedGetBuff()
        {
            this.overTime = GameManager.instance.gameTime + durTime;
        }
    }
}