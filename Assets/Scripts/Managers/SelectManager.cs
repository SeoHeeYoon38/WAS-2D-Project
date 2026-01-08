using UnityEngine;
using TMPro;
public class SelectManager : MonoBehaviour
{
    [Header("선택지 패널")]
    [SerializeField] private GameObject selectPanel;


    [Header("Dropdown 3개")]
    [SerializeField] private TMP_Dropdown dropdown1;
    [SerializeField] private TMP_Dropdown dropdown2;
    [SerializeField] private TMP_Dropdown dropdown3;

    private void Awake()
    {
        if (selectPanel != null)
            selectPanel.SetActive(false); // 시작 시 꺼두기
    }

    public void ShowSelectPanel()
    {
        if (selectPanel != null)
            selectPanel.SetActive(true);
    }

    public void HideSelectPanel()
    {
        if (selectPanel != null)
            selectPanel.SetActive(false);
    }

    public void Evaluate()
    {
    
        int v1 = dropdown1.value;
        int v2 = dropdown2.value;
        int v3 = dropdown3.value;

     

     
        ResultDefault(v1, v2, v3);
        
    }

    private void ResultDefault(int v1, int v2, int v3)
    {
        Debug.Log($"기본 결과: ({v1}, {v2}, {v3})");
    }

}
