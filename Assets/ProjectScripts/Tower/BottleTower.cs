using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using DTR.Shell;
namespace DTR.Tower
{
    /// <summary>
    /// 瓶子塔台
    /// </summary>
    public class BottleTower : Tower
    {
        private Transform m_PointOfRotate;
        protected override void Awake()
        {
            m_TowerType = EnumTower.BottleTower;
            base.Awake();           
            m_PointOfRotate =GetComponent<Transform>("PointOfRotate");
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_PointOfRotate.localEulerAngles = Vector3.zero;
        }
        protected override void AttackCheckUpdate()
        {
            if (m_AttackMonsterLi.Count!=0)
            {
                Vector3 monsterPos = m_AttackMonsterLi[0].Center.position;
                m_PointOfRotate.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, (monsterPos - transform.position).normalized));
                base.AttackCheckUpdate();
            }       
        }

        protected override void OnAttack()
        {
            base.OnAttack();
            if(!GoReusePool.Take("BottleShell",out GameObject shell))
            {
                if(!GoLoad.Take("Prefabs/Shell/BottleShell",out shell))
                {
                    return;
                }
            }
            shell.transform.position = m_ShootPoint.position;
            shell.transform.eulerAngles = new Vector3(0, 0, m_PointOfRotate.localEulerAngles.z+90f);
            shell.GetComponent<BottleShell>().BindTower = this;
        }

    }
}
