using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotButton : MonoBehaviour
{
    [Tooltip("�Ω�I�Ϫ� Camera �C��")]
    [SerializeField] private Camera[] cameras;

    [Tooltip("�x�s���|�]�۹�����ε{����Ƨ��^")]
    [SerializeField] private string saveFolder = "Screenshots";

    [Tooltip("���s����")]
    [SerializeField] private Button screenshotButton;

    void Start()
    {
        if (screenshotButton == null)
        {
            Debug.LogError("�Цb Inspector ���j�w Button �ե�");
            return;
        }

        if (cameras == null || cameras.Length == 0)
        {
            Debug.LogError("�Цb Inspector ���j�w�ܤ֤@�� Camera");
            return;
        }
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
        }

        // �j�w���s�I���ƥ�
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

            // �]�m��e Camera ���ؼЯ��z�]�p�G�ݭn�^
            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameras[i].targetTexture = renderTexture;
            cameras[i].Render();

            // �Ыد��z2D���x�s�I��
            RenderTexture.active = renderTexture;
            Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();

            // ���� RenderTexture
            RenderTexture.active = null;
            cameras[i].targetTexture = null;
            Object.Destroy(renderTexture);

            // �x�s���ɮ�
            string fileName = $"Screenshot_Camera{i}_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
            string filePath = Path.Combine(fullPath, fileName);
            byte[] bytes = screenshot.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);

            // ���񯾲z�O����
            Object.Destroy(screenshot);

            Debug.Log($"�I���x�s��: {filePath}");
        }
    }
}