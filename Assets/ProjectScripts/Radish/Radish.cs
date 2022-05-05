using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.UI;
namespace DTR
{
    /// <summary>
    /// 萝卜
    /// </summary>
    public class Radish : BaseMono
    {
        public Sprite[] HurtSpr =new Sprite[6];
        private Animator m_Anim;



        private void OnMouseDown()
        {
            if (!MonoSingletonFactory<WindowRoot>.SingletonExist)
            {
                return;
            }
            WindowRoot windowRoot = MonoSingletonFactory<WindowRoot>.GetSingleton();
            if (windowRoot.ES.IsPointerOverGameObject())//当操作对象是UI时则屏蔽此次事件响应
            {
                return;
            }
            m_Anim.SetTrigger("Click");
        }

        protected override void Awake()
        {
            base.Awake();
            m_Anim = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            m_Anim.updateMode = AnimatorUpdateMode.UnscaledTime;//设置为不受TimeScale影响
        }
    }
}
