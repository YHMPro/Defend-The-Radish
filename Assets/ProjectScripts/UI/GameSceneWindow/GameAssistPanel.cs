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
        private RectTransform m_Indicate;
        private RectTransform m_AttackArea;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<RectTransform>();
            m_Indicate = GetComponent<RectTransform>("Indicate");
            m_AttackArea = GetComponent<RectTransform>("AttackArea");
            MesgManager.MesgListen<IGrid>(GridManager.OperationGridEvent, this.OnOperationGrid);

        }

        protected override void Start()
        {
            base.Start();
     
        }

        protected override void OnDestroy()
        {
          
            base.OnDestroy();
           
        }

        private void OnOperationGrid(IGrid grid)
        {
            switch (grid.GridType)
            {
                case EnumGrid.Empty:
                    {
                        m_Indicate.gameObject.SetActive(true);
                        m_Indicate.position = new Vector3(grid.Position.x, grid.Position.y, m_Indicate.position.z);
                        break;
                    }
                case EnumGrid.Tower:
                    {
                        m_Indicate.gameObject.SetActive(true);
                        m_Indicate.position = new Vector3(grid.Position.x, grid.Position.y, m_Indicate.position.z);
                        m_AttackArea.position = new Vector3(grid.Position.x, grid.Position.y, m_AttackArea.position.z);
                        float radius = 405f + 405f / 3.0f * (grid.PutObj.GetComponent<ITower>().TowerGrade - 1);
                        m_AttackArea.sizeDelta = Vector2.one * radius;
                        m_AttackArea.gameObject.SetActive(true);
                        break;
                    }
            }
        }
      
        public void OnOperationGridFinish()
        {
            m_Indicate.gameObject.SetActive(false);
            m_AttackArea.gameObject.SetActive(false);
        }
    }
}
