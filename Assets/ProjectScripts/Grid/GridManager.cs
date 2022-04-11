using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Farme;
using DTR.Data;
namespace DTR.MapGrid
{
    /// <summary>
    /// 网格管理器
    /// </summary>
    public class GridManager
    {
        public static readonly string OperationGridEvent = "OperationGridEvent";
        private static GameObject m_GridManagerGo = null;
        private static int[] m_GridSize = new int[] { 12, 7 };
        /// <summary>
        /// 大小-> m_GridSize[0]:宽  m_GridSize[1]:高
        /// </summary>
        public static int[] GridSize => m_GridSize; 
        private static IGrid m_NowOperationGrid = null;
        /// <summary>
        /// 当前被操作的网格
        /// </summary>
        public static IGrid NowOperationGrid => m_NowOperationGrid;
        private static List<IGrid> m_GridLi = new List<IGrid>();
        /// <summary>
        /// 网格容器
        /// </summary>
        public static List<IGrid> GridLi => m_GridLi;
        /// <summary>
        /// 偏移量
        /// </summary>
        public static Vector2 Offset = new Vector2 { x = -4.4f, y = -2.75f };
        /// <summary>
        /// 相邻格子之间的距离
        /// </summary>
        public static  float m_Distance = 0.8f;
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            if(m_GridManagerGo==null)
            {
                m_GridManagerGo = new GameObject("GridManager");
                MesgManager.MesgListen<IGrid>(OperationGridEvent, OnOperationGrid);
            }
            CreateGridGroup(() => 
            {
                Update();
                //MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,Update);
            });      
        }
        /// <summary>
        /// 更新
        /// </summary>
        private static void Update()
        {
            foreach (var grid in m_GridLi)
            {
                grid.Position = Offset + new Vector2(grid.Index[0] * m_Distance, grid.Index[1] * m_Distance);
            }
        }
        /// <summary>
        /// 创建网格组
        /// </summary>
        /// <param name="callback"></param>
        private static void CreateGridGroup(UnityAction callback = null)
        {
            GameObject grid;
            for (int y = 0; y < m_GridSize[1]; y++)
            {
                for (int x = 0; x < m_GridSize[0]; x++)
                {
                    if (!GoReusePool.Take("Grid", out grid))
                    {
                        //采用Resources加载
                        if (!GoLoad.Take("Prefabs/Grid", out grid, m_GridManagerGo.transform))
                        {
                            continue;
                        }
                    }
                    SetGridData(x,y,grid.GetComponent<IGrid>());            
                }
            }
            callback?.Invoke();
        }
        /// <summary>
        /// 设置网格数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="grid"></param>
        private static void SetGridData(int x,int y,IGrid grid)
        {
            if(GameManager.NowLevelData==null)
            {
                grid.Index[0] = x;
                grid.Index[1] = y;
            }
            else
            {
                GridData gridData = GameManager.NowLevelData.GridDataLi[m_GridLi.Count];
                grid.Index[0] = gridData.Index[0];
                grid.Index[1] = gridData.Index[1];
                grid.GridType = gridData.GridType;
            }           
            m_GridLi.Add(grid);
        }
        #region Event
        /// <summary>
        /// 监听格子操作事件(格子被操作时自动调用)
        /// </summary>
        private static void OnOperationGrid(IGrid grid)
        {
            if (!Equals(m_NowOperationGrid, grid))
            {
                m_NowOperationGrid = grid;
            }
        }
        #endregion
    }
}