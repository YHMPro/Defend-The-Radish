using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using UnityEngine.UI;
using UnityEngine.Events;
using Farme;
using DTR.MapGrid;
using Farme.Extend;
using UnityEngine.EventSystems;
using DG.Tweening;
using Farme.Tool;
namespace DTR.UI
{
    /// <summary>
    /// 游戏辅助面板
    /// </summary>
    public class GameAssistPanel : BasePanel
    {
        private RectTransform m_Forbid;
        private RectTransform m_TowerBuilderRect;
        private RectTransform m_UpgradeAndTeardownRect;
        private RectTransform m_Indicate;
        /// <summary>
        /// 塔台建造按钮
        /// </summary>
        private ElasticBtn m_BuilderBtn;
        /// <summary>
        /// 塔台升级按钮
        /// </summary>
        private ElasticBtn m_UpgradeBtn;
        /// <summary>
        /// 塔台拆卸按钮
        /// </summary>
        private ElasticBtn m_TeardownBtn;


        private Image m_HideBtn;
        private Coroutine m_Cor;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<RectTransform>();
            RegisterComponentsTypes<ElasticBtn>();
            RegisterComponentsTypes<Image>();
            m_HideBtn = GetComponent<Image>("HideBtn");
            m_TowerBuilderRect =GetComponent<RectTransform>("TowerBuilderRect");
            m_UpgradeAndTeardownRect = GetComponent<RectTransform>("UpgradeAndTeardownRect");
            m_Forbid = GetComponent<RectTransform>("Forbid");
            m_Indicate = GetComponent<RectTransform>("Indicate");
            m_BuilderBtn = GetComponent<ElasticBtn>("BuilderBtn");
            m_UpgradeBtn = GetComponent<ElasticBtn>("UpgradeBtn");
            m_TeardownBtn = GetComponent<ElasticBtn>("TeardownBtn");
        }

        protected override void Start()
        {
            base.Start();
            m_HideBtn.UIEventRegistered(EventTriggerType.PointerDown, OnHide);
            m_BuilderBtn.onClick.AddListener(OnBuilder);
            m_UpgradeBtn.onClick.AddListener(OnUpgrade);
            m_TeardownBtn.onClick.AddListener(OnTeardown);
        }

        protected override void OnDestroy()
        {
            m_HideBtn.UIEventRemove(EventTriggerType.PointerDown, OnHide);
            m_BuilderBtn.onClick.RemoveListener(OnBuilder);
            m_UpgradeBtn.onClick.RemoveListener(OnUpgrade);
            m_TeardownBtn.onClick.RemoveListener(OnTeardown);
            base.OnDestroy();
           
        }

        public override void SetState(EnumPanelState state, UnityAction callback = null)
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(0, () =>
            {              
                base.SetState(state, () =>
                {
                    switch (state)
                    {
                        case EnumPanelState.Show:
                            {
                                IGrid grid = GridManager.NowOperationGrid;
                                //Vector2 map_Line = MonoSingletonFactory<MapGirdManager>.GetSingleton().Line;
                                //float x = mapGrid.MapGridIndex.x;
                                //float y= mapGrid.MapGridIndex.y;
                                //float dis = 110;
                                switch (grid.GridType)
                                {
                                    case EnumGrid.Empty:
                                        {
                                            m_HideBtn.raycastTarget = true;
                                            m_Indicate.position = grid.Position;
                                            m_Indicate.gameObject.SetActive(true);
                                            #region 暂时先这样设置位置
                                            m_BuilderBtn.transform.localPosition = new Vector3(0, 110, -4);
                                            #endregion
                                            m_TowerBuilderRect.position = grid.Position;
                                            m_TowerBuilderRect.gameObject.SetActive(true);
                                            break;
                                        }
                                    case EnumGrid.Tower:
                                        {
                                            m_HideBtn.raycastTarget = true;
                                            m_Indicate.position = grid.Position;
                                            m_Indicate.gameObject.SetActive(true);
                                            #region 暂时先这样设置位置      


                                            //if((x==0) || (y==0))
                                            //{
                                            //    m_UpgradeBtn.transform.localPosition = new Vector3(0, dis, -4);
                                            //    m_TeardownBtn.transform.localPosition = new Vector3(dis,0, -4);
                                            //}else if((x == 0) || (y == (map_Line.y-1)))
                                            //{
                                            //    m_UpgradeBtn.transform.localPosition = new Vector3(dis, 0, -4);
                                            //    m_TeardownBtn.transform.localPosition = new Vector3(0, -dis, -4);
                                            //}else if((x == (map_Line.x-1)) || (y == (map_Line.y-1)))
                                            //{
                                            //    m_UpgradeBtn.transform.localPosition = new Vector3(-dis, 0, -4);
                                            //    m_TeardownBtn.transform.localPosition = new Vector3(0, -dis, -4);
                                            //}else if((x == (map_Line.x - 1)) || (y == 0))
                                            //{
                                            //    m_UpgradeBtn.transform.localPosition = new Vector3(0, dis, -4);
                                            //    m_TeardownBtn.transform.localPosition = new Vector3(-dis,0, -4);
                                            //}
                                            //else if((x == 0)||((y>0)||y< (map_Line.y - 1)))
                                            //{

                                            //}
                                            m_UpgradeBtn.transform.localPosition = new Vector3(0, 110, -4);
                                            m_TeardownBtn.transform.localPosition = new Vector3(0, -110, -4);
                                            #endregion
                                            m_UpgradeAndTeardownRect.position = grid.Position;
                                            m_UpgradeAndTeardownRect.gameObject.SetActive(true);
                                            break;
                                        }
                                    default:
                                        {
                                            if (m_Cor != null)
                                            {
                                                MonoSingletonFactory<ShareMono>.GetSingleton().StopCoroutine(m_Cor);
                                                m_Cor = null;
                                            }
                                            m_Forbid.gameObject.SetActive(true);
                                            m_Forbid.position = grid.Position;
                                            m_Cor = MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(1f, () =>
                                            {
                                                m_Forbid.gameObject.SetActive(false);
                                            });
                                            break;
                                        }
                                }                             
                                break;
                            }
                        case EnumPanelState.Hide:
                            {
                                m_HideBtn.raycastTarget = false;
                                m_TowerBuilderRect.gameObject.SetActive(false);
                                m_UpgradeAndTeardownRect.gameObject.SetActive(false);
                                m_Indicate.gameObject.SetActive(false);
                                m_Forbid.gameObject.SetActive(false);
                                break;
                            }
                    }
                    callback?.Invoke();
                });
            });
            
        }

        #region ButtonEvent
        private void OnHide(BaseEventData bEData)
        {            
            SetState(EnumPanelState.Hide);
        }
        private void OnBuilder()
        {      
            SetState(EnumPanelState.Hide, () => 
            {
                IGrid grid = GridManager.NowOperationGrid;
                GameManager.TowerBuilder(grid.Index, EnumTower.BottleTower, (tower) =>
                {
                    tower.transform.position = grid.Position;
                    grid.GridType = EnumGrid.Tower;
                });            
            });
        }
        private void OnUpgrade()
        {
            SetState(EnumPanelState.Hide, () => 
            {
                
            });
        }
        private void OnTeardown()
        {
            SetState(EnumPanelState.Hide, () => 
            {
                
            });
        }



        #endregion

    }
}
