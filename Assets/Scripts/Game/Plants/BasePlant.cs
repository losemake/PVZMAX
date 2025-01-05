using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.PVZMAX 
{
    public abstract class BasePlant : MonoBehaviour
    {
        [Header("#BaseDate")]
        public float moveSpeed;

        public int jumpTime;
        public int maxJumpTime;
        public float jumpSpeed;

        public float currentHealth;
        public float maxHealth;

        [Header("#State")]
        public bool isRun;

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
                }
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(footPoint.position, new Vector3(0.4f, 0.1f, 0.1f));
        }
        
        public void Move(float dir)
        {
            if (Mathf.Abs(rb.velocity.x) >= 0.001f && Mathf.Sign(rb.velocity.x) != transform.forward.z)
            {
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            }

            Vector2 force = new Vector2(moveSpeed * dir, 0.0f);

            Force(force);
        }
        
        public void Jump()
        {
            if(jumpTime > 0)
            {
                jumpTime--;
                Vector2 impulse = new Vector2(0.0f, jumpSpeed);
                Impulse(impulse);
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
