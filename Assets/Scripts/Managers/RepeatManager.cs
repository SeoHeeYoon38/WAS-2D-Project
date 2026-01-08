using UnityEngine;

public class RepeatManager : MonoBehaviour 
{
    [Header("반복 패널")]
    [SerializeField] private GameObject repeatPanel;

    private void Awake()
    {
        if (repeatPanel != null)
            repeatPanel.SetActive(false); // 시작 시 꺼두기
    }

    public void ShowPanel()
    {
        if (repeatPanel != null)
            repeatPanel.SetActive(true);
    }

    public void HidePanel()
    {
        if (repeatPanel != null)
            repeatPanel.SetActive(false);
    }

}
