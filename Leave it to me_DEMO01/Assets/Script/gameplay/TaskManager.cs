using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    [Tooltip("任務列表的 Canvas 物件")]
    [SerializeField] private Canvas taskCanvas;
    [Tooltip("任務完成提示的 UI Image")]
    [SerializeField] private Image completionPopup;
    [Tooltip("完成提示文字")]
    [SerializeField] private TMP_Text completionText;
    [Tooltip("顯示任務提示的模板")]
    [SerializeField] private GameObject missionText;
    [Tooltip("任務列表")]
    [SerializeField] private List<TaskData> tasks = new();

    public static event Action AllComplete;

    private int TaskFinished = 0;

    private void Start()
    {
        GenerateTaskList();
        SetTaskContent();
    }

    void GenerateTaskList()
    {
        if (taskCanvas == null) return;

        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject textObj = Instantiate(missionText);
            textObj.name = $"{i}_{tasks[i].name}";
            textObj.transform.position = new Vector3(textObj.transform.position.x,
                                                    textObj.transform.position.y - i * 120,
                                                    textObj.transform.position.z);
            textObj.transform.SetParent(taskCanvas.transform, false);

            TMP_Text taskText = textObj.GetComponent<TextMeshProUGUI>();
            taskText.text = "▣　" + tasks[i].name;
            taskText.color = tasks[i].isCompleted ? Color.green : Color.black;
        }
    }

    public void CheckTaskCompletion()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (!tasks[i].isCompleted && tasks[i].condition?.Invoke() == true)
            {
                TaskFinished ++;

                Debug.Log($"{tasks[i].name}已達成！");
                tasks[i].isCompleted = true;

                UpdateTaskList();

                tasks[i].OnTaskComplete.Invoke();
                ShowCompletionPopup($"{tasks[i].shortName} 已達成！");
            }
        }
    }

    void UpdateTaskList()
    {
        foreach (Transform child in taskCanvas.transform)
        {
            TMP_Text taskText = child.GetComponent<TMP_Text>();
            if (taskText != null)
            {
                int index = int.Parse(child.name.Split('_')[0]); // 修正索引提取
                taskText.color = tasks[index].isCompleted ? Color.green : Color.black;
            }
        }

        if(TaskFinished == tasks.Count) AllComplete?.Invoke();

    }

    void ShowCompletionPopup(string taskName)
    {
        completionPopup.gameObject.SetActive(true);
        completionText.text = taskName;

        StartCoroutine(HidePopupAfterDelay(3f));
    }

    IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        completionPopup.gameObject.SetActive(false);
    }

    void SetTaskContent()
    {
        tasks[0].condition = () => ObjectScanner.ObjectCount(7) > 2
                                 || ObjectScanner.ObjectCount(7) > 0 && ObjectScanner.ObjectCount(8) > 0
                                 || ObjectScanner.ObjectCount(8) > 2;

        tasks[1].condition = () => ObjectScanner.ObjectCount(10) > 0;

        tasks[2].condition = () => ObjectScanner.DetectObject(7) && ObjectScanner.DetectObject(5)
                                   || ObjectScanner.DetectObject(8) && ObjectScanner.DetectObject(5);

        tasks[3].condition = () => ObjectScanner.DetectObject(7) && ObjectScanner.DetectObject(15)
                                   || ObjectScanner.DetectObject(8) && ObjectScanner.DetectObject(15);

        tasks[4].condition = () => ObjectScanner.DetectObject(4);
    }
    private void OnEnable()
    {
        SummonObjectManager.NewObject += CheckTaskCompletion;
    }
    private void OnDisable()
    {
        SummonObjectManager.NewObject -= CheckTaskCompletion;
    }
}

/// <summary>
/// 任務的結構
/// </summary>
[System.Serializable]
public class TaskData
{
    [Tooltip("任務內容")]
    public string name;
    [Tooltip("任務簡稱")]
    public string shortName;
    [Tooltip("任務完成狀態")]
    public bool isCompleted;
    [Tooltip("任務達成條件")]
    public System.Func<bool> condition;
    [Tooltip("任務達成發生事件")]
    public UnityEvent OnTaskComplete;
}