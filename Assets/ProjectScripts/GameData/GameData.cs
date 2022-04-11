using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace DTR.Data
{
    /// <summary>
    /// 游戏数据
    /// </summary>
    [Serializable]
    public class GameData
    {
        /// <summary>
        /// 关卡模式数据容器
        /// </summary>
        public Dictionary<string, LevelModelData> LevelModelDataDic = new Dictionary<string, LevelModelData>();



    } 
    /// <summary>
    /// 关卡模式数据
    /// </summary>
    public class LevelModelData
    {
        /// <summary>
        /// 锁
        /// </summary>
        public bool Lock = true;
        /// <summary>
        /// 关卡主题数据容器
        /// </summary>
        public Dictionary<string, LevelThemeData> LevelThemeDataDic = new Dictionary<string, LevelThemeData>();
    }
    [Serializable]
    /// <summary>
    /// 关卡主题数据
    /// </summary>
    public class LevelThemeData
    {
        /// <summary>
        /// 锁
        /// </summary>
        public bool Lock = true;     
        /// <summary>
        /// 关卡数据容器
        /// </summary>
        public Dictionary<int, LevelData> LevelDataDic = new Dictionary<int, LevelData>();
    }
    [Serializable]
    /// <summary>
    /// 关卡数据
    /// </summary>
    public class LevelData
    {
        /// <summary>
        /// 锁
        /// </summary>
        public bool Lock = true;
        /// <summary>
        /// 索引
        /// </summary>
        public int Index = 0;
        /// <summary>
        /// 金币数量
        /// </summary>
        public int Gold = 0;
        /// <summary>
        /// 怪物波数
        /// </summary>
        public int MonsterWaveNumber = 0;     
        /// <summary>
        /// 格子数据容器
        /// </summary>
        public List<GridData> GridDataLi=new List<GridData>();
        /// <summary>
        /// 路径数据
        /// </summary>
        public PathData PathData = new PathData();
    }
    [Serializable]
    /// <summary>
    /// 路径数据
    /// </summary>
    public class PathData
    {
        /// <summary>
        /// int[](在网格数组中的索引)Index[0]对应横向(左~右)Index[1]对应纵向(下~上)  网格索引容器
        /// </summary>
        public List<int[]> IndexLi=new List<int[]>();
        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            IndexLi.Clear();
        }
    }
    [Serializable]
    /// <summary>
    /// 格子数据
    /// </summary>
    public class GridData
    {
        /// <summary>
        /// 索引
        /// </summary>
        public int[] Index = new int[2];
        /// <summary>
        /// 格子类型
        /// </summary>
        public EnumGrid GridType = EnumGrid.Empty;
    }
    /// <summary>
    /// 关卡依赖数据
    /// </summary>
    public class LevelRelyData
    {     
        /// <summary>
        /// 关卡模式
        /// </summary>
        public string LevelModel = "";     
        /// <summary>
        /// 关卡主题
        /// </summary>
        public string LevelTheme = "";
    }
}
