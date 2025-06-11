using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotButton : MonoBehaviour
{
    [Tooltip("用於截圖的 Camera 列表")]
    [SerializeField] private Camera[] cameras;

    [Tooltip("儲存路徑（相對於應用程式資料夾）")]
    [SerializeField] private string saveFolder = "Screenshots";

    [Tooltip("按鈕元件")]
    [SerializeField] private Button screenshotButton;

    void Start()
    {
        if (screenshotButton == null)
        {
            Debug.LogError("請在 Inspector 中綁定 Button 組件");
            return;
        }

        if (cameras == null || cameras.Length == 0)
        {
            Debug.LogError("請在 Inspector 中綁定至少一個 Camera");
            return;
        }
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
        }

        // 綁定按鈕點擊事件
        screenshotButton.onClick.AddListener(TakeScreenshots);
    }

    void TakeScreenshots()
    {
        foreach (Camera cam in cameras)
        {
            cam.enabled = true;
        }
        string fullPath = Path.Combine(UnityEngine.Application.persistentDataPath, saveFolder);
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == null) continue;

            // 設置當前 Camera 的目標紋理（如果需要）
            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameras[i].targetTexture = renderTexture;
            cameras[i].Render();

            // 創建紋理2D來儲存截圖
            RenderTexture.active = renderTexture;
            Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();

            // 釋放 RenderTexture
            RenderTexture.active = null;
            cameras[i].targetTexture = null;
            Object.Destroy(renderTexture);

            // 儲存到檔案
            string fileName = $"Screenshot_Camera{i}_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
            string filePath = Path.Combine(fullPath, fileName);
            byte[] bytes = screenshot.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);

            // 釋放紋理記憶體
            Object.Destroy(screenshot);

            Debug.Log($"截圖儲存至: {filePath}");
        }
    }
}