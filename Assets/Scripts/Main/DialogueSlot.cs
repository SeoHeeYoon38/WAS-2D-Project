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
}