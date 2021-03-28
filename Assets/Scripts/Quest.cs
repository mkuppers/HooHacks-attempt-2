using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public enum EncounterType { deliver, combat, dialogue };

    public string title;
    public float distance;
    public int exp;
    public string description;
    public EncounterType encounter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
