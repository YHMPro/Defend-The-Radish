using System.Collections.Generic;
using UnityEngine.Events;
using Farme;
using UnityEngine;
using DTR.Tower;
using DTR.Data;
namespace DTR
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public class GameManager 
    {
        /// <summary>
        /// 关卡依赖数据
        /// </summary>
        private static LevelRelyData m_RelyData=new LevelRelyData();
        /// <summary>
        /// 关卡依赖数据
        /// </summary>
        public static LevelRelyData RelyData => m_RelyData;
        /// <summary>
        /// 当前关卡索引
        /// </summary>
        private static int m_NowLevelIndex = 1;
        /// <summary>
        /// 当前关卡数据
        /// </summary>
        public static LevelData NowLevelData
        {
            get
            {                      
                return GameDataManager.GetLevelData(m_RelyData, m_NowLevelIndex);
            }
        }



        private static Dictionary<int[], Tower.Tower> m_TowerDic = new Dictionary<int[], Tower.Tower>();
        #region TowerBuilder
        /// <summary>
        /// 塔台建造
        /// </summary>
        /// <param name="index">绑定的地图网格索引</param>
        /// <param name="towerType">塔台类型</param>
        /// <param name="callback">回调</param>
        public static void TowerBuilder(int[] index, EnumTower towerType,UnityAction<GameObject> callback)
        {
            if(!GoReusePool.Take(towerType.ToString(),out GameObject tower))
            {
                if(!GoLoad.Take("Prefabs/Tower/"+towerType.ToString(),out tower))
                {
                    return;
                }
            }
            m_TowerDic.Add(index, tower.GetComponent<Tower.Tower>());
            callback?.Invoke(tower);
        }
     
        private static bool m_IsGameRun = true;
        /// <summary>
        /// 游戏是否运行
        /// </summary>
        public static bool IsGameRun => m_IsGameRun;
        #endregion

        #region GameStateControl
        /// <summary>
        /// 游戏状态控制
        /// </summary>
        /// <param name="gameState">游戏状态</param>
        public static void GameStateControl(EnumGameState gameState)
        {       
            switch (gameState)
            {
                case EnumGameState.Run:
                    {
                        m_IsGameRun = true;
                        break;
                    }
                case EnumGameState.Pause:
                    {
                        m_IsGameRun = false;
                        break;
                    }
                
            }
        }
        /// <summary>
        /// 游戏开始
        /// </summary>
        public static void GameStart()
        {

        }
        /// <summary>
        /// 游戏重置(指当前关卡重置而不是游戏执行程序)
        /// </summary>
        public static void GameReset()
        {

        }
        #endregion

        #region Scene
        /// <summary>
        /// 场景跳转
        /// </summary>
        public static void SceneSkip(EnumScene sceneType,UnityAction callback)
        {

        }
        private static void UnloadScene()
        {

        }
        #endregion
    }
}
