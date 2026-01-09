using UnityEditor.SceneManagement;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    [SerializeField]private PlayerProgress progress = new PlayerProgress();

    private const string SAVE_KEY = "PLAYER_PROGRESS";

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

    // ���൵ ����
    public void SaveProgress(int chapter, int stage)
    {
        progress.chapter = chapter;
        progress.stage = stage;

        string json = JsonUtility.ToJson(progress);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    // ���൵ �ҷ�����
    public void LoadProgress()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            progress = JsonUtility.FromJson<PlayerProgress>(json);
        }
        else
        {
            progress.chapter = 1;
            progress.stage = 1;
        }
    }

    public void UpProgress()
    {
        int chapter= progress.chapter;
        int stage= progress.stage;

        stage++;
        if (stage >= 8)
        {
            stage = 1;
            chapter++;
        }


        SaveProgress(chapter,stage);
    }

    public void InitializeProgress()
    {
        int chapter= 1;
        int stage = 1;
        SaveProgress(chapter, stage);
        LoadProgress();
        
    }
    
    public PlayerProgress GetProgress()
    {
        return progress;
    }
}