using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// 챕터 시작 연출 패널.
/// 검은 화면에 "CHAPTER n" + 챕터 이름을 띄우고 다시 사라진다.
/// (각 챕터 첫 스테이지 진입 시에만 챕터 이름을 함께 표시)
/// </summary>
public class ChapterIntroUI : MonoBehaviour
{
    [Header("Panel (CanvasGroup 필수)")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI chapterNumberText;
    [SerializeField] private TextMeshProUGUI chapterTitleText;

    [Header("Timing")]
    [SerializeField] private float fadeInTime = 0.6f;
    [SerializeField] private float holdTime = 1.6f;
    [SerializeField] private float fadeOutTime = 0.6f;

    private Sequence sequence;

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    /// <summary>챕터 번호만 표시 (2~8 스테이지 진입 등)</summary>
    public void Show(int chapter, Action onComplete = null)
    {
        Show(chapter, null, onComplete);
    }

    /// <summary>챕터 번호 + 챕터 이름 표시 (챕터 첫 스테이지 진입)</summary>
    public void Show(int chapter, string chapterTitle, Action onComplete = null)
    {
        chapterNumberText.text = $"CHAPTER {chapter}";

        bool hasTitle = !string.IsNullOrEmpty(chapterTitle);
        chapterTitleText.text = hasTitle ? chapterTitle : string.Empty;
        chapterTitleText.gameObject.SetActive(hasTitle);

        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;

        sequence?.Kill();
        sequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(1f, fadeInTime).SetEase(Ease.OutQuad))
            .AppendInterval(holdTime)
            .Append(canvasGroup.DOFade(0f, fadeOutTime).SetEase(Ease.InQuad))
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
    }

    public void Hide()
    {
        sequence?.Kill();
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
