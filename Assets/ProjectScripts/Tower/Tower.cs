using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Extend;
using DTR.Config;
namespace DTR.Tower
{
    /// <summary>
    /// 塔台
    /// </summary>
    public abstract class Tower : BaseMono,ITower
    {
        protected CircleCollider2D m_Co;
        /// <summary>
        /// 炮弹信息
        /// </summary>
        protected ShellInfo m_ShellInfo = new ShellInfo();
        /// <summary>
        /// 炮弹信息
        /// </summary>
        public ShellInfo ShellInfo => m_ShellInfo;
        /// <summary>
        /// 配置
        /// </summary>
        protected TowerConfig m_Config = null;
        protected Coroutine m_Cor = null;
        /// <summary>
        /// 塔台类型
        /// </summary>
        public EnumTower TowerType => m_TowerType;
        [SerializeField]
        /// <summary>
        /// 塔台类型
        /// </summary>
        protected EnumTower m_TowerType;
        /// <summary>
        /// 计时器
        /// </summary>
        protected float m_Timer = 0;      
        /// <summary>
        /// 动画状态控制器
        /// </summary>
        protected Animator m_Anim;
        [SerializeField]
        /// <summary>
        /// 塔台等级
        /// </summary>
        protected int m_TowerGrade = 1;
        /// <summary>
        /// 塔台等级
        /// </summary>
        public int TowerGrade => m_TowerGrade;
        [SerializeField]
        /// <summary>
        /// 攻击怪物的列表
        /// </summary>
        protected List<Monster.Monster> m_AttackMonsterLi=new List<Monster.Monster>();
        /// <summary>
        /// 当前攻击的怪物
        /// </summary>
        public Monster.Monster NowAttackMonster
        {
            get
            {
                if(m_AttackMonsterLi.Count!=0)
                {
                    return m_AttackMonsterLi[0];
                }
                return null;
            }
        }
        /// <summary>
        /// 锁定的攻击的怪物
        /// </summary>
        protected static Monster.Monster m_LockAttackMonster = null;
        /// <summary>
        /// 射击点位
        /// </summary>
        protected Transform m_ShootPoint = null;

        protected override void Awake()
        {        
            base.Awake();
            RegisterComponentsTypes<Transform>();
            m_Anim = GetComponent<Animator>();
            m_Co = GetComponent<CircleCollider2D>();
            m_ShootPoint = GetComponent<Transform>("ShootPoint");
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            
        }
        protected override void Start()
        {
            base.Start();
            m_Config = TowerConfig.GetTowerConfig(m_TowerType);
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            m_Co.radius = m_Config.GetAttackDis(m_TowerGrade - 1);
            m_ShellInfo.AttackValue = m_Config.GetAttackValue(m_TowerGrade-1);
            m_ShellInfo.FlySpeed = m_Config.GetShellFlySpeed(m_TowerGrade-1);
            m_Anim.SetTrigger("Grade" + m_TowerGrade);
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.AttackCheckUpdate);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_TowerGrade = 1;
            m_AttackMonsterLi.Clear();
            if (MonoSingletonFactory<ShareMono>.SingletonExist)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.AttackCheckUpdate);
            }
        }
        #region AttackCheck
        /// <summary>
        /// 攻击检测
        /// </summary>
        protected virtual void AttackCheckUpdate()
        {
            if(m_Timer-Time.time<=0)
            {
                m_Timer = Time.time +m_Config.GetAttackFrequency(m_TowerGrade-1);
                TriggerAttack();
            }
        }
        #endregion


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
            if (m_TowerGrade < 3)
            {
                m_TowerGrade++;
                m_ShellInfo.ShellGrade = m_TowerGrade;
                
                m_Co.radius = m_Config.GetAttackDis(m_TowerGrade - 1);
                m_Anim.SetTrigger("Grade" + m_TowerGrade);
            }
            
        }
        
        /// <summary>
        /// 塔台卸载
        /// </summary>
        public virtual void TowerTeardown()
        {
            if (m_Cor != null)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().StopCoroutine(m_Cor);
                m_Cor = null;
            }
            GameLogic.PutMoney(m_Config.GetTeardownMoney(m_TowerGrade-1));
            gameObject.Recycle(m_TowerType.ToString());
        }


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            m_AttackMonsterLi.Add(collision.GetComponent<Monster.Monster>());      
            if(m_AttackMonsterLi.Count==1)
            {
                m_ShellInfo.FollowMonster = m_AttackMonsterLi[0];
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            m_AttackMonsterLi.Remove(collision.GetComponent<Monster.Monster>());
            m_ShellInfo.FollowMonster = (m_AttackMonsterLi.Count > 0) ? m_AttackMonsterLi[0] : null;
            //m_Cor = MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(0.15f, () =>
            // {
            //     m_AttackMonsterLi.Remove(collision.GetComponent<Monster.Monster>());
            //     m_ShellInfo.FollowMonster = (m_AttackMonsterLi.Count > 0) ? m_AttackMonsterLi[0] : null;
            // });
        }

      
    }
}
