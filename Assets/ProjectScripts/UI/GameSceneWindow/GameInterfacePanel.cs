using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using Farme;
using UnityEngine.UI;
using DTR.MapGrid;
using Farme.Extend;
using UnityEngine.EventSystems;
using DTR.Config;
namespace DTR.UI
{
    /// <summary>
    /// 游戏界面面板
    /// </summary>
    public class GameInterfacePanel : BasePanel
    {
        public Sprite[] Temp;
        private Coroutine m_Cor;
        private RectTransform m_EnemyCountRect;
        private RectTransform m_HurdleRect;
        private RectTransform m_PauseTitleRect;
        private RectTransform m_TowerRect;
        #region TowerRect
        private Image m_Forbid;
        private Image m_HBtn;
        private ElasticBtn m_BuilderBtn;
        private ElasticBtn m_UpgradeBtn;
        private ElasticBtn m_TeardownBtn;
        #endregion

        #region MenuRect
        /// <summary>
        /// 钱
        /// </summary>
        private Text m_MoneyText;
        /// <summary>
        /// 怪物的波数(第几波)
        /// </summary>
        private Text m_EnemyCountText;
        /// <summary>
        /// 怪物的总波数
        /// </summary>
        private Text m_EnemyTotalCountText;
        /// <summary>
        /// 加速按钮
        /// </summary>
        private Button m_SpeedUpBtn;
        /// <summary>
        /// 开始与暂停按钮
        /// </summary>
        private ElasticBtn m_StartOrPauseBtn;
        /// <summary>
        /// 栏按钮
        /// </summary>
        private ElasticBtn m_HurdleBtn;
        #endregion
        #region HurdleRect
        /// <summary>
        /// 继续按钮
        /// </summary>
        private ElasticBtn m_ContinueBtn;
        /// <summary>
        /// 重玩按钮
        /// </summary>
        private ElasticBtn m_ReplayBtn;
        /// <summary>
        /// 关卡选择按钮
        /// </summary>
        private ElasticBtn m_LevelChooseBtn;
        #endregion
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Text>();
            RegisterComponentsTypes<Button>();
            RegisterComponentsTypes<RectTransform>();
            RegisterComponentsTypes<Image>();
            m_HBtn = GetComponent<Image>("HBtn");
            m_Forbid = GetComponent<Image>("Forbid");
            m_TowerRect = GetComponent<RectTransform>("TowerRect");
            m_HurdleRect = GetComponent<RectTransform>("HurdleRect");
            m_PauseTitleRect = GetComponent<RectTransform>("PauseTitleImg");
            m_EnemyCountRect = GetComponent<RectTransform>("EnemyCountRect");
            m_MoneyText=GetComponent<Text>("MoneyText");
            m_EnemyCountText = GetComponent<Text>("EnemyCountText");
            m_EnemyTotalCountText = GetComponent<Text>("EnemyTotalCountText");
            m_SpeedUpBtn = GetComponent<Button>("SpeedUpBtn");
            m_StartOrPauseBtn = GetComponent<ElasticBtn>("StartOrPauseBtn");
            m_BuilderBtn = GetComponent<ElasticBtn>("BuilderBtn");
            m_UpgradeBtn = GetComponent<ElasticBtn>("UpgradeBtn");
            m_TeardownBtn = GetComponent<ElasticBtn>("TeardownBtn");
            m_HurdleBtn = GetComponent<ElasticBtn>("HurdleBtn");
            m_ContinueBtn = GetComponent<ElasticBtn>("ContinueBtn");
            m_ReplayBtn = GetComponent<ElasticBtn>("ReplayBtn");
            m_LevelChooseBtn = GetComponent<ElasticBtn>("LevelChooseBtn");

