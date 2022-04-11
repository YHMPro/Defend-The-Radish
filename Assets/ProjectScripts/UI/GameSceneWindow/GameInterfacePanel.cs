using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using Farme;
using UnityEngine.UI;
namespace DTR.UI
{
    /// <summary>
    /// 游戏界面面板
    /// </summary>
    public class GameInterfacePanel : BasePanel
    {
        private RectTransform m_EnemyCountRect;
        private RectTransform m_HurdleRect;
        private RectTransform m_PauseTitleRect;  
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
            m_HurdleRect = GetComponent<RectTransform>("HurdleRect");
            m_PauseTitleRect = GetComponent<RectTransform>("PauseTitleImg");
            m_EnemyCountRect = GetComponent<RectTransform>("EnemyCountRect");
            m_MoneyText=GetComponent<Text>("MoneyText");
            m_EnemyCountText = GetComponent<Text>("EnemyCountText");
            m_EnemyTotalCountText = GetComponent<Text>("EnemyTotalCountText");
            m_SpeedUpBtn = GetComponent<Button>("SpeedUpBtn");
            m_StartOrPauseBtn = GetComponent<ElasticBtn>("StartOrPauseBtn");
            m_HurdleBtn = GetComponent<ElasticBtn>("HurdleBtn");
            m_ContinueBtn = GetComponent<ElasticBtn>("ContinueBtn");
            m_ReplayBtn = GetComponent<ElasticBtn>("ReplayBtn");
            m_LevelChooseBtn = GetComponent<ElasticBtn>("LevelChooseBtn");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_PauseTitleRect.gameObject.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();


            m_SpeedUpBtn.onClick.AddListener(OnSpeedUp);
            m_StartOrPauseBtn.onClick.AddListener(OnStartOrPause);
            m_HurdleBtn.onClick.AddListener(OnHurdle);
            m_ContinueBtn.onClick.AddListener(OnContinue);
            m_ReplayBtn.onClick.AddListener(OnReplay);
            m_LevelChooseBtn.onClick.AddListener(OnLevelChoose);
        }

        protected override void OnDestroy()
        {
            m_SpeedUpBtn.onClick.RemoveListener(OnSpeedUp);
            m_StartOrPauseBtn.onClick.RemoveListener(OnStartOrPause);
            m_HurdleBtn.onClick.RemoveListener(OnHurdle);
            m_ContinueBtn.onClick.RemoveListener(OnContinue);
            m_ReplayBtn.onClick.RemoveListener(OnReplay);
            m_LevelChooseBtn.onClick.RemoveListener(OnLevelChoose);
            base.OnDestroy();
        }

        #region ButtonEvent
        private void OnSpeedUp()
        {

        }
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
    }
}
