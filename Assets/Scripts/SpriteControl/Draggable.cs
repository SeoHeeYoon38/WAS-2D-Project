using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Canvas rootCanvas;

    private Image myImage;
    private CanvasGroup cg;

    private static Sprite draggingSprite;
    private static Image fromImage;

    private static Image previewImage;
    private static RectTransform previewRT;

    void Awake()
    {
        myImage = GetComponent<Image>();

        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        if (rootCanvas == null)
        {
            var c = GetComponentInParent<Canvas>();
            if (c != null) rootCanvas = c.rootCanvas;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (myImage.sprite == null) return;

        draggingSprite = myImage.sprite;
        fromImage = myImage;
        
        cg.alpha = 0.3f;

 
        CreatePreview(eventData);
        MovePreview(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        MovePreview(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        RectTransform canvasRT = (RectTransform)rootCanvas.transform;
        Camera cam = rootCanvas.worldCamera != null ? rootCanvas.worldCamera : Camera.main;
        
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRT, eventData.position, cam, out worldPos))
        {
            Debug.Log("Drop (anywhere) World Pos = " + worldPos);
        
            RepeatApplyManager repeatApplyManager = PresentManager.Instance.RepeatApplyManager;
            
            var sprite = draggingSprite;
            
            if (repeatApplyManager.IsEffectiveDragDrop((Vector2)worldPos, draggingSprite.name))
            {//성공
                Debug.Log("성공!");
                cg.alpha = 0f;
                DestroyPreview();
        
                repeatApplyManager.ApplyRepeat(sprite);
        
                draggingSprite = null;
                fromImage = null;
            }
            else
            { //실패
                cg.alpha = 1f;
                DestroyPreview();
        
                draggingSprite = null;
                fromImage = null;
            }
        
        }
        else
        {
            cg.alpha = 1f;
            DestroyPreview();
            draggingSprite = null;
            fromImage = null;
        }



    }

    public void OnDrop(PointerEventData eventData)
    {
        
        if (draggingSprite == null) return;
        
        // 기존 스왑 로직
        Sprite temp = myImage.sprite;
        myImage.sprite = draggingSprite;
        
        //다 알파값 1로 변환
        cg.alpha = 1f;
        CanvasGroup canvasGroup =fromImage.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        
        DestroyPreview();
        if (fromImage != null) fromImage.sprite = temp;
    }

    private void CreatePreview(PointerEventData eventData)
    {
        if (previewImage != null) Destroy(previewImage.gameObject);

        var go = new GameObject("DragginItem", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        go.transform.SetParent(rootCanvas.transform, false);
        go.transform.SetAsLastSibling();
        
        go.layer = rootCanvas.gameObject.layer;

        previewImage = go.GetComponent<Image>();
        previewImage.sprite = draggingSprite;
        previewImage.raycastTarget = false;
        previewImage.color = Color.white;
        previewImage.preserveAspect = true; 

        previewRT = previewImage.rectTransform;

      
        previewRT.anchorMin = previewRT.anchorMax = new Vector2(0.5f, 0.5f);
        previewRT.pivot = new Vector2(0.5f, 0.5f);
        
        previewRT.sizeDelta = new Vector2(100, 100);

   
        previewRT.localScale = Vector3.one;
        previewRT.localRotation = Quaternion.identity;
        previewRT.localPosition = Vector3.zero; 
   
        RectTransform canvasRT = (RectTransform)rootCanvas.transform;
        Camera cam = rootCanvas.worldCamera != null ? rootCanvas.worldCamera : Camera.main;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, eventData.position, cam, out Vector2 localPos))
            previewRT.anchoredPosition = localPos;
        else
            previewRT.anchoredPosition = Vector2.zero;

    }

    private void MovePreview(PointerEventData eventData)
    {
        if (previewRT == null) return;

        RectTransform canvasRT = rootCanvas.transform as RectTransform;
        Camera cam = rootCanvas.worldCamera; 

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, eventData.position, cam, out Vector2 localPos))
        {
            previewRT.anchoredPosition = localPos;
        }
    }

    private void DestroyPreview()
    {
        if (previewRT != null) Destroy(previewRT.gameObject);
        previewRT = null;
        previewImage = null;
    }
}
