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
        //SetImageSlot();
    }


    public void SetImageSlot()
    {
        PlayerProgress progress = GameProgressManager.Instance.GetProgress();
        if (progress != null)
        {
            ImageSlot slot=slotStorage.GetImageSlot(progress.chapter, progress.stage); //이미지 슬롯에서 이미지 가져와서 이미지 바꾸기

            targetImage.sprite = slot.sprite; //이미지 셋

            dialogueManager.SetDialogueSlot(slot.dialogueSlot); // 대화 셋
            
            //slot.onStart?.Invoke();//시작시 원하는 이벤트 있을시 실행

        }
    }

    
}
