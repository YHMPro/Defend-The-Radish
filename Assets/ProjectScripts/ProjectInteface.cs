
using Farme;
using UnityEngine;
using UnityEngine.Events;
namespace DTR
{
    public interface ITag : IInterfaceBase
    {      
        /// <summary>
        /// 标签类型
        /// </summary>
        EnumTag TagType { get; }
    }
    /// <summary>
    /// 升级标签接口
    /// </summary>
    public interface IUpgradeTag: ITag
    {
        /// <summary>
        /// 升级标签UI的屏幕全局坐标
        /// </summary>
        Vector3 UpgradeTagPosition { get; }
    }
    /// <summary>
    /// 可被攻击
    /// </summary>
    public interface IAssaultable:ITag
    {
        /// <summary>
        /// 攻击标记屏幕全局坐标
        /// </summary>
        Vector3 AttackTagPosition { get; }
        /// <summary>
        /// 血条屏幕全局坐标
        /// </summary>
        Vector3 BloodPosition { get; }
        /// <summary>
        /// 添加死亡监听
        /// </summary>
        /// <param name="ua"></param>
        void AddDiedListen(UnityAction ua);
        /// <summary>
        /// 移除死亡监听
        /// </summary>
        /// <param name="ua"></param>
        void RemoveDiedListen(UnityAction ua);
        /// <summary>
        /// 添加血量变化监听
        /// </summary>
        /// <param name="ua"></param>
        void AddBloodChangeListen(UnityAction<float> ua);
        /// <summary>
        /// 移除血条变化监听
        /// </summary>
        /// <param name="ua"></param>
        void RemoveBloodChangeListen(UnityAction<float> ua);
    }
    /// <summary>
    /// 炮弹接口
    /// </summary>
    public interface IShell: IInterfaceBase
    {
        /// <summary>
        /// 炮弹信息
        /// </summary>
        ShellInfo ShellInfo { get; }
    }
    /// <summary>
    /// 回收接口
    /// </summary>
    public interface IRecycle: IInterfaceBase
    {
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="isDestroy">是否销毁</param>
        void Recycle(bool isDestroy=false);
    }
    public interface ITower:IInterfaceBase
    {
        /// <summary>
        /// 塔台类型
        /// </summary>
        EnumTower TowerType { get; }
        /// <summary>
        /// 塔台等级
        /// </summary>
        int TowerGrade { get; }
        /// <summary>
        /// 塔台升级
        /// </summary>
        void TowerUpgrade();
        /// <summary>
        /// 塔台卸载
        /// </summary>
        void TowerTeardown();
    }
    public interface IGrid : IInterfaceBase
    {
        /// <summary>
        /// 摆放的物体
        /// </summary>
        GameObject PutObj { get; set; }
        /// <summary>
        /// 网格类型
        /// </summary>
        EnumGrid GridType { get; set; }      
        /// <summary>
        /// 索引(在网格组中网格的索引)Index[0]对应横向(左~右)Index[1]对应纵向(下~上)
        /// </summary>
        int[] Index { get; set; }      
        /// <summary>
        /// 基于Unity的世界坐标
        /// </summary>
        Vector3 Position
        {
            get;set;
        }
    }
}