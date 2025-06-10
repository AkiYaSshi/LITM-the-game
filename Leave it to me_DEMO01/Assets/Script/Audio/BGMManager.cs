using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgm;
    [SerializeField]
    private AudioSource mouseDown;
    [SerializeField]
    private AudioSource buttonClick;
    [SerializeField]
    private AudioSource dialoguePop;
    [SerializeField]
    private List<AudioSource> speaking;
    [SerializeField]
    private AudioSource putObject;

    private void Start()
    {
        bgm.enabled = true;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseDown?.Play();
        }
    }
    public void ButtonClick()
    {
        buttonClick?.Play();
    }

    public void SetObject()
    {
        putObject?.Play();
    }

    public void DialoguePop()
    {
        dialoguePop?.Play();
    }
    public void Speaking()
    {
        int i = Random.Range(0, speaking.Count);
        speaking[i]?.Play();
    }

    private void OnEnable()
    {
        DialogueStream.nextDialogue += DialoguePop;
        DialogueStream.nextDialogue += Speaking;
        ObjectTransformer.DownObject += SetObject;
    }
    private void OnDisable()
    {
        DialogueStream.nextDialogue -= DialoguePop;
        DialogueStream.nextDialogue -= Speaking;
        ObjectTransformer.DownObject -= SetObject;
    }
}
