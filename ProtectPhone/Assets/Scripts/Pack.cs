using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    public int[] tools;
    
    public Pack()
    {
        tools=new int[7];
        for(int i=0;i<7;i++)
        {
            tools[i]=0;
        }
    }
    public void addTool(int toolId,int amount=1)
    {
        tools[toolId-1]+=amount;
    }
    public void addPack(Pack a)
    {
        for(int i=0;i<7;i++)
        {
            this.tools[i]+=a.tools[i];
        }
    }
    public bool SubmitTool(int toolId)
    {
        if(tools[toolId-1]>0)
        {
            tools[toolId-1]--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
