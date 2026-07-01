using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private TextMeshProUGUI buttonText;

    // 배경 색상
    private Color defaultBgColor = new Color(0, 0, 0, 0); 
    private Color hoverBgColor = new Color(0.7f, 0.7f, 0.7f, 1f);

    // 글자 색상
    private Color defaultTextColor = Color.white; 
    private Color hoverTextColor = Color.black;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        buttonImage.color = defaultBgColor;
        buttonText.color = defaultTextColor;
        buttonImage.fillCenter = false; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverBgColor;
        buttonText.color = hoverTextColor;

        buttonImage.fillCenter = true; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = defaultBgColor; 
        buttonText.color = defaultTextColor; 

        buttonImage.fillCenter = false; 
    }
}