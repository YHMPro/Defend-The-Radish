using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using DTR.Path;
using DTR.Config;
using Farme.Extend;
using Farme.Tool;
using Farme.UI;
using UnityEngine.Events;
using DTR.UI;
namespace DTR.Monster
{
    /// <summary>
    /// 怪物
    /// </summary>
    public class Monster : BaseMono,IRecycle, IAssaultable
    {
        /// <summary>
        /// 怪物配置
        /// </summary>
        protected MonsterConfig m_Config;
        /// <summary>
        /// 怪物中心点
        /// </summary>
        protected Transform m_Center;
        /// <summary>
        /// 怪物中心点
        /// </summary>
        public Transform Center => m_Center;
        /// <summary>
        /// 血量
        /// </summary>
        protected int m_Blood = 0;   
        /// <summary>
        /// 血量
        /// </summary>
        public int Blood => m_Blood;

        public bool IsDied
        {
            get { return gameObject.activeInHierarchy; } 
        }

        public Vector3 BloodPosition
        {
            get
            {
                if (MonoSingletonFactory<WindowRoot>.SingletonExist)
                {
                    Vector3 screenPoint = MonoSingletonFactory<WindowRoot>.GetSingleton().Camera.WorldToScreenPoint(transform.position + Vector3.up * 0.72f);
                    screenPoint.z = 0;
                    return screenPoint;
                }
                return Vector3.zero;
            }
        }
        
        public Vector3 AttackTagPosition
        {
            get
            {
                if (MonoSingletonFactory<WindowRoot>.SingletonExist)
                {
                    Vector3 screenPoint = MonoSingletonFactory<WindowRoot>.GetSingleton().Camera.WorldToScreenPoint(transform.position + Vector3.up * 1f);
                    screenPoint.z = 0;
                    return screenPoint;
                }
                return Vector3.zero;
            }
        }
        /// <summary>
        /// 血条类型
        /// </summary>
        public EnumTag TagType
        {
            get { return EnumTag.Dynamic; }
        }
        /// <summary>
        /// 血量变化回调
        /// </summary>
        private UnityAction<float> m_BloodChangeCallback;
        /// <summary>
        /// 死亡回调
        /// </summary>
        private UnityAction m_DiedCallback;
        /// <summary>
        /// 怪物类型
        /// </summary>
        protected EnumMonster m_MonsterType;
        /// <summary>
        /// 动画状态机
        /// </summary>
        protected Animator m_Anim;
        /// <summary>
        /// 目标网格
        /// </summary>
        protected IGrid m_AimGrid;


        protected virtual void OnMouseDown()
        {
            if (!MonoSingletonFactory<WindowRoot>.SingletonExist)
            {
                return;
            }
            WindowRoot windowRoot = MonoSingletonFactory<WindowRoot>.GetSingleton();
            if (windowRoot.ES.IsPointerOverGameObject())//当操作对象是UI时则屏蔽此次事件响应
            {
                return;
            }
            MesgManager.MesgTirgger<IAssaultable>(AttackTag.AttackTagEvent, this);
        }

        protected override void Awake()
        {
            base.Awake();
            m_Anim=GetComponent<Animator>();
            m_Center = transform.Find("Center");
            m_Config = MonsterConfig.GetMonsterConfig(m_MonsterType);

        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Blood = m_Config.Blood;
            m_AimGrid = PathManager.GetGrid(PathManager.GridIndexOf(PathManager.Head) + 1);
            transform.position = PathManager.Head.Position + new Vector3(0, 0, -0.1f);
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);
            MesgManager.MesgTirgger<IAssaultable>(TagPanel.BloodCreateEvent, this);
        }

        protected override void Start()
        {
            base.Start();
            
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();


        }
        protected virtual void MoveUpdate()
        {       
            float dis = Vector2.Distance(m_AimGrid.Position, transform.position);
            if (dis < 0.01f)
            {
                transform.position = m_AimGrid.Position+ new Vector3(0, 0, -0.1f);
                m_AimGrid = PathManager.GetGrid(PathManager.GridIndexOf(m_AimGrid) + 1);
                if (m_AimGrid == null)
                {
                    m_BloodChangeCallback?.Invoke(0);
                    Recycle();
                    Debuger.Log("怪物抵达终点!!!");
                    return;
                }           
            }
            transform.Translate(((Vector2)(m_AimGrid.Position - transform.position)).normalized * m_Config.MoveSpeed * Time.deltaTime);
        }


        protected override void OnDisable()
        {
            base.OnDisable();
            m_AimGrid = null;
            if (MonoSingletonFactory<ShareMono>.SingletonExist)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);
            }
        }

        public void Recycle(bool isDestroy=false)
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);
            if (isDestroy)
            {
                Destroy(gameObject);
                return;
            }
            gameObject.Recycle(m_MonsterType.ToString());
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            IShell iShell = collision.GetComponent<IShell>();
            if (iShell!=null&&Equals(iShell.ShellInfo.FollowMonster, this))//判断当前子弹打击的目标是否是自身
            {
                m_Blood -= iShell.ShellInfo.AttackValue;
                if (m_Blood <= 0)
                {
                    m_Blood = 0;
                    MonsterDied();
                    Recycle();
                }
                m_BloodChangeCallback?.Invoke(m_Blood / (float)m_Config.Blood);
            }
        }
        /// <summary>
        /// 怪物被炮弹打死时调用
        /// </summary>
        protected virtual void MonsterDied()
        {
            GameLogic.PutMoney(m_Config.Money);
            m_DiedCallback?.Invoke();
        }
        
        public void AddDiedListen(UnityAction ua )
        {
            m_DiedCallback += ua;
        }

        public void RemoveDiedListen(UnityAction ua)
        {
            m_DiedCallback -= ua;
        }

        public void AddBloodChangeListen(UnityAction<float> ua)
        {
            m_BloodChangeCallback += ua;
        }

        public void RemoveBloodChangeListen(UnityAction<float> ua)
        {
            m_BloodChangeCallback -= ua;
        }
    }
}
