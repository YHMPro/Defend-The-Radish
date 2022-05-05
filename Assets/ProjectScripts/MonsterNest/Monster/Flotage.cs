using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Monster
{
    /// <summary>
    /// 漂浮
    /// </summary>
    public class Flotage : Monster
    {
        protected override void Awake()
        {
            m_MonsterType = EnumMonster.Flotage;
            base.Awake();
        }    
    }
}
