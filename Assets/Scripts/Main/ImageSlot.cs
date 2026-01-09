using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Story/Image Slot", fileName = "ImageSlot")]
public class ImageSlot : ScriptableObject
{
    [Header("1) ������ �̹���")]
    public Sprite sprite;
    
    [Header("2) ���� ��� ")] 
    public DialogueSlot dialogueSlot;

}