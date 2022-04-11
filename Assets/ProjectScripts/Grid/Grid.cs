using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.UI;
using DTR.UI;
using Farme.Extend;
namespace DTR.MapGrid
{
    /// <summary>
    /// 网格
    /// </summary>
    public class Grid : BaseMono, IGrid
    {
        private GameObject m_Tag = null;
        public GameObject Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
        private EnumGrid m_GridType = EnumGrid.Empty;
        public EnumGrid GridType
        {
            get { return m_GridType; }
            set { m_GridType = value; }
        }
        private int m_PathIndex = -1;
        public int PathIndex
        {
            get
            {
                return m_PathIndex;
            }
            set
            {
                m_PathIndex = value;
            }
        }
        private int[] m_Index = new int[2];
        public int[] Index
        {
            get
            {
                return m_Index;
            }
            set
            {
                m_Index = value;
            }
        }
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position= value;
            }
        }     
        protected override void Awake()
        {
            InterfaceManager.AddInterface(GetType().Name, this);
            base.Awake();
           
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();         
        }
        protected override void OnDestroy()
        {
            InterfaceManager.RemoveInterface(GetType().Name, this);
            base.OnDestroy();
        }
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

            MesgManager.MesgTirgger<IGrid>(GridManager.OperationGridEvent,this);

            #region LevelBuilder
            //if (m_GridType == EnumGrid.Empty)
            //{
            //    MesgManager.MesgTirgger("OnPathSetRect");
            //}
            //else
            //{
            //    m_Tag.Recycle("Tag");
            //    m_Tag = null;
            //    m_GridType = EnumGrid.Empty;
            //    m_PathIndex = -1;
            //}
            #endregion
            if (InterfaceManager.GetInterfaceLi(GetType().Name,out List<IGrid> mapGridLi))
            {
                
            }
            if(windowRoot.GetWindow("GameSceneWindow_ScreenSpaceCamera", out StandardWindow gsWindow))
            {
                if(gsWindow.GetPanel("GameAssistPanel",out GameAssistPanel gAPanel))
                {                  
                    gAPanel.SetState(EnumPanelState.Show);
                }
            }       
        }
    }
}
