using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

namespace QFramework.PVZMAX
{
    public enum BulletPrefabs
    {
        PeaBullet,
    }

    public enum MovementMode 
    {
        Agravity,
        Gravity,
        None,
    }
    public abstract class BaseBullet : MonoBehaviour
    {
        [Header("#Component")]
        public Animator anim;

        [Header("#BaseDate")]
        public MovementMode mode;
        protected GameObject sender;
        [SerializeField] protected Vector3 moveSpeed;
        public Vector3 curMoveSpeed;
        public ElemType type;

        public float damage;
        public float force;
        protected void Update()
        {
            Move();
        }
        public virtual void Init(MovementMode mode, GameObject sender)
        {
            this.mode = mode;
            this.sender = sender;
            transform.position = sender.transform.position;
            transform.rotation = sender.transform.rotation;
            
            curMoveSpeed = moveSpeed;
            curMoveSpeed.x *= sender.transform.forward.z;

            this.type = ElemType.None;
        }

        public virtual void Init(MovementMode mode, GameObject sender, Vector3 pos, Quaternion rot)
        {
            this.mode = mode;
            this.sender = sender;
            transform.position = pos;
            transform.rotation = rot;

            curMoveSpeed = moveSpeed;
            curMoveSpeed.x *= sender.transform.forward.z;

            this.type = ElemType.None;
        }

        public virtual void Init(MovementMode mode, GameObject sender, float other)
        {
            this.mode = mode;
            this.sender = sender;
            transform.position = sender.transform.position;
            transform.rotation = sender.transform.rotation;

            this.type = ElemType.None;
        }

        public virtual void Init(MovementMode mode, GameObject sender, ElemType type)
        {
            this.Init(mode, sender);
            this.type = type;
        }

        public virtual void Init(MovementMode mode, GameObject sender, Vector3 pos, Quaternion rot, ElemType type)
        {
            this.Init(mode, sender, pos, rot);
            this.type = type;
        }

        public virtual void Init(MovementMode mode, GameObject sender, float other, ElemType type)
        {
            this.Init(mode, sender, other);
            this.type = type;
        }
        public void Destroy()
        {
            gameObject.SetActive(false);
        }
        protected virtual void Move()
        {
            if(mode == MovementMode.None)
            {
                return;
            }

            Vector3 pos = transform.position;

            if (mode == MovementMode.Agravity)
            {
                transform.position = Movement.AgravityMove(pos, curMoveSpeed, Time.deltaTime);
            }else if(mode == MovementMode.Gravity)
            {
                Movement.MovementDate date = Movement.GravityMove(pos, curMoveSpeed, Time.deltaTime);

                transform.position = date.pos;
                curMoveSpeed = date.speed;
            }
        }

        public RaycastHit2D[] RangeAttack(float scanRange, LayerMask layer)
        {
            return Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, layer);
        }
    }
}