            MesgManager.MesgListen<IGrid>(GridManager.OperationGridEvent, this.OnOperationGrid);
            MesgManager.MesgListen(GameLogic.MoneyRefreshEvent, this.MoneyRefreshCallback);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_PauseTitleRect.gameObject.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();
            m_HBtn.UIEventRegistered(EventTriggerType.PointerDown, HBtnOnPointerDown);
            m_BuilderBtn.onClick.AddListener(OnBuilder);
            m_UpgradeBtn.onClick.AddListener(OnUpgrade);
            m_TeardownBtn.onClick.AddListener(OnTeardown);
            m_SpeedUpBtn.onClick.AddListener(OnSpeedUp);
            m_StartOrPauseBtn.onClick.AddListener(OnStartOrPause);
            m_HurdleBtn.onClick.AddListener(OnHurdle);
            m_ContinueBtn.onClick.AddListener(OnContinue);
            m_ReplayBtn.onClick.AddListener(OnReplay);
            m_LevelChooseBtn.onClick.AddListener(OnLevelChoose);
        }

        protected override void OnDestroy()
        {
            MesgManager.MesgBreakListen<IGrid>(GridManager.OperationGridEvent, this.OnOperationGrid);
            MesgManager.MesgBreakListen(GameLogic.MoneyRefreshEvent, this.MoneyRefreshCallback);
            m_HBtn.UIEventRemove(EventTriggerType.PointerDown, HBtnOnPointerDown);
            m_BuilderBtn.onClick.RemoveListener(OnBuilder);
            m_UpgradeBtn.onClick.RemoveListener(OnUpgrade);
            m_TeardownBtn.onClick.RemoveListener(OnTeardown);
            m_SpeedUpBtn.onClick.RemoveListener(OnSpeedUp);
            m_StartOrPauseBtn.onClick.RemoveListener(OnStartOrPause);
            m_HurdleBtn.onClick.RemoveListener(OnHurdle);
            m_ContinueBtn.onClick.RemoveListener(OnContinue);
            m_ReplayBtn.onClick.RemoveListener(OnReplay);
            m_LevelChooseBtn.onClick.RemoveListener(OnLevelChoose);
            base.OnDestroy();
        }

        /// <summary>
        /// 操作格子时自动调用
        /// </summary>
        /// <param name="grid"></param>
        private void OnOperationGrid(IGrid grid)
        {
            if (MonoSingletonFactory<WindowRoot>.SingletonExist)
            {
                WindowRoot windowRoot = MonoSingletonFactory<WindowRoot>.GetSingleton();
                Vector3 screenPoint = windowRoot.Camera.WorldToScreenPoint(grid.Position);
                screenPoint.z = 0;
                MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(0, () =>
                {

                    switch (grid.GridType)
                    {
                        case EnumGrid.Empty:
                            { 
                                //根据当前已可建造的塔台类型进行UI加载  待

                                m_HBtn.raycastTarget = true;
                                m_BuilderBtn.gameObject.SetActive(true);
                                m_BuilderBtn.transform.position = screenPoint + new Vector3(0, 110, 0);
                                break;
                            }
                        case EnumGrid.Tower:
                            {
                                m_HBtn.raycastTarget = true;
                                ITower iTower = grid.PutObj.GetComponent<ITower>();
                                TowerConfig towerConfig = TowerConfig.GetTowerConfig(iTower.TowerType);
                                m_UpgradeBtn.image.sprite = AtlasAssetsLoad.Load("UI/Items02-hd", (iTower.TowerGrade == 3)? "Items02-hd_14":"Items02-hd_1");
                                if (!(towerConfig.GetBuilderMoney(iTower.TowerGrade-1).ToString() == m_TeardownBtn.Content))
                                {
                                    m_UpgradeBtn.Content = (iTower.TowerGrade==3)?"":towerConfig.GetBuilderMoney(iTower.TowerGrade-1).ToString();
                                }
                                if (!(towerConfig.GetTeardownMoney(iTower.TowerGrade-1).ToString() == m_TeardownBtn.Content))
                                {
                                    m_TeardownBtn.Content = towerConfig.GetTeardownMoney(iTower.TowerGrade-1).ToString();
                                }                            
                                m_UpgradeBtn.gameObject.SetActive(true);
                                m_TeardownBtn.gameObject.SetActive(true);
                                m_UpgradeBtn.transform.position = screenPoint + new Vector3(0, 110,0);
                                m_TeardownBtn.transform.position = screenPoint + new Vector3(0, -110, 0);
                                break;
                            }
                        case EnumGrid.Path:
                            {
                                if (m_Cor != null)
                                {
                                    MonoSingletonFactory<ShareMono>.GetSingleton().StopCoroutine(m_Cor);
                                    m_Cor = null;
                                }
                                m_Forbid.gameObject.SetActive(true);
                                m_Forbid.transform.position = screenPoint;
                                m_Cor = MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(1f, () =>
                                {
                                    m_Forbid.gameObject.SetActive(false);
                                });
                                break;
                            }
                    }
                });
            }
        }
        private void OnOperationGridFinish()
        {
            if (!MonoSingletonFactory<WindowRoot>.SingletonExist)
            {
                return;
            }
            WindowRoot windowRoot = MonoSingletonFactory<WindowRoot>.GetSingleton();
            if(windowRoot.GetWindow("GameSceneWindow_ScreenSpaceCamera",out StandardWindow gsWindow))
            {
                if(gsWindow.GetPanel("GameAssistPanel",out GameAssistPanel gaPanel))
                {
                    gaPanel.OnOperationGridFinish();
                }
            }

        }
        #region ButtonEvent

