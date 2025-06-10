using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class LoadScreenshotToUI : MonoBehaviour
{
    [Tooltip("顯示截圖的 UI Image")]
    [SerializeField] private Image uiImage;

    [Tooltip("儲存截圖的資料夾路徑（相對於 Application.dataPath）")]
    [SerializeField] private string saveFolder = "Screenshots";

    void Start()
    {
        if (uiImage == null)
        {
            Debug.LogError("請在 Inspector 中綁定 UI Image 和 Load Button");
            return;
        }

        // 綁定按鈕點擊事件
        LoadLatestScreenshot();
    }

    void LoadLatestScreenshot()
    {
        string fullPath = Path.Combine(UnityEngine.Application.persistentDataPath, saveFolder);
        if (!Directory.Exists(fullPath))
        {
            Debug.LogError("截圖資料夾不存在");
            return;
        }

        // 獲取最新截圖（根據時間排序）
        string[] files = Directory.GetFiles(fullPath, "*.png")
            .OrderByDescending(f => new FileInfo(f).LastWriteTime)
            .ToArray();

        if (files.Length == 0)
        {
            Debug.LogWarning("無可加載的截圖");
            return;
        }

        string latestFile = files[0];
        StartCoroutine(LoadTexture(latestFile));
    }

    private System.Collections.IEnumerator LoadTexture(string filePath)
    {
        // 異步加載紋理
        WWW www = new("file://" + filePath);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError($"加載截圖失敗: {www.error}");
            yield break;
        }

        Texture2D texture = www.texture;
        if (texture == null)
        {
            Debug.LogError("紋理加載失敗");
            yield break;
        }

        // 將紋理轉為 Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // 應用到 UI Image
        uiImage.sprite = sprite;
        uiImage.preserveAspect = true; // 保持長寬比

        // 釋放資源
        www.Dispose();
        Resources.UnloadUnusedAssets();
    }
}