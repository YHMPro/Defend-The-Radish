using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Monster
{
    /// <summary>
    /// 海星
    /// </summary>
    public class Starfish : Monster
    {
        protected override void Awake()
        {
            m_MonsterType = EnumMonster.Starfish;
            base.Awake();
        }


       
    }
}