        private void HBtnOnPointerDown(BaseEventData bEData)
        {
            m_HBtn.raycastTarget = false;
            m_BuilderBtn.gameObject.SetActive(false);
            m_UpgradeBtn.gameObject.SetActive(false);
            m_TeardownBtn.gameObject.SetActive(false);
            OnOperationGridFinish();
        }
        /// <summary>
        /// 建造
        /// </summary>
        private void OnBuilder()
        {
            m_HBtn.raycastTarget = false;
            m_BuilderBtn.gameObject.SetActive(false);
            OnOperationGridFinish();
            IGrid grid = GridManager.NowOperationGrid;
            GameManager.TowerBuilder(grid.Index, EnumTower.BottleTower, (tower) =>
            {
                tower.transform.position = grid.Position+new Vector3(0,0,0.1f);
                grid.GridType = EnumGrid.Tower;
                grid.PutObj = tower;
            });
        }
        /// <summary>
        /// 升级
        /// </summary>
        private void OnUpgrade()
        {
            IGrid grid = GridManager.NowOperationGrid;
            ITower iTower = grid.PutObj.GetComponent<ITower>();
            iTower.TowerUpgrade();           
            m_HBtn.raycastTarget = false;
            m_UpgradeBtn.gameObject.SetActive(false);
            m_TeardownBtn.gameObject.SetActive(false);
            OnOperationGridFinish();
        }
        /// <summary>
        /// 拆卸
        /// </summary>
        private void OnTeardown()
        {
            IGrid grid = GridManager.NowOperationGrid;
            ITower iTower = grid.PutObj.GetComponent<ITower>();
            iTower.TowerTeardown();
            grid.GridType = EnumGrid.Empty;
            grid.PutObj = null;
            m_HBtn.raycastTarget = false;
            m_UpgradeBtn.gameObject.SetActive(false);
            m_TeardownBtn.gameObject.SetActive(false);
            OnOperationGridFinish();
        }
        /// <summary>
        /// 提速
        /// </summary>
        private void OnSpeedUp()
        {

        }
        /// <summary>
        /// 开始或暂停
        /// </summary>
        private void OnStartOrPause()
        {
            GameManager.GameStateControl(GameManager.IsGameRun?EnumGameState.Pause:EnumGameState.Run);
            m_EnemyCountRect.gameObject.SetActive(GameManager.IsGameRun);
            m_PauseTitleRect.gameObject.SetActive(!GameManager.IsGameRun);
        }
        private void OnHurdle()
        {
            m_HurdleRect.gameObject.SetActive(true);
        }
        private void OnContinue()
        {
            m_HurdleRect.gameObject.SetActive(false);
            GameManager.GameStateControl(EnumGameState.Run);        
        }
        private void OnReplay()
        {
            GameManager.GameReset();
        }
        private void OnLevelChoose()
        {

        }
        #endregion
        /// <summary>
        /// 钱刷新事件回调
        /// </summary>
        private void MoneyRefreshCallback()
        {
            m_MoneyText.text = GameLogic.MoneyTatal.ToString();
        }
    }
}
