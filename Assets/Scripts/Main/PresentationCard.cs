using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/PresentationCard", fileName = "PresentationCard")]
public class PresentationCard : ScriptableObject
{
    [Header("Card Identity")]
    [SerializeField] private int stage;
    [SerializeField] private int chapter;

    [Header("Slots")]
    [SerializeField] private List<ImageSlot> imageSlots = new();
    [SerializeField] private List<RepeatSlot> repeatSlots = new();

    [Header("Fixed select numbers per card")]
    [SerializeField] private int[] selectNumbers = new int[3];

    
    [Header("Idx")]
    [SerializeField] private int currentImageIdx=0;
    [SerializeField] private int currentRepeatIdx=0;
    
    public int Stage => stage;
    public int Chapter => chapter;
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
        currentRepeatIdx++;
    }

    public RepeatSlot[] GetAllRepeatSlots()
    {
        return repeatSlots.ToArray();
    }
    
    public bool CheckIsAllRepeatApplied()
    {
        return currentRepeatIdx < repeatSlots.Count;
    }
    

}
