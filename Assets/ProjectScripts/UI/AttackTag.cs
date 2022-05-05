using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.UI;
namespace DTR.UI
{
    /// <summary>
    /// 攻击标记
    /// </summary>
    public class AttackTag : BaseMono
    {
        /// <summary>
        /// 攻击标记事件
        /// </summary>
        public static readonly string AttackTagEvent = "AttackTagEvent";
        private IAssaultable m_IAssaultable = null;
        protected override void Awake()
        {
            base.Awake();
            MesgManager.MesgListen<IAssaultable>(AttackTagEvent, TagEvent);
            gameObject.SetActive(false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (m_IAssaultable != null)
            {
                m_IAssaultable.AddDiedListen(this.DiedCallback);
                switch (m_IAssaultable.TagType)
                {
                    case EnumTag.Dynamic:
                        {
                            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.Follow);
                            break;
                        }
                    case EnumTag.Static:
                        {
                            transform.position = m_IAssaultable.AttackTagPosition;
                            break;
                        }
                }
            }
        }
   
        protected override void OnDisable()
        {
            base.OnDisable();
            if (MonoSingletonFactory<ShareMono>.SingletonExist)
            {
                if (m_IAssaultable != null)
                {
                    m_IAssaultable.RemoveDiedListen(this.DiedCallback);
                    switch (m_IAssaultable.TagType)
                    {
                        case EnumTag.Dynamic:
                            {
                                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.Follow);
                                break;
                            }
                    }
                }
            }
            m_IAssaultable = null;
        }

        protected override void OnDestroy()
        {
            MesgManager.MesgBreakListen<IAssaultable>(AttackTagEvent, TagEvent);
            base.OnDestroy();
        }
        private void Follow()
        {
            if (m_IAssaultable != null)
            {
                transform.position = m_IAssaultable.AttackTagPosition;
            }
        }
        /// <summary>
        /// 死亡回调
        /// </summary>
        private void DiedCallback()
        {
            gameObject.SetActive(false);
        }
        /// <summary>
        /// 标记事件
        /// </summary>
        /// <param name="iAssaultable"></param>
        private void TagEvent(IAssaultable iAssaultable)
        {
            bool isSame = Equals(iAssaultable, m_IAssaultable);
            if(!isSame)
            {
                m_IAssaultable = iAssaultable;
            }        
            gameObject.SetActive(!isSame);
            
        }
    }
}
