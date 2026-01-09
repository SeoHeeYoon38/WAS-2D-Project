using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PresentManager : MonoBehaviour
{

    public static PresentManager Instance;

    [SerializeField] private SelectManager selectManager;
    [SerializeField] private SelfDialogueManager selfDialogueManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private RepeatManager repeatManager;
    [SerializeField] private RepeatApplyManager repeatApplyManager;

    public SelectManager SelectManager => selectManager;
    public SelfDialogueManager SelfDialogueManager => selfDialogueManager;
    public DialogueManager DialogueManager => dialogueManager;
    public RepeatManager RepeatManager => repeatManager;
    public RepeatApplyManager RepeatApplyManager => repeatApplyManager;
    
    

    [SerializeField] private Image targetImage;
    [SerializeField] private SlotStorage slotStorage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) HandleDebugInput(1);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) HandleDebugInput(2);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) HandleDebugInput(3);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) HandleDebugInput(4);
        if (Keyboard.current.digit5Key.wasPressedThisFrame) HandleDebugInput(5);
    }

    private void HandleDebugInput(int key)
    {
        switch (key)
        {
            case 1:
                SetImageSlot();
                break;
            case 2:
                selfDialogueManager.ShowDialogue("HelloWorld");
                break;
            case 3:
                selectManager.ShowSelectPanel();
                break;
            case 4:
                repeatManager.ShowPanel();
                break;
            case 5:
               
                break;
        }
    }


    private void Start()
    {
        selectManager = GetComponentInChildren<SelectManager>();
        selfDialogueManager = GetComponentInChildren<SelfDialogueManager>();
        dialogueManager = GetComponentInChildren<DialogueManager>();
        repeatManager = GetComponentInChildren<RepeatManager>();
        repeatApplyManager = GetComponentInChildren<RepeatApplyManager>();
        //SetImageSlot();
    }


    public void SetImageSlot()
    {
        PlayerProgress progress = GameProgressManager.Instance.GetProgress();
        if (progress != null)
        {
            ImageSlot slot=slotStorage.GetImageSlot(progress.chapter, progress.stage); //�̹��� ���Կ��� �̹��� �����ͼ� �̹��� �ٲٱ�

            targetImage.sprite = slot.sprite; //�̹��� ��

            dialogueManager.SetDialogueSlot(slot.dialogueSlot); // ��ȭ ��
            
            //slot.onStart?.Invoke();//���۽� ���ϴ� �̺�Ʈ ������ ����

        }
    }

  

    
}
