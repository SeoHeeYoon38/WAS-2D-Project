using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Story/PresentationCard", fileName = "PresentationCard")]
public class PresentationCard : ScriptableObject
{
    [Header("Card Identity")]
    [SerializeField] private int stage;
    [SerializeField] private int chapter;
    
    [Header("Fixed select numbers per card")]
    [SerializeField] private int[] selectNumbers = new int[3];
    
    
    [Header("Slots")]
    [SerializeField] private List<ImageSlot> imageSlots = new();
    [SerializeField] private List<RepeatSlot> repeatSlots = new();
    [SerializeField] private List<Sprite> repeatCandidates = new();
    
    [Header("RepeatImage")]
    [SerializeField] private Sprite repeatImage ;


    [Header("AnimationCard")]
    [SerializeField] private AnimationCard animationCard;
    
    [Header("Idx")]
    [SerializeField] private int currentImageIdx=0;
    [SerializeField] private int currentRepeatIdx=0;
    
    public int Stage => stage;
    public int Chapter => chapter;
    public Sprite RepeatImage => repeatImage;
    
    public AnimationCard AnimationCard => animationCard;
    public IReadOnlyList<int> SelectNumbers => selectNumbers;

    public ImageSlot GetCurrentImageSlot()
    {
        if (currentImageIdx < 0 || currentImageIdx >= imageSlots.Count) return null;
        Debug.Log(imageSlots[currentImageIdx].sprite.name);
        return imageSlots[currentImageIdx];
    }

    public bool CheckIsImageSlotRemain()
    {
        return currentImageIdx < imageSlots.Count;
    }

    public void PlusImageSlotIdx()
    {
        currentImageIdx++;
    }


    public bool CheckIsAllSelectComplete(int A,int B,int C)
    {
        return A==selectNumbers[0]&&B==selectNumbers[1]&&C==selectNumbers[2];
    }


    
    
    
    
    public void PlusRepeatIdx()
    {
        Debug.Log("하나추가요");
        currentRepeatIdx++;
    }

    public RepeatSlot[] GetAllRepeatSlots()
    {
        return repeatSlots.ToArray();
    }

    public List<Sprite> GetAllRepeatCandidates()
    {
        return repeatCandidates;
    }
    public bool CheckIsAllRepeatApplied()
    {
        return currentRepeatIdx <= repeatSlots.Count;
    }
    

}
