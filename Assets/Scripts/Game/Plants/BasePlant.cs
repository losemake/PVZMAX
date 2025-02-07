using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;


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

    public enum ElemType
    {
        None,
        Wind,
        Water,
        Fire,
        Thunder,
        Ice,
        Flame,
    }


    public abstract class BasePlant : MonoBehaviour, ICanSendEvent
    {
        [Header("#BaseDate")]
        public float moveSpeed;

        public int jumpTime;
        public int maxJumpTime;
        public float jumpSpeed;

        public float currentHealth;
        public float maxHealth;

        public float currentEnergy;
        public float maxEnergy;

        public ElemType elemType;
        public float curElemEnergy;
        public float maxElemEnergy;

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
        public SpriteRenderer sr;

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

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Area"))
                return;

            SetHealth(-currentHealth);
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

        public virtual void TurnTo(float dir)
        {
            if (dir == 0)
                return;

            if(Mathf.Sign(dir) != transform.forward.z)
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
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

        public void SetHealth(float value)
        {
            currentHealth = Mathf.Max(0, Mathf.Min(maxHealth, currentHealth + value));

            this.SendEvent<ChangeHealthEvent>();

            if(currentHealth == 0)
                StartCoroutine(GameOverFn());
        }

        public float GetHealthPer()
        {
            return currentHealth / maxHealth;
        }
        public void SetEnergy(float value)
        {
            currentEnergy = Mathf.Max(0, Mathf.Min(maxEnergy, currentEnergy + value));

            this.SendEvent<ChangeEnergyEvent>();
        }

        public float GetEnergyPer()
        {
            return currentEnergy / maxEnergy;
        }

        public void SetSpeed(float value)
        {
            moveSpeed = Mathf.Max(0, moveSpeed + value);
            jumpSpeed = Mathf.Max(0, jumpSpeed + value / 2);
        }

        public void ConsumeElemEnergy(float value)
        {
            curElemEnergy = Mathf.Max(0, curElemEnergy - value);
            
            if(curElemEnergy == 0)
            {
                elemType = ElemType.None;
            }

            this.SendEvent<ChangeElemEnergyEvent>();
        }
        public void ChangedElemEnergy(ElemType type)
        {
            if(elemType == ElemType.Water && type == ElemType.Wind)
            {
                elemType = ElemType.Ice;
            }
            else if(elemType == ElemType.Fire && type == ElemType.Wind)
            {
                elemType = ElemType.Flame;
            }
            else
            {
                elemType = type;
            }
            curElemEnergy = maxElemEnergy;

            this.SendEvent<ChangeElemEnergyEvent>();
        }

        public float GetElemEnergyPer()
        {
            
            return curElemEnergy / maxElemEnergy;
        }

        public void SetColor()
        {
            switch (elemType)
            {
                case ElemType.None:
                    
                    break;
                case ElemType.Wind:
                    break; 
                case ElemType.Water:
                    break;
                case ElemType.Fire:
                    break;
                case ElemType.Thunder:
                    break;
                case ElemType.Ice:
                    break;
                case ElemType.Flame:
                    break;
            }
        }

        IEnumerator GameOverFn()
        {
            yield return null;      // —”≥Ÿ÷¡œ¬“ª÷°
            GameManager.instance.SetSceneState(SceneState.End);
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}
