using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.UI;
using Farme.Extend;
namespace DTR.UI
{
    public class Blood : BaseMono,IRecycle
    {
        public IAssaultable IAssaultable = null;
        private Image m_BloodBg;
        private Image m_BloodFill;
        private Coroutine m_Cor = null;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Image>();
            m_BloodFill=GetComponent<Image>("BloodFill");
            m_BloodBg = GetComponent<Image>();           
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            m_BloodBg.enabled = false;
            m_BloodFill.enabled = false;
        }
        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            IAssaultable.AddBloodChangeListen(this.BloodChangeCallback);
            IAssaultable.AddDiedListen(this.DiedCallback);
            switch (IAssaultable.TagType)
            {
                case EnumTag.Dynamic:
                    {
                        MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.Follow);
                        break;
                    }
                case EnumTag.Static:
                    {
                        transform.position = IAssaultable.BloodPosition;
                        break;
                    }
            }

        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (MonoSingletonFactory<ShareMono>.SingletonExist)
            {
                switch (IAssaultable.TagType)
                {
                    case EnumTag.Dynamic:
                        {
                            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.Follow);
                            break;
                        }
                }
                if (m_Cor != null)
                {
                    MonoSingletonFactory<ShareMono>.GetSingleton().StopCoroutine(m_Cor);
                }
            }
            m_BloodFill.fillAmount = 1;
            IAssaultable = null;
      
        }
        private void Follow()
        {
            if (IAssaultable != null)
            {
                transform.position = IAssaultable.BloodPosition;             
            }
        }
        /// <summary>
        /// 死亡回调
        /// </summary>
        private void DiedCallback()
        {
            Recycle();
        }

        /// <summary>
        /// 血量变化回调
        /// </summary>
        /// <param name="percent">剩余血量百分比</param>
        private void BloodChangeCallback(float percent)
        {
            m_BloodBg.enabled = true;
            m_BloodFill.enabled = true;
            m_BloodFill.fillAmount = percent;
            if (m_Cor != null)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().StopCoroutine(m_Cor);            
            }
            m_Cor = MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(4f, () =>
            {
                m_BloodBg.enabled = false;
                m_BloodFill.enabled = false;
            });   
        }

        public void Recycle(bool isDestroy = false)
        {
            if(isDestroy)
            {
                Destroy(gameObject);
                return;
            }
            IAssaultable.RemoveBloodChangeListen(this.BloodChangeCallback);
            IAssaultable.RemoveDiedListen(this.DiedCallback);
            switch (IAssaultable.TagType)
            {
                case EnumTag.Dynamic:
                    {
                        MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.Follow);
                        break;
                    }
            }
            gameObject.Recycle(gameObject.name);
        }
    }
}
