using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Monster
{
    /// <summary>
    /// 羊
    /// </summary>
    public class Sheep : Monster
    {
        protected override void Awake()
        {
            m_MonsterType = EnumMonster.Sheep;
            base.Awake();
        }


       
    }
}
