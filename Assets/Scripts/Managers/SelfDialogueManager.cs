using UnityEngine;
using DG.Tweening;
using TMPro;
public class SelfDialogueManager : MonoBehaviour
{
    [Header("패널 루트 (CanvasGroup 필수)")]
    [SerializeField] private CanvasGroup dialoguePanel;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("페이드 인 시간")]
    [SerializeField] private float fadeTime = 0.5f;

    private Tween fadeTween;



    private void Awake()
    {
        // 시작 시 숨김
        dialoguePanel.alpha = 0f;
        dialoguePanel.gameObject.SetActive(false);
    }


    public void ShowDialogue(string message)
    {
        // 텍스트 변경
        dialogueText.text = message;

        // 패널 활성화
        dialoguePanel.gameObject.SetActive(true);

        // 기존 트윈 있으면 제거
        fadeTween?.Kill();

        // 알파 초기화
        dialoguePanel.alpha = 0f;

        // 페이드 인
        fadeTween = dialoguePanel.DOFade(1f, fadeTime)
            .SetEase(Ease.OutQuad);
    }


    public void HideDialogue()
    {
        fadeTween?.Kill();                 // 페이드 중이었으면 끊고
        dialogueText.text = "";            // 텍스트 안 보이게(비우기)
        dialoguePanel.alpha = 0f;          // 투명
        dialoguePanel.gameObject.SetActive(false); // 비활성화(팟)
    }

}
