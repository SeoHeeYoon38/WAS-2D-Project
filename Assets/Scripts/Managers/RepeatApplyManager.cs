using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatApplyManager : MonoBehaviour
{
    
    [SerializeField] private RepeatSlot[] allSlots; // 모든 RepeatSlot SO를 여기 넣기
    [SerializeField] private float snapDistance = 50f;
    [SerializeField] private Transform parentImage;
    private readonly Dictionary<(int stage, int chapter), List<RepeatSlot>> _map = new();

    void Awake() // 매핑된 모든 슬롯 정리하기 
    {
        _map.Clear();

        foreach (var slot in allSlots)
        {
            if (slot == null) continue;

            var k = (slot.Stage, slot.Chapter);

            if (!_map.TryGetValue(k, out var list))
            {
                list = new List<RepeatSlot>(4);
                _map.Add(k, list);
            }

            list.Add(slot);
        }

        // 선택: stage/chapter당 4개인지 체크
        foreach (var kv in _map)
        {
            if (kv.Value.Count != 4)
                Debug.LogWarning($"(stage,chapter)=({kv.Key.stage},{kv.Key.chapter}) 슬롯 개수가 {kv.Value.Count}개입니다잉 (정상은 4개).");
        }
    }

    public bool IsEffectiveDragDrop(Vector2 dropPos, string key)   //제대로된 위치인지 확인
    {
        var p = GameProgressManager.Instance.GetProgress();

        if (!_map.TryGetValue((p.stage, p.chapter), out var slots))
            return false;

        foreach (var slot in slots)
        {
            if (slot != null && slot.IsMatch(dropPos, key, snapDistance))
                return true;
        }

        return false;
    }


    public void ApplyRepeat(Sprite spriteToSpawn)
    {
        var p = GameProgressManager.Instance.GetProgress();
        
        if (!_map.TryGetValue((p.stage, p.chapter), out var slots))
            return;
        
        RepeatSlot target = null;

        foreach (var slot in slots) // 해당하는 repeatslot 찾기
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
        
    }


}
