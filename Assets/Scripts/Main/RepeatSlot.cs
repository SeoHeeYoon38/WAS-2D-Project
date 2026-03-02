using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/Repeat Slot", fileName = "RepeatSlot")]
public class RepeatSlot: ScriptableObject
{
    [SerializeField] private int stage;
    [SerializeField] private int chapter;
    [SerializeField] private string key;
    [SerializeField] private Vector2 position; 
    [SerializeField] private Vector2 size = new Vector2(1, 1);
 
    public int Stage => stage;
    public int Chapter => chapter;
    public string Key => key;
    public Vector2 Position => position;
    public Vector2 Size => size;

    
    
    public bool IsMatch(Vector2 dropPos, string dropKey, float snapDist)
    {
        Debug.Log(key+"비교대상은"+dropKey+"거리는"+Vector2.Distance(dropPos, position));  
        if (string.Equals(key, dropKey))
        {
            Debug.Log("key는 동일");
            
            return Vector2.Distance(dropPos, position) <= snapDist;
        }
        
        
        return false;
            
    }
    
}
