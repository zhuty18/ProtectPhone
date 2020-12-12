using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    public Text des;
    public Text title;
    public NPC target;
    public CanvasGroup phone;
    public int want;
    public Pack reward;
    public GameObject myPack;
    public GameObject myTool;
    
    void Start()
    {
        des.text=target.name+"想要一个"+Tool.GetName(want);
        phone=this.transform.GetComponent<CanvasGroup>();
        want=6;
        phone.alpha=0;
        reward=Instantiate(myPack).GetComponent<Pack>();
        Tool gift=Instantiate(myTool).GetComponent<Tool>();
        gift.id=3;
        reward.addTool(gift);
    }
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(phone.alpha==0)
            {
                phone.alpha=1;
            }
            else
            {
                phone.alpha=0;
            }
        }
    }
    public void Finish()
    {
        des.text="任务已完成";
    }
}
