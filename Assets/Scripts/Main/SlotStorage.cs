using UnityEngine;

public class SlotStorage : MonoBehaviour
{

    [SerializeField] private ImageSlot[] slots = new ImageSlot[40];

    private const int ChapterCount = 5;
    private const int StageCount = 8;

    public ImageSlot GetImageSlot(int chapter, int stage)
    {
        if (chapter < 1 || chapter > ChapterCount || stage < 1 || stage > StageCount)
        {
            Debug.LogError($"Invalid chapter/stage: {chapter}-{stage}");
            return null;
        }

        int index = (chapter - 1) * StageCount + (stage - 1);

        if (slots == null || slots.Length != ChapterCount * StageCount)
        {
            Debug.LogError($"slots 길이는 {ChapterCount * StageCount} 이어야 합니다.");
            return null;
        }

        var slot = slots[index];
        if (slot == null)
        {
            Debug.LogWarning($"ImageSlot이 비어있음: chapter {chapter}, stage {stage} (index {index})");
        }
        return slot;
    }
}
