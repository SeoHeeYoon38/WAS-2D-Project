using UnityEngine;
using DG.Tweening;
using TMPro;
public class SelfDialogueManager : MonoBehaviour
{
    [Header("�г� ��Ʈ (CanvasGroup �ʼ�)")]
    [SerializeField] private CanvasGroup dialoguePanel;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("���̵� �� �ð�")]
    [SerializeField] private float fadeTime = 0.5f;

    private Tween fadeTween;



    private void Awake()
    {
        // ���� �� ����
        dialoguePanel.alpha = 0f;
        dialoguePanel.gameObject.SetActive(false);
    }


    public void ShowDialogue(string message)
    {
        // �ؽ�Ʈ ����
        dialogueText.text = message;

        // �г� Ȱ��ȭ
        dialoguePanel.gameObject.SetActive(true);

        // ���� Ʈ�� ������ ����
        fadeTween?.Kill();

        // ���� �ʱ�ȭ
        dialoguePanel.alpha = 0f;

        // ���̵� ��
        fadeTween = dialoguePanel.DOFade(1f, fadeTime)
            .SetEase(Ease.OutQuad);
    }


    public void HideDialogue()
    {
        fadeTween?.Kill();                 // ���̵� ���̾����� ����
        dialogueText.text = "";            // �ؽ�Ʈ �� ���̰�(����)
        dialoguePanel.alpha = 0f;          // ����
        dialoguePanel.gameObject.SetActive(false); // ��Ȱ��ȭ(��)
        
        PresentManager.Instance.DialogueManager.StartNext();
    }

}
