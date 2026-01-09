using UnityEngine;
using DG.Tweening;

public class VibeManager : MonoBehaviour
{
    [SerializeField] private Transform target; // 보통 Main Camera
    [SerializeField] private float duration = 2f;
    [SerializeField] private float strength = 3f;
    [SerializeField] private int vibrato = 20;
    [SerializeField] private float randomness = 90f;

    private Tween tween;
    private Vector3 originalPos;

    private void Awake()
    {
        if (target == null)
            target = Camera.main.transform;

        originalPos = target.localPosition;
    }

    public void Vibe()
    {
        if (target == null) return;
        Kill();
        Debug.Log("Vibe!!!");
        originalPos = target.localPosition;

        tween = target.DOShakePosition(
            duration,
            strength,
            vibrato,
            randomness,
            false,              // snapping
            true                // fade out
        ).OnComplete(() =>
        {
            target.localPosition = originalPos; // 원위치 보정
        });
    }

    public void StopVibe()
    {
        Kill();
        if (target != null)
            target.localPosition = originalPos;
    }

    private void Kill()
    {
        if (tween != null && tween.IsActive()) tween.Kill();
        tween = null;
    }
}