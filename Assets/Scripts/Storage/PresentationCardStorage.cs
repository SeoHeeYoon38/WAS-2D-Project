using UnityEngine;

public class PresentationCardStorage : MonoBehaviour
{

    [SerializeField] private PresentationCard[] slots = new PresentationCard[40];

    private const int ChapterCount = 5;
    private const int StageCount = 8;

    public PresentationCard GetPresentationCard(int chapter, int stage)
    {
        if (chapter < 1 || chapter > ChapterCount || stage < 1 || stage > StageCount)
        {
            Debug.LogError($"Invalid chapter/stage: {chapter}-{stage}");
            return null;
        }

        int index = (chapter - 1) * StageCount + (stage - 1);

        if (slots == null || slots.Length != ChapterCount * StageCount)
        {
            Debug.LogError($"slots ���̴� {ChapterCount * StageCount} �̾�� �մϴ�.");
            return null;
        }

        var slot = slots[index];
        if (slot == null)
        {
            Debug.LogWarning($"ImageSlot�� �������: chapter {chapter}, stage {stage} (index {index})");
        }
        return slot;
    }
}
