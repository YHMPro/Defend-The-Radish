using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Tower
{
    /// <summary>
    /// 瓶子塔台
    /// </summary>
    public class BottleTower : Tower
    {
        
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<SpriteRenderer>();
            m_Anim=GetComponent<Animator>();
            m_Spr = GetComponent<SpriteRenderer>("Spr");
        }

        protected virtual void Update()
        {
            //if(Input.GetMouseButtonDown(0))
            //{
            //    if (TowerSprs != null)
            //    {
            //        m_Spr.sprite = TowerSprs[m_TowerGrade - 1];
            //    }
            //    TriggerAttack();
            //}
        }

        


    }
}
