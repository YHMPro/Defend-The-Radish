using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using Farme;
using DTR.MapGrid;
using DTR.Data;
using DTR.Path;
namespace DTR.LevelBuilder
{
    /// <summary>
    /// 关卡建造面板
    /// </summary>
    public class LevelBuilderPanel : BasePanel
    {
        private RectTransform m_PathSetRect;
        public RectTransform PathSetRect => m_PathSetRect;
        private ElasticBtn m_HeadBtn;
        private ElasticBtn m_TailBtn;
        private ElasticBtn m_MiddleBtn;
        private ElasticBtn m_LastPathBtn;
        private ElasticBtn m_NextPathBtn;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<ElasticBtn>();
            RegisterComponentsTypes<RectTransform>();

            m_PathSetRect=GetComponent<RectTransform>("PathSetRect");
            m_HeadBtn=GetComponent<ElasticBtn>("HeadBtn");
            m_TailBtn = GetComponent<ElasticBtn>("TailBtn");
            m_MiddleBtn = GetComponent<ElasticBtn>("MiddleBtn");
            m_LastPathBtn = GetComponent<ElasticBtn>("LastPathBtn");
            m_NextPathBtn = GetComponent<ElasticBtn>("NextPathBtn");
            MesgManager.MesgListen("OnPathSetRect", OnPathSetRect);
        }

        protected override void Start()
        {
            base.Start();
            m_LastPathBtn.onClick.AddListener(OnLastPath);
            m_NextPathBtn.onClick.AddListener(OnNextPath);
            m_HeadBtn.onClick.AddListener(OnHead);
            m_TailBtn.onClick.AddListener(OnTail);
            m_MiddleBtn.onClick.AddListener(OnMiddle);
        }

        protected override void OnDestroy()
        {
            m_LastPathBtn.onClick.RemoveListener(OnLastPath);
            m_NextPathBtn.onClick.RemoveListener(OnNextPath);
            m_HeadBtn.onClick.RemoveListener(OnHead);
            m_TailBtn.onClick.RemoveListener(OnTail);
            m_MiddleBtn.onClick.RemoveListener(OnMiddle);
            base.OnDestroy();
        }
        #region ButtonEvent
        private void OnHead()//头部
        {
            Common().color = Color.green;

        }
        private void OnTail()//尾部
        {
            Common().color = Color.red;

        }
        private void OnMiddle()//中间
        {
            Common().color = Color.yellow;

        }
        private void OnLastPath()
        {

        }
        private void OnNextPath()
        {

            
        }
        #endregion
        PathData pathDara = new PathData();
        private SpriteRenderer Common()
        {
            m_PathSetRect.gameObject.SetActive(false);
            if (!GoReusePool.Take("Tag", out GameObject tag))
            {
                if (!GoLoad.Take("Prefabs/Tag", out tag))
                {
                    return null;
                }
            }
            IGrid grid = GridManager.NowOperationGrid;
            (grid as MapGrid.Grid).Tag = tag;
            tag.transform.position = grid.Position;
            grid.GridType = EnumGrid.Path;
            pathDara.IndexLi.Add(grid.Index);
            PathManager.AddPathData(pathDara);
            return tag.GetComponent<SpriteRenderer>();      
        }
        private void OnPathSetRect()
        {
            m_PathSetRect.gameObject.SetActive(true);
            m_PathSetRect.position = GridManager.NowOperationGrid.Position;
        }
    }
}
    