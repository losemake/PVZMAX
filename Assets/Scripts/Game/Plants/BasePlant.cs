using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.PVZMAX 
{
    public enum PlantPrefabs
    {
        Peashooter,
        Sunflower,
        Gloomshroom,
        Nut,
        END
    }
    public abstract class BasePlant : MonoBehaviour
    {
        [Header("#BaseDate")]
        public float moveSpeed;

        public int jumpTime;
        public int maxJumpTime;
        public float jumpSpeed;

        public float currentHealth;
        public float maxHealth;

        [Header("#FireDate")]
        public BulletPrefabs mButtet;
        public float attackCooldown;
        public float nextAttackTime;

        [Header("#FireDate")]
        public float sunFxCooldown;
        public float nextSunFxTime;

        [Header("#State")]
        public bool isRun;
        public bool isEXAttack;

        [Header("#OffsetPoint")]
        public Transform footPoint;

        [Header("#Component")]
        public Rigidbody2D rb;
        public Animator anim;

        protected void Update()
        {
            if (Mathf.Abs(rb.velocity.x) >= 0.05f)
            {
                isRun = true;

                if (nextSunFxTime <= GameManager.instance.gameTime)
                {
                    nextSunFxTime = GameManager.instance.gameTime + sunFxCooldown;

                    GameObject runFx = PoolManager.instance.Get((int)PerfabsName.RunFx);
                    runFx.GetComponent<Effect>().Init(footPoint.transform.position, footPoint.transform.rotation);
                }
            }
            else
            {
                isRun = false;
            }


            anim.SetBool("IsRun", isRun);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.tag == "Platform")
            {
                var raycastall = Physics2D.OverlapBoxAll(footPoint.position, new Vector2(0.4f, 0.1f), 0, LayerMask.GetMask("Platform"));
                if (raycastall.Length > 0)
                {
                    jumpTime = maxJumpTime;

                    GameObject fallFx = PoolManager.instance.Get((int)PerfabsName.FallFx);
                    fallFx.GetComponent<Effect>().Init(footPoint.transform.position, footPoint.transform.rotation);
                }
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(footPoint.position, new Vector3(0.4f, 0.1f, 0.1f));
        }
        
        public virtual void Move(float dir)
        {
            TurnTo();

            Vector2 force = new Vector2(moveSpeed * dir, 0.0f);

            Force(force);
        }
        
        public void Jump()
        {
            if(jumpTime > 0)
            {
                if(jumpTime == maxJumpTime)
                {
                    GameObject jumpFx = PoolManager.instance.Get((int)PerfabsName.JumpFx);
                    jumpFx.GetComponent<Effect>().Init(footPoint.transform.position, footPoint.transform.rotation);
                }
                    
                jumpTime--;
                Vector2 impulse = new Vector2(0.0f, jumpSpeed);
                Impulse(impulse);
            }
        }

        public virtual void TurnTo()
        {
            if (Mathf.Abs(rb.velocity.x) >= 0.001f && Mathf.Sign(rb.velocity.x) != transform.forward.z)
            {
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            }
        }
        public virtual void Fire() { }

        public virtual void Skill() { }

        public void Force(Vector2 force)
        {
            rb.AddForce(force, ForceMode2D.Force);
        }
        public void Impulse(Vector2 impulse)
        {
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }
    }
}
