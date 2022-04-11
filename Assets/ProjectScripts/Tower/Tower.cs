using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Tower
{
    /// <summary>
    /// 塔台
    /// </summary>
    public abstract class Tower : BaseMono
    {
        /// <summary>
        /// 塔台默认精灵
        /// </summary>
        public Sprite[] TowerSprs;
        /// <summary>
        /// 塔台精灵渲染器
        /// </summary>
        protected SpriteRenderer m_Spr;
        /// <summary>
        /// 动画状态控制器
        /// </summary>
        protected Animator m_Anim;
        [SerializeField]
        /// <summary>
        /// 塔台等级
        /// </summary>
        protected int m_TowerGrade = 1;

        #region Attack
        /// <summary>
        /// 触发攻击
        /// </summary>
        protected virtual void TriggerAttack()
        {
            m_Anim.SetTrigger("attackGrade" + m_TowerGrade);
        }
        /// <summary>
        /// 监听攻击命令
        /// </summary>
        [SerializeField]
        protected virtual void OnAttack()
        {

        }
        #endregion
        /// <summary>
        /// 塔台升级
        /// </summary>
        public virtual void TowerUpgrade()
        {
            if (m_TowerGrade < 4)
            {
                m_TowerGrade++;
            }
            
        }
    }
}
