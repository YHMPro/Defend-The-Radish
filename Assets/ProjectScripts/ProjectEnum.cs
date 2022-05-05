
using System;
namespace DTR
{ 
    /// <summary>
    /// 标签类型
    /// </summary>
    public enum EnumTag
    {
        /// <summary>
        /// 静态
        /// </summary>
        Static,
        /// <summary>
        /// 动态
        /// </summary>
        Dynamic
    }  
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
        /// <summary>
        /// 绵羊
        /// </summary>
        Sheep,
        /// <summary>
        /// 独眼
        /// </summary>
        SingleEye,
        /// <summary>
        /// 漂浮
        /// </summary>
        Flotage,
        /// <summary>
        /// 海星
        /// </summary>
        Starfish,

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
