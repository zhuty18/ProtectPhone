using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackView : MonoBehaviour 
{
    public Player player;
    public ToolImage[] tools; 

    void Start()
    {
        tools=new ToolImage[7];
        for(int i=0;i<7;i++)
        {
            tools[i]=GameObject.Find("BackpackView/Image/Tool"+(i+1)).GetComponent<ToolImage>();
        }
    }
    void Update()
    {
        for(int i=0;i<7;i++)
        {
            tools[i].number.text=""+player.backpack.tools[i];
        }
    }
}
