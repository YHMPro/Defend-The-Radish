using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR
{
    /// <summary>
    /// 炮弹信息
    /// </summary>
    public class ShellInfo
    {
        /// <summary>
        /// 跟随的怪物
        /// </summary>
        public Monster.Monster FollowMonster = null;
        /// <summary>
        /// 子弹等级
        /// </summary>
        public int ShellGrade = 0;
        /// <summary>
        /// 飞行速率
        /// </summary>
        public float FlySpeed = 0;
        /// <summary>
        /// 攻击值
        /// </summary>
        public int AttackValue = 0;
        /// <summary>
        /// 复制信息
        /// </summary>
        /// <param name="shellInfo">炮弹信息</param>
        public void CopyInfo(ShellInfo shellInfo)
        {
            this.FollowMonster= shellInfo.FollowMonster;
            this.FlySpeed= shellInfo.FlySpeed;
            this.AttackValue = shellInfo.AttackValue;
            this.ShellGrade= shellInfo.ShellGrade;
        }
    }
}
namespace DTR.Shell
{
    /// <summary>
    /// 炮弹
    /// </summary>
    public class Shell : BaseMono,IShell
    {
        /// <summary>
        /// 绑定的塔台
        /// </summary>
        public Tower.Tower BindTower = null;
        /// <summary>
        /// 炮弹信息
        /// </summary>
        protected ShellInfo m_ShellInfo = new ShellInfo();
        /// <summary>
        /// 炮弹信息
        /// </summary>
        public ShellInfo ShellInfo => m_ShellInfo;
        /// <summary>
        /// 碰撞盒
        /// </summary>
        protected Collider2D m_Co;
        /// <summary>
        /// 动画状态机
        /// </summary>
        protected Animator m_Anim;

        protected override void Awake()
        {
            base.Awake();
            m_Anim = GetComponent<Animator>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Co.enabled = true;
        }

        protected override void LateOnEnable()
        {
            if (BindTower != null)
            {
                m_ShellInfo.CopyInfo(BindTower.ShellInfo);
                m_Anim.SetTrigger("ShellGrade" + BindTower.TowerGrade);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_ShellInfo.FollowMonster = null;        
        }
        protected virtual void MoveUpdate()
        {

        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {

        }

    }
}
