using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    private List<Tool> tools;
    
    public Pack()
    {
        Tools = new List<Tool>();
    }
    public void addTool(int index, int n)
    {
        Tools.Add(new Tool(index, n));
    }
    public void addTool(Tool res)
    {
        Tools.Add(res);
    }
    public List<Tool> getTools()
    {
        return Tools;
    }
    public void deBug()
    {
        System.Diagnostics.Debug.WriteLine("Pack: ");
        foreach (Tool res in Tools)
        {
            res.deBug();
        }
    }
}
