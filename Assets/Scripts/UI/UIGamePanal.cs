using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.PVZMAX
{
    /// <summary>
    /// UI(抽象类防止基类被实例化)基类
    /// </summary>
    public abstract class UIGamePanal : MonoBehaviour
    {
        protected void OnEnable()
        {
            OnOpen();
        }

        protected void OnDestroy()
        {
            OnClose();
        }

        protected virtual void OnOpen() { }

        protected virtual void OnClose() { }

        public virtual void OnShow() 
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            if(canvasGroup != null)
            {
                canvasGroup.alpha = 1.0f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public virtual void OnHide() 
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0.0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }
    }
}