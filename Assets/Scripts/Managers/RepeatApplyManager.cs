using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatApplyManager : MonoBehaviour
{
    
    [SerializeField] private RepeatSlot[] currentRepeatSlots; // 모든 RepeatSlot SO를 여기 넣기
    [SerializeField] private float snapDistance = 0.5f;
    [SerializeField] private Transform parentImage;
    
    [SerializeField] private GameObject repeatContent;
    [SerializeField] private GameObject repeatSlotPrefab;
    [SerializeField] private Image repeatImage;
    private readonly List<GameObject> spawnedSlots = new List<GameObject>();

    public void Initialize(PresentationCard input)
    {
        DeleteRepeatSlots();
        repeatImage.sprite = input.RepeatImage;
        currentRepeatSlots = input.GetAllRepeatSlots(); //키후보 채우기
        UpdateRepeatContents(input.GetAllRepeatCandidates()); //선택후보채우기
    }

    private void UpdateRepeatContents(List<Sprite> repeatCandidates)
    {
        for (int i = 0; i < repeatCandidates.Count; i++)
        {
            // 1) 프리팹 생성 + repeatContent 자식으로
            GameObject slotObj = Instantiate(repeatSlotPrefab, repeatContent.transform);
            spawnedSlots.Add(slotObj);

            // 2) 프리팹 안의 Image 찾아서 sprite 넣기
            Image img = slotObj.GetComponentInChildren<Image>(true);
            if (img != null)
            {
                img.sprite = repeatCandidates[i];
            }
            else
            {
                Debug.LogWarning("repeatSlotPrefab 안에 Image 컴포넌트가 없습니다.");
            }
        }
        
    }

    private void DeleteRepeatSlots()
    {
        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            if (spawnedSlots[i] != null)
                Destroy(spawnedSlots[i]);
        }
        spawnedSlots.Clear();
    }
    public bool IsEffectiveDragDrop(Vector2 dropPos, string key)   //제대로된 위치인지 확인
    {
        var p = GameProgressManager.Instance.GetProgress();
        
        
        foreach (var slot in currentRepeatSlots)
        {
            //Debug.Log(key+"비교대상은"+slot.Key);
            if (slot != null && slot.IsMatch(dropPos, key, snapDistance))
                return true;
        }

        return false;
    }


    public void ApplyRepeat(Sprite spriteToSpawn)
    {
        var p = GameProgressManager.Instance.GetProgress();
        
        RepeatSlot target = null;

        foreach (var slot in currentRepeatSlots)
        {
            if (slot == null) continue;
            if (string.Equals(slot.Key, spriteToSpawn.name))
            {
                target = slot;
                break; 
            }
        }

        if (target == null)
        {
            Debug.LogWarning("Target RepeatSlot not found.");
            return;
        }

        // GameObject 생성
        GameObject go = new GameObject($"Repeat_{spriteToSpawn.name}");
        go.transform.SetParent(parentImage, false);

        // SpriteRenderer 추가
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = spriteToSpawn;

        // ⭐️ 월드 좌표로 설정
        go.transform.localPosition = target.Position; 
        go.transform.localScale = Vector3.one;

        PresentManager.Instance.ApplyRepeat();
    }


}
