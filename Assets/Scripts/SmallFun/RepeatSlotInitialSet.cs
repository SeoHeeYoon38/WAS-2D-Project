using UnityEngine;
using UnityEngine.UI; 

public class RepeatSlotInitialSet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private Image image;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateImage(Sprite input)
    {
        if (image == null) return;
        image.sprite = input;
    }
}
