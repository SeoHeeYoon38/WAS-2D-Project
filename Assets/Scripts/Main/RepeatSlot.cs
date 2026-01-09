using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/Repeat Slot", fileName = "RepeatSlot")]
public class RepeatSlot: ScriptableObject
{
    [SerializeField] private int stage;
    [SerializeField] private int chapter;
    [SerializeField] private string key;
    [SerializeField] private Vector2 position; 
    [SerializeField] private Vector2 size = new Vector2(100, 100);
 
    public int Stage => stage;
    public int Chapter => chapter;
    public string Key => key;
    public Vector2 Position => position;
    public Vector2 Size => size;

    
    
    public bool IsMatch(Vector2 dropPos, string dropKey, float snapDist)
    {
        if (!string.Equals(key, dropKey, StringComparison.Ordinal)) return false;
        return Vector2.Distance(dropPos, position) <= snapDist;
    }
    
}
