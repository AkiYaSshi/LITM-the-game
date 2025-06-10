using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskFocusManager : MonoBehaviour
{
    [Tooltip("�P�@�ժ�����M��")]
    [SerializeField] private List<PanelTarget> targetsList;

    [Tooltip("�]�w�s���J�I���s")]
    public static event Action<int> setFocus;

    private float lastFocusTime; // �O���̫�@���I�s�ɶ�
    private const float cooldownTime = 0.4f; // �N�o�ɶ��]��^

    private void Start()
    {
        foreach (var target in targetsList)
        {
            target.anim = target.button.GetComponent<OnClickAnimation>();
        }
    }

    public void SetFocus(int index)
    {
        // �ˬd�N�o�ɶ�
        if (Time.time - lastFocusTime < cooldownTime)
            return;

        if (targetsList[index].anim.reverse) return; // focus�w�B��}�Ҫ��A

        targetsList[index].anim.AnimationStart();
        SetAnimation(index);
        SetDisplayPanel(index);
        setFocus?.Invoke(index);

        lastFocusTime = Time.time; // ��s�̫�I�s�ɶ�
    }

    private void SetAnimation(int index)
    {
        for (int i = 0; i < targetsList.Count; i++)
        {
            if (i != index)
            {
                if (targetsList[i].anim.reverse)
                {
                    targetsList[i].anim.AnimationStart();
                }
            }
        }
    }

    void SetDisplayPanel(int index)
    {
        for (int i = 0; i < targetsList.Count; i++)
        {
            targetsList[i].panel.GetComponent<Canvas>().enabled = i == index;
        }
    }
}

[System.Serializable]
public class PanelTarget
{
    [Tooltip("���s")]
    public GameObject button;

    [Tooltip("���O")]
    public GameObject panel;

    [Tooltip("���s�I���ɪ��a���ʵe")]
    public OnClickAnimation anim;
}
