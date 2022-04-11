
using System;
namespace DTR
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum EnumGameState
    {
        /// <summary>
        /// 运行
        /// </summary>
        Run,
        /// <summary>
        /// 停止
        /// </summary>
        Pause
    }
    /// <summary>
    /// 场景类型
    /// </summary>
    public enum EnumScene
    {

    }
    [Serializable]
    /// <summary>
    /// 怪物类型
    /// </summary>
    public enum EnumMonster
    {

    }
    [Serializable]
    /// <summary>
    /// 塔台类型
    /// </summary>
    public enum EnumTower
    {
        /// <summary>
        /// 瓶子塔台
        /// </summary>
        BottleTower
    }
    [Serializable]
    /// <summary>
    /// 网格类型
    /// </summary>
    public enum EnumGrid
    {
        /// <summary>
        /// 空
        /// </summary>
        Empty,
        /// <summary>
        /// 塔台
        /// </summary>
        Tower,
        /// <summary>
        /// 障碍物
        /// </summary>
        Barrier,
        /// <summary>
        /// 路径
        /// </summary>
        Path
    }
}
