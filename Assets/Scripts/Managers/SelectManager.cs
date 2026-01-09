using UnityEngine;
using TMPro;
public class SelectManager : MonoBehaviour
{
    [Header("������ �г�")]
    [SerializeField] private GameObject selectPanel;


    [Header("Dropdown 3��")]
    [SerializeField] private TMP_Dropdown dropdown1;
    [SerializeField] private TMP_Dropdown dropdown2;
    [SerializeField] private TMP_Dropdown dropdown3;

    private void Awake()
    {
        if (selectPanel != null)
            selectPanel.SetActive(false); // ���� �� ���α�
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

     

     
        PresentManager.Instance.SwitchSelectImage(v1, v2, v3);
        Debug.Log($"�⺻ ���: ({v1}, {v2}, {v3})");
    }

  

}
