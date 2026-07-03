using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class PresentManager : MonoBehaviour
{
    public static PresentManager Instance;

    [SerializeField] private SelectManager selectManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private RepeatManager repeatManager;
    [SerializeField] private RepeatApplyManager repeatApplyManager;
    [SerializeField] private AnimationManager AnimationManager;

    [SerializeField] private FadeManager fadeManager;
    [SerializeField] private VibeManager vibeManager;

    public SelectManager SelectManager => selectManager;
    public DialogueManager DialogueManager => dialogueManager;
    public RepeatManager RepeatManager => repeatManager;
    public RepeatApplyManager RepeatApplyManager => repeatApplyManager;

    [Header("기존 변수")]
    [SerializeField] private Image targetImage;
    [SerializeField] private PresentationCardStorage presentationCardStorage;
    [SerializeField] private PresentationCard currentPresentationCard;

    [Header("새로 추가한 저장 UI")]
    [SerializeField] private GameObject saveCompleteUI;

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

        // 테스트용: 숫자 9번 누르면 강제 저장 실행
        if (Keyboard.current.digit9Key.wasPressedThisFrame) HandleDebugInput(9);
    }

    private void HandleDebugInput(int key)
    {
        switch (key)
        {
            case 1: StartPresentation(); break;
            case 2: break;
            case 3: selectManager.ShowSelectPanel(); break;
            case 4: repeatManager.ShowPanel(); break;
            case 5: break;

            case 9:
                Debug.Log("테스트용 저장 루틴을 강제로 시작합니다.");
                PlayAnimationPart();
                break;
        }
    }

    private void Start()
    {
        selectManager = GetComponentInChildren<SelectManager>();
        dialogueManager = GetComponentInChildren<DialogueManager>();
        repeatManager = GetComponentInChildren<RepeatManager>();
        repeatApplyManager = GetComponentInChildren<RepeatApplyManager>();
        fadeManager = GetComponentInChildren<FadeManager>();
        vibeManager = GetComponentInChildren<VibeManager>();
        AnimationManager = GetComponentInChildren<AnimationManager>();
        StartPresentation();
    }

    public void StartPresentation()
    {
        PlayerProgress progress = GameProgressManager.Instance.GetProgress();
        if (progress != null)
        {
            currentPresentationCard = presentationCardStorage.GetPresentationCard(progress.chapter, progress.stage);
        }
        PresentImageSlot();
    }

    public void PresentNextImageSlot()
    {
        ImageSlot imageSlot = currentPresentationCard.GetCurrentImageSlot();
        if (imageSlot.endFade) fadeManager.FadeIn();
        if (imageSlot.endVibe) vibeManager.Vibe();

        currentPresentationCard.PlusImageSlotIdx();

        if (currentPresentationCard.CheckIsImageSlotRemain()) PresentImageSlot();
        else PresentSelectImage();
    }

    private void PresentImageSlot()
    {
        ImageSlot imageSlot = currentPresentationCard.GetCurrentImageSlot();
        if (imageSlot.startFade) fadeManager.FadeOut();
        if (imageSlot.startVibe) vibeManager.Vibe();

        targetImage.sprite = imageSlot.sprite;
        dialogueManager.SetDialogueSlot(imageSlot.dialogueSlot);
    }

    private void PresentSelectImage()
    {
        selectManager.ShowSelectPanel();
    }

    public void SwitchSelectImage(int v1, int v2, int v3)
    {
        if (currentPresentationCard.CheckIsAllSelectComplete(v1, v2, v3))
        {
            selectManager.HideSelectPanel();
            repeatManager.ShowPanel();
            repeatApplyManager.Initialize(currentPresentationCard);
        }
    }

    public void ApplyRepeat()
    {
        currentPresentationCard.PlusRepeatIdx();

        if (currentPresentationCard.CheckIsAllRepeatApplied())
        {
            repeatManager.HidePanel();
            AnimationManager.ShowAnimationPanel();
            AnimationManager.PlayAnimation(currentPresentationCard.AnimationCard);

            PlayAnimationPart();
        }
    }

    public void PlayAnimationPart()
    {
        StartCoroutine(StageClearAndSaveRoutine());
    }

    // 저장 루틴
    private IEnumerator StageClearAndSaveRoutine()
    {
        // 애니메이션 끝나고 넘어올 때 약간 대기 (테스트용 0.5초)
        yield return new WaitForSeconds(0.5f);
        if (saveCompleteUI != null)
        {
            TextMeshProUGUI tmpText = saveCompleteUI.GetComponentInChildren<TextMeshProUGUI>();

            if (tmpText != null)
            {
                saveCompleteUI.SetActive(true);
                tmpText.text = "";

                // 0.3초 대기
                yield return new WaitForSeconds(0.3f);

                string typingStr = "자동 저장 중... ";
                for (int i = 0; i < typingStr.Length; i++)
                {
                    tmpText.text += typingStr[i];
                    yield return new WaitForSeconds(0.25f); // 타자 치는 속도
                }

                // 타이핑 다 치고 실제 백엔드 데이터 저장
                //GameProgressManager.Instance.UpProgress();

                // 자동 저장 중...
                yield return new WaitForSeconds(2.0f);

                // 별로임.
                // "저장 완료"로  바꾸기
                //tmpText.text = "저장 완료!  ";

                // "저장 완료" 글자를 1.5초 동안 보여주기
                //yield return new WaitForSeconds(1.8f);
            }
            saveCompleteUI.SetActive(false);
        }
        else
        {
            GameProgressManager.Instance.UpProgress();
        }
        SceneManager.LoadScene("MainScene");
    }
}