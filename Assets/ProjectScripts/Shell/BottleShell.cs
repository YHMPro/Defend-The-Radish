using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Extend;
using DTR.Tower;
namespace DTR.Shell
{
    /// <summary>
    /// 瓶子塔台炮弹
    /// </summary>
    public class BottleShell : Shell
    {
        protected override void Awake()
        {
            base.Awake();
            m_Co = GetComponent<CircleCollider2D>();
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if(MonoSingletonFactory<ShareMono>.SingletonExist)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);

            }
        }
        protected override void MoveUpdate()
        {
            if (m_ShellInfo.FollowMonster != null&& m_ShellInfo.FollowMonster.gameObject.activeInHierarchy)
            {
                Vector3 flyDir = (m_ShellInfo.FollowMonster.Center.position - transform.position).normalized;
                transform.Translate(flyDir * Time.deltaTime * m_ShellInfo.FlySpeed, Space.World);
                transform.eulerAngles = Vector2.SignedAngle(Vector2.right, flyDir) * Vector3.forward;
            }
            else
            {
                //暂时使用这种方式进行屏蔽
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);
                m_Co.enabled = false;
                gameObject.Recycle(gameObject.name);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_ShellInfo.FollowMonster != null)
            {
                if (Equals(collision.GetComponent<Monster.Monster>(), m_ShellInfo.FollowMonster))
                {
                    m_Co.enabled = false;
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);
                    gameObject.Recycle(gameObject.name);
                    if (!GoReusePool.Take("BottleShellBoom", out GameObject boom))
                    {
                        if (!GoLoad.Take("Prefabs/Boom/BottleShellBoom", out boom))
                        {
                            return;
                        }
                    }
                    boom.transform.position = collision.GetComponent<Monster.Monster>().Center.position;
                }
            }
            else
            {
                m_Co.enabled = false;
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.MoveUpdate);
                gameObject.Recycle(gameObject.name);
            }
        }
        
    }
}
