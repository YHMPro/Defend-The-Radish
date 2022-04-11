using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Shell
{
    /// <summary>
    /// 瓶子塔台炮弹
    /// </summary>
    public class BottleCannon_Shell : MonoBehaviour
    {
        /// <summary>
        /// 炮弹等级
        /// </summary>
        private int m_ShellGrade = 1;
        /// <summary>
        /// 碰撞盒
        /// </summary>
        private BoxCollider2D m_Co;
        /// <summary>
        /// 飞行方向
        /// </summary>
        private Vector2 m_FlyDir;
        /// <summary>
        /// 飞行速率
        /// </summary>
        private float m_Speed;


        public void SetFlyDir(Vector2 dir,float speed)
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.FlyUpdate);
        }
        private void FlyUpdate()
        {
            //transform.Translate()
        }




    }
}
