using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PresentManager : MonoBehaviour
{

    public static PresentManager Instance;

    [SerializeField] private SelectManager selectManager;
    [SerializeField] private SelfDialogueManager selfDialogueManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private RepeatManager repeatManager;
    [SerializeField] private RepeatApplyManager repeatApplyManager;
    
    [SerializeField] private FadeManager fadeManager;
    [SerializeField] private VibeManager vibeManager;
    public SelectManager SelectManager => selectManager;
    public SelfDialogueManager SelfDialogueManager => selfDialogueManager;
    public DialogueManager DialogueManager => dialogueManager;
    public RepeatManager RepeatManager => repeatManager;
    public RepeatApplyManager RepeatApplyManager => repeatApplyManager;
    
    

    [SerializeField] private Image targetImage;
    [SerializeField] private PresentationCardStorage presentationCardStorage;
    [SerializeField] private PresentationCard currentPresentationCard;
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
                StartPresentation();
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
        fadeManager = GetComponentInChildren<FadeManager>();
        vibeManager = GetComponentInChildren<VibeManager>();
        //StartPresentation();
    }


    public void StartPresentation() //presentation card initialize
    {
        PlayerProgress progress = GameProgressManager.Instance.GetProgress();
        if (progress != null)
        {
            currentPresentationCard=presentationCardStorage.GetPresentationCard(progress.chapter, progress.stage); //�̹��� ���Կ��� �̹��� �����ͼ� �̹��� �ٲٱ�
            
        }
        
        PresentImageSlot();
    }
   
    public void PresentNextImageSlot()
    {
        ImageSlot imageSlot = currentPresentationCard.GetCurrentImageSlot();
        
        if (imageSlot.endFade)  //presentend
        {
            fadeManager.FadeIn();
        }
        else
        {
            
        }
        
        
        if (imageSlot.endVibe)
        {
            vibeManager.Vibe();
        }
        else
        {
            
        }
        
        
        
        currentPresentationCard.PlusImageSlotIdx(); // read on imageslot
        
        if (currentPresentationCard.CheckIsImageSlotRemain()) //isremain?
        {
            PresentImageSlot();
        }
        else // yes
        {
            PresentSelectImage();
        }
    }

    private void PresentImageSlot()
    {
        ImageSlot imageSlot = currentPresentationCard.GetCurrentImageSlot();

        if (imageSlot.startFade)
        {
            fadeManager.FadeOut();
        }
        else
        {
            
        }
        
        
        if (imageSlot.startVibe)
        {
            vibeManager.Vibe();
        }
        else
        {
            
        }
        
        
        
        
        targetImage.sprite = imageSlot.sprite; //�̹��� ��

        dialogueManager.SetDialogueSlot(imageSlot.dialogueSlot); // ��ȭ ��
    }

    
    
    
    private void PresentSelectImage()
    {
        selectManager.ShowSelectPanel();
    }

    public void SwitchSelectImage(int v1, int v2, int v3)
    {
        if (currentPresentationCard.CheckIsAllSelectComplete(v1, v2, v3))//if all complete
        {
            selectManager.HideSelectPanel();
            repeatApplyManager.Initialize(currentPresentationCard.GetAllRepeatSlots());
            repeatManager.ShowPanel();
        }
        else // is not 
        {
            
        }
        
    }

    public void ApplyRepeat()
    {
        currentPresentationCard.PlusRepeatIdx();
        
        if (currentPresentationCard.CheckIsAllRepeatApplied())
        {
           //go animation  
        }
        
    }
    
    
}
