using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    [SerializeField] private GameObject AnimationPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowAnimationPanel()
    {
        if (AnimationPanel != null)
            AnimationPanel.SetActive(true);
    }

    public void HideAnimationPanel()
    {
        if (AnimationPanel != null)
            AnimationPanel.SetActive(false);
    }
    
}
