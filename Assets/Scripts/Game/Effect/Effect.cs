using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace QFramework.PVZMAX 
{
    public class Effect : MonoBehaviour
    {
        [Header("#Component")]
        public SpriteRenderer sr;
        public Vector3 offset;

        [Header("#PictureDate")]
        public Sprite[] pictures;
        public int index;
        public float playRate;

        public void Init(Vector3 pos, Quaternion rot)
        {
            transform.position = pos + offset;
            transform.rotation = rot;

            index = 0;
            sr.sprite = pictures[index];

            InvokeRepeating("PlayFrame", playRate, playRate);
        }

        
        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void PlayFrame()
        {
            if (index >= pictures.Length - 1)
            {
                Destroy();
                CancelInvoke("PlayFrame");
                return;
            }
            
            index++;
            sr.sprite = pictures[index];
        }
    }
}