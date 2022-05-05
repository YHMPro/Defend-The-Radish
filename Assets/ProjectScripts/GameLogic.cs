using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
namespace DTR
{
    /// <summary>
    /// 游戏逻辑处理器
    /// </summary>
    public class GameLogic
    {       
        /// <summary>
        /// 钱刷新事件
        /// </summary>
        public static readonly string MoneyRefreshEvent = "MoneyRefreshEvent";
        private static int m_MoneyTatal = 0;
        /// <summary>
        /// 钱的总数
        /// </summary>
        public static int MoneyTatal => m_MoneyTatal;
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
        }
        /// <summary>
        /// 重置
        /// </summary>
        public static void Reset()
        {
            m_MoneyTatal = 0;
        }
        /// <summary>
        /// 清除
        /// </summary>
        public static void Clear()
        {
            Reset();      
        }      
        /// <summary>
        /// 拿取钱
        /// </summary>
        /// <param name="money"></param>
        public static void TakeMoney(int money)
        {
            m_MoneyTatal -= money;
            MoneyRefresh();
        }
        /// <summary>
        /// 放入钱
        /// </summary>
        /// <param name="money"></param>
        public static void PutMoney(int money)
        {
            m_MoneyTatal += money;
            MoneyRefresh();
        }
        /// <summary>
        /// 钱数量更改时调用
        /// </summary>
        private static void MoneyRefresh()
        {
            Debuger.Log("钱的总数:" + m_MoneyTatal);
            MesgManager.MesgTirgger(MoneyRefreshEvent);
        }


    }
}
