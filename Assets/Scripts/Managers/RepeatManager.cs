using UnityEngine;

public class RepeatManager : MonoBehaviour 
{
    [Header("�ݺ� �г�")]
    [SerializeField] private GameObject repeatPanel;

    private void Awake()
    {
        if (repeatPanel != null)
            repeatPanel.SetActive(false); // ���� �� ���α�
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
