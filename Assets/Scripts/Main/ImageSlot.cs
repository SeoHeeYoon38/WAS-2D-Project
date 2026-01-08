using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Story/Image Slot", fileName = "ImageSlot")]
public class ImageSlot : ScriptableObject
{
    [Header("1) 보여줄 이미지")]
    public Sprite sprite;

    [Header("2) 이미지 시작 이벤트")]
    public UnityEvent onStart;

    [Header("3) 이미지 끝 이벤트")]
    public UnityEvent onEnd;

    [Header("4) 나올 대사 ")] 
    public DialogueSlot dialogueSlot;

}