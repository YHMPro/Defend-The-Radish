
using Farme;
using UnityEngine;
namespace DTR
{
    public interface IGrid : IInterfaceBase
    {
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