using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using Farme;
namespace DTR.UI
{
    /// <summary>
    /// 标签面板
    /// </summary>
    public class TagPanel : BasePanel
    {
        /// <summary>
        /// 血条创建事件
        /// </summary>
        public static readonly string BloodCreateEvent = "BloodCreateEvent";
        /// <summary>
        /// 攻击标签
        /// </summary>
        private RectTransform m_AttackTag;
        /// <summary>
        /// 血条标签层
        /// </summary>
        private RectTransform m_BloodTags;
        /// <summary>
        /// 升级标签层
        /// </summary>
        private RectTransform m_UpgradeTags;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<RectTransform>();

            m_AttackTag=GetComponent<RectTransform>("AttackTag");
            m_BloodTags =GetComponent<RectTransform>("BloodTags");
            m_UpgradeTags = GetComponent<RectTransform>("UpgradeTags");

            MesgManager.MesgListen<IAssaultable>(BloodCreateEvent, this.BloodCreate);
        }

        protected override void OnDestroy()
        {
            MesgManager.MesgBreakListen<IAssaultable>(BloodCreateEvent, this.BloodCreate);
            base.OnDestroy();
        }


        //private void AttackTag
        /// <summary>
        /// 血条创建
        /// </summary>
        /// <param name="iAssaultable"></param>
        private void BloodCreate(IAssaultable iAssaultable)
        {
            if (!GoReusePool.Take("Blood", out GameObject blood))
            {
                if (!GoLoad.Take("Prefabs/UI/Blood",out blood, m_BloodTags))
                {
                    return;
                }
            }
            blood.GetComponent<Blood>().IAssaultable = iAssaultable;
        }

    }
}
