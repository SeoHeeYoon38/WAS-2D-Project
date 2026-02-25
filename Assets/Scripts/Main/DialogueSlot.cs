using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Story/Dialogue Slot", fileName = "DialogueSlot")]
public class DialogueSlot : ScriptableObject
{
    [Header("1) ��� �ؽ�Ʈ")]
    [TextArea(2, 6)]
    public string text;

    [Header("2) ȿ����")]
    public AudioClip sfx;
    
    [Header("3) ���� ��簡 �ִٸ�")] 
    public DialogueSlot nextDialogueSlot;
    
    [Header("4) 자문 자답 상호작용 ")]
    
    
    
    [Header("5) 시작시 효과 ")] 
    public bool startFade;
    public float startFadeDuration;

    public bool startVibe;
    public float startIntense;
    
    [Header("6) 종료시 효과 ")]
    
    public bool endFade;
    public float endFadeDuration;

    public bool endVibe;
    public float endIntense;
    

    [Header("7) 종류 ")] 
    public bool isAnswer;

}