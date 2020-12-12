using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public int id;
    
    public virtual void Use(GameCharacter character){}
    static public string GetName(int toolId)
    {
        string res="";
        switch(toolId)
        {
            case 1:
                res="手枪";
                break;
            case 2:
                res="子弹";
                break;
            case 3:
                res="食物";
                break;
            case 4:
                res="绷带";
                break;
            case 5:
                res="手机";
                break;
            case 6:
                res="宝石";
                break;
            case 7:
                res="假GPS";
                break;
        }
        return res;
    }
}
