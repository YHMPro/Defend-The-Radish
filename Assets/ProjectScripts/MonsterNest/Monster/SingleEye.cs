using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Monster
{
    /// <summary>
    /// 独眼
    /// </summary>
    public class SingleEye : Monster
    {
        protected override void Awake()
        {
            m_MonsterType = EnumMonster.SingleEye;
            base.Awake();
        }


    }
}
