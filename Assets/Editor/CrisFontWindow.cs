using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
public class CrisFontWindow : EditorWindow
{
    private string fontPath = "Assets/crisFont.fontsettings";
    private Font font;
    private Rect[] rects;
    private TextAsset characterCfg;

    void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("字体：", GUILayout.Width(100.0f));
        font = (Font)EditorGUILayout.ObjectField(font, typeof(Font), true);
        GUILayout.EndHorizontal();

        if (font == null)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("字体路径", GUILayout.Width(100));
            fontPath = GUILayout.TextField(fontPath);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("新建字体"))
            {
                Font newFont = new Font();
                try
                {
                    AssetDatabase.CreateAsset(newFont, fontPath);
                    font = AssetDatabase.LoadAssetAtPath<Font>(fontPath);
                }
                catch
                {

                }
                if (font == null) Debug.LogError("路径错误：" + fontPath);
            }
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("字体配置：", GUILayout.Width(100.0f));
            characterCfg = (TextAsset)EditorGUILayout.ObjectField(characterCfg, typeof(TextAsset), true);
            GUILayout.EndHorizontal();
            GUILayout.Label("请选中需要打入的字体文件");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("更新文本")) UpdateFont();

            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }

    void UpdateFont()
    {
        string fontPath = AssetDatabase.GetAssetPath(font);
        string fontDir = Path.GetDirectoryName(fontPath);
        var texSavePath = Path.Combine(fontDir, Path.GetFileNameWithoutExtension(fontPath) + ".png");
        CombineTexture(texSavePath);
        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texSavePath);
        if (tex == null) Debug.LogError("未发现生成的图集");

        var matPath = Path.Combine(fontDir, Path.GetFileNameWithoutExtension(fontPath) + ".mat");
        var mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
        if (mat == null)
        {
            var newMat = new Material(Shader.Find("GUI/Text Shader"));
            AssetDatabase.CreateAsset(newMat, matPath);
            mat = newMat;
        }
        mat.SetTexture("_MainTex", tex);
        EditorUtility.SetDirty(mat);
        var list = new List<CharacterInfo>();

        float maxHeight = 0;
        for (int i = 0; i < rects.Length; i++)
        {
            if (rects[i].height > maxHeight) maxHeight = rects[i].height;
        }

        for (int i = 0; i < rects.Length; i++)
        {
            Rect rect = ConvertToPixels(rects[i], tex.width, tex.height);
            int asciiIndex = ac[i];
            var charInfo = new CharacterInfo();
            charInfo.index = asciiIndex;

            Rect uv = ConvertToTexCoords(rect, tex.width, tex.height);
            charInfo.uvBottomLeft = new Vector2(uv.x, uv.y);
            charInfo.uvTopLeft = new Vector2(uv.x, uv.y + uv.height);
            charInfo.uvBottomRight = new Vector2(uv.x + uv.width, uv.y);
            charInfo.uvTopRight = new Vector2(uv.x + uv.width, uv.y + uv.height);

            charInfo.minX = 0;
            charInfo.maxX = (int)rect.width;
            charInfo.minY = (int)(tex.height / 2 - (maxHeight - rect.height) / 2 - rect.height);
            charInfo.maxY = (int)(tex.height / 2 - (maxHeight - rect.height) / 2);

            charInfo.advance = (int)rect.width;

            list.Add(charInfo);
        }
        font.material = mat;
        font.characterInfo = list.ToArray();
        EditorUtility.SetDirty(font);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("更新成功");
    }

    Rect ConvertToPixels(Rect rect, int width, int height)
    {
        Rect r = rect;
        r.xMin = Mathf.RoundToInt(rect.xMin * width);
        r.xMax = Mathf.RoundToInt(rect.xMax * width);
        r.yMin = Mathf.RoundToInt((1 - rect.yMax) * height);
        r.yMax = Mathf.RoundToInt((1 - rect.yMin) * height);
        return r;
    }

    Rect ConvertToTexCoords(Rect rect, int width, int height)
    {
        Rect r = rect;
        if (width != 0 && height != 0)
        {
            r.xMin = rect.xMin / width;
            r.xMax = rect.xMax / width;
            r.yMin = 1 - rect.yMax / height;
            r.yMax = 1 - rect.yMin / height;
        }
        return r;
    }

    List<int> ac = new List<int>();
    void CombineTexture(string texSavePath)
    {
        ac.Clear();
        int num = Selection.objects.Length;
        if (num <= 0)
        {
            Debug.LogError("请选中需要合并的数字图片");
            return;
        }

        Dictionary<string, object> cfgDic = null;
        if (characterCfg != null)
        {
            cfgDic =JsonConvert.DeserializeObject(characterCfg.text) as Dictionary<string, object>;
        }

        for (int i = 0; i < num; ++i)
        {
            Texture2D tex = Selection.objects[i] as Texture2D;
            string name = Path.GetFileNameWithoutExtension(tex.name);
            if (cfgDic != null && cfgDic.ContainsKey(name))
                ac.Add((int)(cfgDic[name].ToString()[0]));
            else
                ac.Add((int)(name[0]));
        }
        Texture2D r = new Texture2D(2, 2, TextureFormat.RGBA32, false);

        Texture2D[] textures = new Texture2D[num];

        for (int i = 0; i < num; i++)
        {
            Texture2D tex = Selection.objects[i] as Texture2D;
            textures[i] = tex;
        }
        rects = r.PackTextures(textures, 1);
        r.Apply();

        SaveTexture(r, texSavePath);
    }

    void SaveTexture(Texture2D texture, string path)
    {
        if (texture == null) return;
        File.WriteAllBytes(path, texture.EncodeToPNG());
        AssetDatabase.ImportAsset(path);
        AssetDatabase.Refresh();
    }

    [MenuItem("BlzTools/UI/美术字体", false, 1102)]
    public static void OpenWin()
    {
        EditorWindow.GetWindow<CrisFontWindow>("美术字体");
    }
}
