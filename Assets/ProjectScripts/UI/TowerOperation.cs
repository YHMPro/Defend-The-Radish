//using System.Collections;
//using System.Collections.Generic;
// using UnityEngine;
//using Farme;
//using DG.Tweening;
//using UnityEngine.UI;
//using Farme.UI;
//namespace DTR.UI
//{
//    /// <summary>
//    /// 塔台操作UI
//    /// </summary>
//    public class TowerOperation : BaseMono
//    {
//        [SerializeField]
//        private GameObject m_UpgradeAndTeardownRectGo;
//        [SerializeField]
//        private GameObject m_BuilderRectGo;
//        /// <summary>
//        /// 塔台攻击范围UI
//        /// </summary>
//        [SerializeField]
//        private Image m_TowerAreaImg;
//        /// <summary>
//        /// 建造塔台按钮
//        /// </summary>
//        private ElasticBtn m_BuilderTowerBtn;
         

//        protected override void Awake()
//        {
//            base.Awake();
//            RegisterComponentsTypes<ElasticBtn>();
//            RegisterComponentsTypes<HorizontalLayoutGroup>();
//            MonoSingletonFactory<TowerOperation>.GetSingleton(this.gameObject);           
//            m_BuilderTowerBtn = GetComponent<ElasticBtn>("Tower");
//        }


//        protected override void Start()
//        {
//            base.Start();
//            m_BuilderTowerBtn.onClick.AddListener(this.OnBuilderTower);
//            gameObject.SetActive(false);
//        }

//        protected override void OnDisable()
//        {
//            base.OnDisable();
//            m_BuilderRectGo.SetActive(false);
//            m_UpgradeAndTeardownRectGo.SetActive(false);
//        }
//        protected override void OnDestroy()
//        {
//            m_BuilderTowerBtn.onClick.RemoveListener(this.OnBuilderTower);
//            base.OnDestroy();
//        }
//        /// <summary>
//        /// 显示
//        /// </summary>
//        /// <param name="fillObject"></param>
//        /// <param name="mapGridPoint"></param>
//        public void Show(EnumMapGridFillObject fillObject,Vector2 mapGridPoint)
//        {
//            switch(fillObject)
//            {
//                case EnumMapGridFillObject.Empty://显示构建塔台的UI
//                    {
//                        m_BuilderRectGo.transform.localPosition = new Vector2(0, mapGridPoint.y > 3 ? -100 : 100);
//                        m_BuilderRectGo.SetActive(true);
//                        break;
//                    }
//                case EnumMapGridFillObject.Tower://显示升级与拆卸的UI
//                    {
//                        m_UpgradeAndTeardownRectGo.SetActive(true);
                      
//                        break;
//                    }
//            }
//            gameObject.SetActive(true);
//        }
 
//        #region  Button
//        private void OnBuilderTower()
//        {
//            GameManager.TowerBuilder(EnumTower.BottleTower, (tower) =>
//            {
//                tower.transform.position = transform.position;

                
//            });
//            gameObject.SetActive(false);
//        }
//        #endregion
//    }
//}
