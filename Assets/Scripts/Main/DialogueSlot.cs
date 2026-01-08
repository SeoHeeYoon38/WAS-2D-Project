using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Story/Dialogue Slot", fileName = "DialogueSlot")]
public class DialogueSlot : ScriptableObject
{
    [Header("1) 대사 텍스트")]
    [TextArea(2, 6)]
    public string text;

    [Header("2) 효과음")]
    public AudioClip sfx;

    [Header("3) 시작 이벤트")]
    public UnityEvent onStart;

    [Header("4) 끝 이벤트")]
    public UnityEvent onEnd;


    [Header("5) 다음 대사가 있다면")] 
    public DialogueSlot nextDialogueSlot;
}