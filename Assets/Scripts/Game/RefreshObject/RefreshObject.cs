using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace QFramework.PVZMAX
{
    public class RefreshObject : MonoBehaviour
    {

        [Header("#Component")]
        public SpriteRenderer sr;

        [Header("#PictureDate")]
        public Sprite[] pictures;
        public int index;
        public float playRate;

        [Header("#MoveDate")]
        [SerializeField] protected MovementMode mode;
        [SerializeField] protected Vector3 moveSpeed;

        void Update()
        {
            Move();
        }

        public void Init(Vector3 pos)
        {
            transform.position = pos;

            index = 0;
            sr.sprite = pictures[index];
            mode = MovementMode.Agravity;

            InvokeRepeating("PlayFrame", playRate, playRate);
        }

        public void Destroy()
        {
            CancelInvoke("PlayFrame");
            gameObject.SetActive(false);
        }

        private void Move()
        {
            if (mode == MovementMode.None)
            {
                return;
            }

            Vector3 pos = transform.position;

            if (mode == MovementMode.Agravity)
            {
                transform.position = Movement.AgravityMove(pos, moveSpeed, Time.deltaTime);
            }
        }
        private void PlayFrame()
        {
            if (index >= pictures.Length - 1)
                index = 0;
            else
                index++;

            sr.sprite = pictures[index];
        }
    }
}

