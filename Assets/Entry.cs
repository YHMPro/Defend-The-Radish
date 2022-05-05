using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.UI;
using UnityEngine.UI;
using DTR.UI;
using DTR.MapGrid;
using DTR.Data;
using DTR.Path;
namespace DTR
{
    public class Entry : MonoBehaviour
    {
        public string LevelModel;
        public string LevelTheme;
        public int LevelIndex;

        private void Awake()
        {

            GameDataManager.ReadData();
            MonoSingletonFactory<ShareMono>.GetSingleton(null, false);
            GameManager.RelyData.LevelModel = LevelModel;
            GameManager.RelyData.LevelTheme = LevelTheme;


            //AssetBundleLoad.PackageCatalogueFile_URL = Application.streamingAssetsPath+"/";
            //AssetBundleLoad.MainABName = "PC";
        }
        void Start()
        {

            GameLogic.Init();
            GridManager.Init();
            PathManager.LoadNowLevelPathData();
           
            if(GoLoad.Take("Prefabs/MonsterNest", out GameObject monsterNest))
            {
                monsterNest.transform.position = PathManager.Head.Position;
            }
            if(GoLoad.Take("Prefabs/Radish",out GameObject radish))
            {
                radish.transform.position = PathManager.Tail.Position + new Vector3(0, 0, -0.1f);
            }
            if (GoLoad.Take("FarmeLockFile/WindowRoot", out GameObject windowRootGo))
            {
                WindowRoot windowRoot = MonoSingletonFactory<WindowRoot>.GetSingleton(windowRootGo, false);
                windowRoot.CreateWindow("GameSceneWindow_ScreenSpaceOverlay", RenderMode.ScreenSpaceOverlay, (window) =>
                {
                    window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                    window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式              
                    window.CreatePanel<GameInterfacePanel>("Prefabs/UI/GameSceneWindow/GameInterfacePanel", "GameInterfacePanel", EnumPanelLayer.MIDDLE, (panel) =>//加载面板
                    {

                    });
                    window.CreatePanel<TagPanel>("Prefabs/UI/GameSceneWindow/TagPanel", "TagPanel", EnumPanelLayer.BOTTOM, (panel) =>//加载面板
                    {

                    });
                });
                windowRoot.CreateWindow("GameSceneWindow_ScreenSpaceCamera", RenderMode.ScreenSpaceCamera, (window) =>
                {
                    window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                    window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                    window.Canvas.sortingOrder = 0;//设置画布层级
                    window.Canvas.planeDistance = 100;//设置面板距离
                  
                    window.CreatePanel<GameAssistPanel>("Prefabs/UI/GameSceneWindow/GameAssistPanel", "GameAssistPanel", EnumPanelLayer.TOP, (panel) =>//加载面板
                    {

                    });
                });
              
            }
        }


        private void Update()
        {
        }
    }
}
