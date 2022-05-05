using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DTR.Config
{
    /// <summary>
    /// 怪物配置
    /// </summary>
    public class MonsterConfig 
    {
        /// <summary>
        /// 怪物配置容器
        /// </summary>
        private static Dictionary<EnumMonster, MonsterConfig> m_MonsterConfigDic = new Dictionary<EnumMonster, MonsterConfig>();
        /// <summary>
        /// 获取怪物配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="monsterType"></param>
        /// <returns></returns>
        private static T GetMonsterConfig<T>(EnumMonster monsterType)where T:MonsterConfig,new()
        {
            if(!m_MonsterConfigDic.TryGetValue(monsterType, out MonsterConfig monsterConfig))
            {
                monsterConfig = new T();
                monsterConfig.InitConfig();
                m_MonsterConfigDic.Add(monsterType, monsterConfig);
            }
            return (T)monsterConfig;
        }
        /// <summary>
        /// 获取怪物配置
        /// </summary>
        /// <param name="monsterType">怪物类型</param>
        /// <returns></returns>
        public static MonsterConfig GetMonsterConfig(EnumMonster monsterType)
        {
            MonsterConfig monsterConfig = null;
            switch (monsterType)
            {
                case EnumMonster.Sheep:
                    {
                        monsterConfig = GetMonsterConfig<SheepMonsterConfig>(monsterType);
                        break;
                    }
                case EnumMonster.SingleEye:
                    {
                        monsterConfig = GetMonsterConfig<SingleEyeMonsterConfig>(monsterType);
                        break;
                    }
                case EnumMonster.Flotage:
                    {
                        monsterConfig = GetMonsterConfig<FlotageMonsterConfig>(monsterType);
                        break;
                    }
                case EnumMonster.Starfish:
                    {
                        monsterConfig = GetMonsterConfig<StarfishMonsterConfig>(monsterType);
                        break;
                    }
            }
            return monsterConfig;
        }
        /// <summary>
        /// 血量
        /// </summary>
        protected int m_Blood = 0;
        /// <summary>
        /// 血量
        /// </summary>
        public int Blood => m_Blood;
        /// <summary>
        /// 移动速率
        /// </summary>
        protected float m_MoveSpeed = 0;
        public float MoveSpeed => m_MoveSpeed;
        /// <summary>
        /// 奖励的钱
        /// </summary>
        protected int m_Money = 0;
        /// <summary>
        /// 奖励的钱
        /// </summary>
        public int Money => m_Money;
        /// <summary>
        /// 消耗的萝卜数
        /// </summary>
        protected int m_ConsumeRadish = 0;
        /// <summary>
        /// 消耗的萝卜数
        /// </summary>
        public int ConsumeRadish => m_ConsumeRadish;
        public virtual void InitConfig()
        {

        }
    }


    /// <summary>
    /// 绵羊怪物配置
    /// </summary>
    public class SheepMonsterConfig:MonsterConfig
    {
        public override void InitConfig()
        {
            base.InitConfig();
            m_Blood = 5;
            m_MoveSpeed = 0.5f;
            m_Money = 14;
            m_ConsumeRadish = 1;
        }
    }
    /// <summary>
    /// 独眼怪物配置
    /// </summary>
    public class SingleEyeMonsterConfig: MonsterConfig
    {
        public override void InitConfig()
        {
            base.InitConfig();
            m_Blood = 8;
            m_MoveSpeed = 0.5f;
            m_Money = 14;
            m_ConsumeRadish = 1;
        }
    }
    /// <summary>
    /// 漂浮怪物配置
    /// </summary>
    public class FlotageMonsterConfig: MonsterConfig
    {
        public override void InitConfig()
        {
            base.InitConfig();
            m_Blood = 5;
            m_MoveSpeed = 1.5f;
            m_Money = 14;
            m_ConsumeRadish = 1;
        }
    }
    /// <summary>
    /// 海星怪物配置
    /// </summary>
    public class StarfishMonsterConfig: MonsterConfig
    {
        public override void InitConfig()
        {
            base.InitConfig();
            m_Blood = 5;
            m_MoveSpeed = 0.5f;
            m_Money = 14;
            m_ConsumeRadish = 2;
        }
    }
}
