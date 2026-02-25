using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject dialoguePanel;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Typing")]
    [SerializeField] private float charsPerSecond = 30f;

    [SerializeField] private SelfDialogueManager selfDialogueManager;
    public SelfDialogueManager SelfDialogueManager => selfDialogueManager;


    private Tween typingTween;
    private DialogueSlot currentDialogueSlot;
    private string currentLine = "";
    private bool isTyping = false;
    private bool isLineFullyShown = false;

    private void Awake()
    {
        HideDialogue();
    }


    private void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
    }
    private void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

        private void Update()
        {
            if (Keyboard.current != null &&
                Keyboard.current.enterKey.wasPressedThisFrame)
            {
                HandleAdvanceInput();
            }
        }


    public void SetDialogueSlot(DialogueSlot target) // ���� ��ȭ ���� ���� 
    {
        
        currentDialogueSlot = target;
        if (currentDialogueSlot != null)
        {
            Debug.Log(currentDialogueSlot.text);
        }
        else
        {
            Debug.Log("current is null");
        }


        if (target.isAnswer)
        {
            HideDialogue();
            selfDialogueManager.ShowDialogue(target.text);
        }
        else
        {
            StartDialogue();
        }
        
        
    }

    public void StartDialogue() //��ȭ���� 
    {
        ShowDialogue();
        
        string line = currentDialogueSlot.text;

        KillTypingTween();

        currentLine = line;
        dialogueText.text = "";
        isTyping = true;
        isLineFullyShown = false;

        float duration = Mathf.Max(0.01f, currentLine.Length / charsPerSecond);

        typingTween = DOTween.To(
            () => dialogueText.text.Length,
            x => dialogueText.text = currentLine.Substring(0, x),
            currentLine.Length,
            duration
        )
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            isTyping = false;
            isLineFullyShown = true;
            dialogueText.text = currentLine;
        });
    }

    public void StartDialogue(string input) //��ȭ���� 
    {
        string line = input;

        KillTypingTween();

        currentLine = line;
        dialogueText.text = "";
        isTyping = true;
        isLineFullyShown = false;

        float duration = Mathf.Max(0.01f, currentLine.Length / charsPerSecond);

        typingTween = DOTween.To(
                () => dialogueText.text.Length,
                x => dialogueText.text = currentLine.Substring(0, x),
                currentLine.Length,
                duration
            )
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                isTyping = false;
                isLineFullyShown = true;
                dialogueText.text = currentLine;
            });
    }

    private void HandleAdvanceInput() // ��ȭ ���� 
    {
        
        if (isTyping && !isLineFullyShown)
        {
            ForceCompleteLine();
            return;
        }


        if (isLineFullyShown)
        {
            StartNext();
        }
    }

    private void ForceCompleteLine()
    {
        KillTypingTween();
        dialogueText.text = currentLine;
        isTyping = false;
        isLineFullyShown = true;
    }

    public void StartNext()
    {
        if (currentDialogueSlot.nextDialogueSlot != null) // ���� ��ȭ�� �����ϸ� �״�� ��� 
        {
            SetDialogueSlot(currentDialogueSlot.nextDialogueSlot);
        }
        else //��ȭ�� ������ �̹��� �����̳� �ٸ��� �� 
        {
            HideDialogue();
            //GameProgressManager.Instance.UpProgress();
            PresentManager.Instance.PresentNextImageSlot();
        }
      
    }

    private void KillTypingTween()
    {
        if (typingTween != null && typingTween.IsActive())
            typingTween.Kill();
        typingTween = null;
    }
}
