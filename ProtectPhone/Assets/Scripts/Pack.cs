using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    private List<Tool> tools;
    
    public Pack()
    {
        tools = new List<Tool>();
    }
    public void addTool(Tool res)
    {
        tools.Add(res);
    }
    public List<Tool> getTools()
    {
        return tools;
    }
    public bool SubmitTool(int toolId)
    {
        foreach(Tool t in this.tools)
        {
            if(t.id==toolId)
            {
                tools.Remove(t);
                return true;
            }
        }
        return false;
    }
}
