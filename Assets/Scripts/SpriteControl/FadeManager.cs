using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeEvent : MonoBehaviour
{
    [SerializeField] private Graphic target;   // Image, Text 등 Graphic이면 다 됨
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private bool blockRaycastWhileOpaque = true;

    private Tween tween;

    public void FadeIn()  // 1 -> 0 (사라짐)
    {
        if (target == null) return;
        Kill();
        if (blockRaycastWhileOpaque) target.raycastTarget = true;

        tween = target.DOFade(0f, duration).SetEase(Ease.Linear)
            .OnComplete(() => { if (blockRaycastWhileOpaque) target.raycastTarget = false; });
    }

    public void FadeOut() // 0 -> 1 (나타남)
    {
        if (target == null) return;
        Kill();
        if (blockRaycastWhileOpaque) target.raycastTarget = true;
        tween = target.DOFade(1f, duration).SetEase(Ease.Linear);
    }

    private void Kill()
    {
        if (tween != null && tween.IsActive()) tween.Kill();
        tween = null;
    }
}