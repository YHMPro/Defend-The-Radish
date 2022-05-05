using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace DTR.Config
{
    /// <summary>
    /// 塔台配置
    /// </summary>
    public class TowerConfig 
    {
        /// <summary>
        /// 塔台配置容器
        /// </summary>
        private static Dictionary<EnumTower, TowerConfig> m_TowerConfigDic=new Dictionary<EnumTower, TowerConfig>();
        /// <summary>
        /// 获取塔台配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="towerType">塔台类型</param>
        /// <returns></returns>
        private static T GetTowerConfig<T>(EnumTower towerType) where T : TowerConfig,new()
        {
            if(!m_TowerConfigDic.TryGetValue(towerType, out TowerConfig towerConfig))
            {
                towerConfig = new T();
                towerConfig.InitConfig();
                m_TowerConfigDic.Add(towerType, towerConfig);
            }
            return (T)towerConfig;
        }
        /// <summary>
        /// 获取塔台配置
        /// </summary>
        /// <param name="towerType">塔台类型</param>
        /// <returns></returns>
        public static TowerConfig GetTowerConfig(EnumTower towerType)
        {
            TowerConfig towerConfig = null;
            switch (towerType)
            {
                case EnumTower.BottleTower:
                    {
                        towerConfig = GetTowerConfig<BottleTowerConfig>(towerType);
                        break;
                    }
            }
            return towerConfig;
        }
        /// <summary>
        /// 塔台图标路径
        /// </summary>
        protected string m_TowerIconPath;
        /// <summary>
        /// 塔台图标路径
        /// </summary>
        public string TowerIconPath => m_TowerIconPath;
        /// <summary>
        /// 排序层级
        /// </summary>
        protected int m_OrderInLayer = 0;
        /// <summary>
        /// 排序层级
        /// </summary>
        public int OrderInLayer => m_OrderInLayer;              
        /// <summary>
        /// 建造所需要的钱
        /// </summary>
        protected int[] m_BuilderMoney = null;
        /// <summary>
        /// 获取建造所需要的钱
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int GetBuilderMoney(int grade)
        {
            return m_BuilderMoney[Mathf.Clamp(grade, 0, m_BuilderMoney.Length-1)];
        }
        /// <summary>
        /// 攻击频率 
        /// </summary>
        protected float[] m_AttackFrequency = null;
        /// <summary>
        /// 获取攻击频率
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public float GetAttackFrequency(int grade)
        {
            return m_AttackFrequency[Mathf.Clamp(grade, 0, m_AttackFrequency.Length-1)];
        }
        /// <summary>
        /// 攻击距离
        /// </summary>
        protected float[] m_AttackDis = null;
        /// <summary>
        /// 获取攻击距离
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public float GetAttackDis(int grade)
        {
            return m_AttackDis[Mathf.Clamp(grade, 0, m_AttackDis.Length-1)];
        }
        /// <summary>
        /// 攻击值
        /// </summary>
        protected int[] m_AttackValue = null;
        /// <summary>
        /// 获取攻击值
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int GetAttackValue(int grade)
        {
            return m_AttackValue[Mathf.Clamp(grade, 0, m_AttackValue.Length-1)];
        }
        /// <summary>
        /// 子弹飞行速率
        /// </summary>
        protected float[] m_ShellFlySpeed = null;
        /// <summary>
        /// 获取子弹飞行速率
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public float GetShellFlySpeed(int grade)
        {
            return m_ShellFlySpeed[Mathf.Clamp(grade, 0, m_ShellFlySpeed.Length-1)];
        }        
        /// <summary>
        /// 拆卸所获得的金钱
        /// </summary>
        protected int[] m_TeardownMoney = null;
        /// <summary>
        /// 获取拆卸所获得的金钱
        /// </summary>
        public int GetTeardownMoney(int grade)
        {
            return (int)(GetBuilderMoney(Mathf.Clamp(grade, 0, m_BuilderMoney.Length-1)) * 0.8f);
        }
        /// <summary>
        /// 塔台音效路径
        /// </summary>
        protected string m_TowerAudioPath = null;
        /// <summary>
        /// 塔台音效路径
        /// </summary>
        public string TowerAudioPath => m_TowerAudioPath;
        /// <summary>
        /// 初始化配置
        /// </summary>
        public virtual void InitConfig()
        {

        }
    }
    [LuaCallCSharp]
    /// <summary>
    /// 瓶子塔台配置
    /// </summary>
    public class BottleTowerConfig:TowerConfig
    {      
        public override void InitConfig()
        {
            base.InitConfig();
            m_TowerIconPath = "123";
            m_BuilderMoney = new int[3] {100,180, 260 };
            m_AttackDis = new float[3] { 1.2f, 1.6f, 2.0f };
            m_AttackValue = new int[3] { 1, 2, 3 };
            m_ShellFlySpeed = new float[3] { 5, 6, 7 };
            m_AttackFrequency = new float[3] { 1f, 0.75f, 0.5f };
        }
    }

}
