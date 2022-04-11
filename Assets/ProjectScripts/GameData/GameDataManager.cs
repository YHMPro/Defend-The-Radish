using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Farme.Tool;
using DTR.LevelBuilder;
namespace DTR.Data
{
    /// <summary>
    /// 游戏数据管理
    /// </summary>
    public class GameDataManager 
    {
        /// <summary>
        /// 游戏数据文件路径
        /// </summary>
        private static readonly string m_GameDataFilePath = Application.streamingAssetsPath + @"\GameData.json";
        /// <summary>
        /// 游戏数据实例
        /// </summary>
        private static GameData m_GameData = null;
        /// <summary>
        /// 添加关卡数据
        /// </summary>
        /// <param name="relyData">关卡依赖数据</param>
        /// <param name="levelData">关卡数据</param>
        public static void AddLevelData(LevelRelyData relyData, LevelData levelData)
        {
            if(m_GameData==null)
            {
                m_GameData = new GameData();
            }
            if(!m_GameData.LevelModelDataDic.TryGetValue(relyData.LevelModel, out LevelModelData levelModelData))
            {
                levelModelData = new LevelModelData();
                levelModelData.Lock = true;
                m_GameData.LevelModelDataDic.Add(relyData.LevelModel, levelModelData);
            }
            if(!levelModelData.LevelThemeDataDic.TryGetValue(relyData.LevelTheme, out LevelThemeData levelThemeData))
            {
                levelThemeData=new LevelThemeData();
                levelThemeData.Lock = true;
                levelModelData.LevelThemeDataDic.Add(relyData.LevelTheme, levelThemeData);
            }
            if (!levelThemeData.LevelDataDic.ContainsKey(levelData.Index))
            {
                levelThemeData.LevelDataDic.Add(levelData.Index, levelData);
            }
            else
            {
                levelThemeData.LevelDataDic[levelData.Index] = levelData;
            }
            Debuger.Log("添加关卡数据成功!!!");
        }
        /// <summary>
        /// 获取关卡数据
        /// </summary>
        /// <param name="relyData">关卡依赖数据</param>  
        /// <param name="index">关卡索引</param>  
        /// <returns></returns>
        public static LevelData GetLevelData(LevelRelyData relyData,int index)
        {
            if (m_GameData != null)
            {
                if (m_GameData.LevelModelDataDic.TryGetValue(relyData.LevelModel, out LevelModelData levelModelData))
                {
                    if (levelModelData.LevelThemeDataDic.TryGetValue(relyData.LevelTheme, out LevelThemeData levelThemeData))
                    {
                        if (levelThemeData.LevelDataDic.ContainsKey(index))
                        {
                            return levelThemeData.LevelDataDic[index];
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 移除关卡数据
        /// </summary>
        /// <param name="relyData">关卡依赖数据</param>  
        /// <param name="index">关卡索引</param>  
        public static void RemoveLevelData(LevelRelyData relyData, int index)
        {
            if (m_GameData != null)
            {
                if (m_GameData.LevelModelDataDic.TryGetValue(relyData.LevelModel, out LevelModelData levelModelData))
                {
                    if (levelModelData.LevelThemeDataDic.TryGetValue(relyData.LevelTheme, out LevelThemeData levelThemeData))
                    {
                        if (levelThemeData.LevelDataDic.ContainsKey(index))
                        {
                            levelThemeData.LevelDataDic.Remove(index);
                        }
                    }
                }
            }
                    
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        public static void SaveData()
        {
            if (File.Exists(m_GameDataFilePath))
            {
                string jsonData = JsonConvert.SerializeObject(m_GameData);
                File.WriteAllText(m_GameDataFilePath, jsonData, Encoding.UTF8);
                Debuger.Log("游戏数据保存成功!\nFilePath:" + m_GameDataFilePath+"\nFileContent:\n"+jsonData);
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        public static void ReadData()
        {
            if (!File.Exists(m_GameDataFilePath))
            {            
                using (FileStream fs = File.Create(m_GameDataFilePath))
                {
                    Debuger.Log("游戏数据文件创建成功!\nFilePath:" + m_GameDataFilePath);
                }           
            }
            else
            {
                string jsonData = File.ReadAllText(m_GameDataFilePath, Encoding.UTF8);
                m_GameData = JsonConvert.DeserializeObject<GameData>(jsonData);
                Debuger.Log("游戏数据读取成功!\nFilePath:" + m_GameDataFilePath + "\nFileContent:\n" + jsonData);
            }
        }

      
    }
}
