using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Story/Image Slot", fileName = "ImageSlot")]
public class ImageSlot : ScriptableObject
{
    [Header("1) ������ �̹���")]
    public Sprite sprite;
    
    [Header("2) ���� ��� ")] 
    public DialogueSlot dialogueSlot;

    [Header("3) 시작시 효과 ")] 
    public bool startFade;
    public float startFadeDuration;

    public bool startVibe;
    public float startIntense;
    
    [Header("3) 종료시 효과 ")]
    
    public bool endFade;
    public float endFadeDuration;

    public bool endVibe;
    public float endIntense;
    
}