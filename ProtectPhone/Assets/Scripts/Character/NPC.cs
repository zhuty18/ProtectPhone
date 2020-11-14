using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : GameCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeInteracted(GameCharacter interacter) {}

    public bool ReceiveTool(Tool tool, int count)
    {
        return false;
    }
}
