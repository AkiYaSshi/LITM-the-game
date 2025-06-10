using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class LoadScreenshotToUI : MonoBehaviour
{
    [Tooltip("��ܺI�Ϫ� UI Image")]
    [SerializeField] private Image uiImage;

    [Tooltip("�x�s�I�Ϫ���Ƨ����|�]�۹�� Application.dataPath�^")]
    [SerializeField] private string saveFolder = "Screenshots";

    void Start()
    {
        if (uiImage == null)
        {
            Debug.LogError("�Цb Inspector ���j�w UI Image �M Load Button");
            return;
        }

        // �j�w���s�I���ƥ�
        LoadLatestScreenshot();
    }

    void LoadLatestScreenshot()
    {
        string fullPath = Path.Combine(UnityEngine.Application.persistentDataPath, saveFolder);
        if (!Directory.Exists(fullPath))
        {
            Debug.LogError("�I�ϸ�Ƨ����s�b");
            return;
        }

        // ����̷s�I�ϡ]�ھڮɶ��Ƨǡ^
        string[] files = Directory.GetFiles(fullPath, "*.png")
            .OrderByDescending(f => new FileInfo(f).LastWriteTime)
            .ToArray();

        if (files.Length == 0)
        {
            Debug.LogWarning("�L�i�[�����I��");
            return;
        }

        string latestFile = files[0];
        StartCoroutine(LoadTexture(latestFile));
    }

    private System.Collections.IEnumerator LoadTexture(string filePath)
    {
        // ���B�[�����z
        WWW www = new("file://" + filePath);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError($"�[���I�ϥ���: {www.error}");
            yield break;
        }

        Texture2D texture = www.texture;
        if (texture == null)
        {
            Debug.LogError("���z�[������");
            yield break;
        }

        // �N���z�ର Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // ���Ψ� UI Image
        uiImage.sprite = sprite;
        uiImage.preserveAspect = true; // �O�����e��

        // ����귽
        www.Dispose();
        Resources.UnloadUnusedAssets();
    }
}