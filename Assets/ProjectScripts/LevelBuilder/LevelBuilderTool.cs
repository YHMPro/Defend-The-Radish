using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DTR.Data;
using Farme;
using DTR.MapGrid;
using DTR.Path;
namespace DTR.LevelBuilder
{
    /// <summary>
    /// 关卡构建工具
    /// </summary>
    public class LevelBuilderTool 
    {

        public static LevelRelyData RelyData = new LevelRelyData();
        public static bool LevelLock = true;
        public static int LevelIndex = 1;
        [MenuItem("LevelBuilder/ReadGameData")]
        public static void ReadGameData()
        {
            GameDataManager.ReadData();
        }
        [MenuItem("LevelBuilder/BuilderNowLevelData")]
        public static void BuilderNowLevelData()
        {                   
            LevelData levelData = new LevelData();
            levelData.Lock = LevelLock;
            levelData.Index = LevelIndex;        
            levelData.PathData.IndexLi = PathManager.PD.IndexLi;
            foreach (var grid in GridManager.GridLi)
            {
                GridData gd = new GridData();
                gd.GridType = grid.GridType;
                gd.Index = grid.Index;
                levelData.GridDataLi.Add(gd);
            }
            GameDataManager.AddLevelData(RelyData,levelData);
        }
        [MenuItem("LevelBuilder/DeleteNowLevelData")]
        public static void DeleteNowLevelData()
        {
            GameDataManager.RemoveLevelData(RelyData, LevelIndex);
        }
        [MenuItem("LevelBuilder/SaveGameData")]
        public static void SaveGameData()
        {
            GameDataManager.SaveData();
        }
    }
    
}
