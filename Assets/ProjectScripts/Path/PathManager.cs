using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTR.MapGrid;
using DTR.Data;
namespace DTR.Path
{
    /// <summary>
    /// 路径管理
    /// </summary>
    public class PathManager 
    {
        /// <summary>
        /// 路径长度
        /// </summary>
        public static int PathLength => m_PathData.IndexLi.Count;
        /// <summary>
        /// 当前路径的头
        /// </summary>
        public static IGrid Head => GetGrid(0);
        /// <summary>
        /// 当前路径的尾
        /// </summary>
        public static IGrid Tail => GetGrid(m_PathData.IndexLi.Count-1);
        /// <summary>
        /// 路径数据
        /// </summary>
        private static PathData m_PathData =new PathData();
        /// <summary>
        /// 路径数据
        /// </summary>
        public static PathData PD => m_PathData;
        /// <summary>
        /// 加载当前关卡的路径数据
        /// </summary>
        public static void LoadNowLevelPathData()
        {
            if(GameManager.NowLevelData!=null)
            {
                AddPathData(GameManager.NowLevelData.PathData);              
            }
        }
        /// <summary>
        /// 添加路径数据(自动移除之前添加的数据)
        /// </summary>
        /// <param name="pathData"></param>
        public static void AddPathData(PathData pathData)
        {
            m_PathData.Clear();
            List<int[]> indexLi = pathData.IndexLi;
            foreach (int[] index in indexLi)
            {
                m_PathData.IndexLi.Add(index);
            }
        }
        /// <summary>
        /// 该网格所对应路径中的索引
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int GridIndexOf(IGrid grid)
        {
            for(int index=0;index<m_PathData.IndexLi.Count;index++)
            {
                if (GetGrid(index) == grid)
                {
                    return index;
                }
            }
            return -1;
        }
        /// <summary>
        /// 获取网格
        /// </summary>
        /// <param name="index">路径中网格的索引</param>
        /// <returns></returns>
        public static IGrid GetGrid(int index)
        {
            if (m_PathData.IndexLi.Count>index)
            {
                return GetGrid(m_PathData.IndexLi[index]);
            }
            return null;
        }
        /// <summary>
        /// 获取网格
        /// </summary>
        /// <param name="index">网格组中网格的索引</param>
        /// <returns></returns>
        private static IGrid GetGrid(int[] index)
        {
            return GridManager.GridLi.Find((grid) =>
            {
                return (grid.Index[0] == index[0]) && (grid.Index[1] == index[1]);
            });
        }
    }
}
