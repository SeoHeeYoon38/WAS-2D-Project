using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas rootCanvas;
    [SerializeField] private Vector3 savedWorldScale;
    [SerializeField] private Transform originalParent;
    [SerializeField] private RectTransform rt;
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private float snapDistance = 50f; 
    
    void Awake()
    {
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        // ✅ 최상위 캔버스를 자동으로 잡기
        if (rootCanvas == null)
        {
            var c = GetComponentInParent<Canvas>();
            if (c != null) rootCanvas = c.rootCanvas; // ★ 중요
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        savedWorldScale = transform.lossyScale;   // ✅ 현재 월드 스케일 저장

        transform.SetParent(rootCanvas.transform, true); // true로 월드 유지
        transform.SetAsLastSibling();

        // ✅ 옮긴 뒤에도 월드 스케일 유지하도록 localScale 보정
        var parentScale = rootCanvas.transform.lossyScale;
        transform.localScale = new Vector3(
            savedWorldScale.x / parentScale.x,
            savedWorldScale.y / parentScale.y,
            savedWorldScale.z / parentScale.z
        );

        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ✅ ScreenPoint -> Canvas 로컬 좌표 변환 (모든 Canvas 모드에서 안전)
        RectTransform canvasRt = rootCanvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRt,
                eventData.position,
                rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera,
                out Vector2 localPos))
        {
            rt.anchoredPosition = localPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CheckEffectivePosition())
        {
            transform.SetParent(originalParent, false);
        }
        

            
    }

    private bool CheckEffectivePosition()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = transform.position;

        float dist = Vector3.Distance(myPos, targetPos);

        return dist <= snapDistance;
        
    }
}