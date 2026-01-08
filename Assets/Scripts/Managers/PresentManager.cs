using UnityEngine;
using UnityEngine.UI;

public class PresentManager : MonoBehaviour
{

    public static PresentManager Instance;

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

    private void Start()
    {
       SetImageSlot();
    }


    public void SetImageSlot()
    {
        PlayerProgress progress = GameProgressManager.Instance.GetProgress();
        if (progress != null)
        {
            ImageSlot slot=slotStorage.GetImageSlot(progress.chapter, progress.stage); //이미지 슬롯에서 이미지 가져와서 이미지 바꾸기

            targetImage.sprite = slot.sprite; //이미지 셋

            DialogueManager.Instance.SetDialogueSlot(slot.dialogueSlot); // 대화 셋
            
            //slot.onStart?.Invoke();//시작시 원하는 이벤트 있을시 실행

        }
    }

    
}
