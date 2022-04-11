using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using DTR.Data;
using DTR.MapGrid;
using Farme.UI;
using UnityEngine.UI;
using UnityEditor;
namespace DTR.LevelBuilder
{
    public class LevelBuilderEntry : MonoBehaviour
    {
        [Header("关卡模式栏")]      
        [Tooltip("关卡模式名称")]
        public string LevelModel = "";
        [Space(10)]
        [Header("关卡主题栏")]     
        [Tooltip("关卡主题名称")]
        public string LevelTheme = "";
        [Header("关卡栏")]
        [Tooltip("关卡锁")]
        public bool LevelLock = true;
        [Tooltip("关卡索引")]
        public int LevelIndex = 0;

        private void Awake()
        {
            GameDataManager.ReadData();
            MonoSingletonFactory<ShareMono>.GetSingleton(null, false);
        }

        private void Start()
        {
            GridManager.Init();
            if (GoLoad.Take("FarmeLockFile/WindowRoot", out GameObject windowRootGo))
            {
                WindowRoot windowRoot = MonoSingletonFactory<WindowRoot>.GetSingleton(windowRootGo, false);
                windowRoot.CreateWindow("LevelBuilderWindow", RenderMode.ScreenSpaceCamera, (window) =>
                {
                    window.Canvas.sortingOrder = 5;
                    window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                    window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式              
                    window.CreatePanel<LevelBuilderPanel>("Prefabs/UI/LevelBuilderWindow/LevelBuilderPanel", "LevelBuilderPanel", EnumPanelLayer.MIDDLE, (panel) =>//加载面板
                    {

                    });
                });
            }
        }

        private void OnValidate()
        {
            LevelBuilderTool.RelyData.LevelModel = LevelModel;
            LevelBuilderTool.RelyData.LevelTheme = LevelTheme;
            LevelBuilderTool.LevelLock = LevelLock;
            LevelBuilderTool.LevelIndex = LevelIndex;           
        }
    }
}
