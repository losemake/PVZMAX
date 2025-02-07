using UnityEngine;

namespace QFramework.PVZMAX 
{
    /// <summary>
    /// buff����
    /// </summary>
    public abstract class BuffBase
    {
        /// <summary>
        /// Buff����
        /// </summary>
        public BuffName name;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public float durTime;

        /// <summary>
        /// buffӵ����
        /// </summary>
        protected GameObject owner;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        protected float overTime;
        
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public virtual void Init(GameObject owner)
        {
            this.owner = owner;
            this.overTime = GameManager.instance.gameTime + durTime;
        }

        /// <summary>
        /// buff���ִ�к���
        /// </summary>
        public virtual void OnAdd() { }

        /// <summary>
        /// buffÿִ֡�к���
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// buff����ִ�к���
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