using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatApplyManager : MonoBehaviour
{
    
    [SerializeField] private RepeatSlot[] currentRepeatSlots; // 모든 RepeatSlot SO를 여기 넣기
    [SerializeField] private float snapDistance = 50f;
    [SerializeField] private Transform parentImage;


    public void Initialize(RepeatSlot[] repeatSlots)
    {
        currentRepeatSlots = repeatSlots;
    }
    
    public bool IsEffectiveDragDrop(Vector2 dropPos, string key)   //제대로된 위치인지 확인
    {
        var p = GameProgressManager.Instance.GetProgress();
        
        foreach (var slot in currentRepeatSlots)
        {
            if (slot != null && slot.IsMatch(dropPos, key, snapDistance))
                return true;
        }

        return false;
    }


    public void ApplyRepeat(Sprite spriteToSpawn)
    {
        var p = GameProgressManager.Instance.GetProgress();
        
        RepeatSlot target = null;

        foreach (var slot in currentRepeatSlots) // 해당하는 repeatslot 찾기
        {
            if (slot == null) continue;
            if (string.Equals(slot.Key, spriteToSpawn.name, StringComparison.Ordinal))
            {
                target = slot;
                break; 
            }
        }

        GameObject go = new GameObject($"Repeat_{spriteToSpawn.name}", typeof(RectTransform));
        go.transform.SetParent(parentImage, false);

    
        Image img = go.AddComponent<Image>();
        img.sprite = spriteToSpawn;
        img.preserveAspect = true;
        img.raycastTarget = false;

        RectTransform rt = img.rectTransform;
        rt.anchoredPosition = target.Position;
        rt.localScale = Vector3.one;
        rt.sizeDelta = target.Size;

        PresentManager.Instance.ApplyRepeat();
    }


}
