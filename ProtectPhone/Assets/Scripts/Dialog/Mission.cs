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
    
    void Start()
    {
        want=6;
        des.text=target.name+"想要一个"+Tool.GetName(want)+"。";
        phone=this.transform.GetComponent<CanvasGroup>();
        phone.alpha=0;
        reward=Instantiate(myPack).GetComponent<Pack>();
        reward.addTool(3);
    }
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Player player=GameObject.Find("Player").GetComponent<Player>();
            if(player.backpack.tools[4]>0)
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
    }
    public void Finish()
    {
        des.text="任务已完成";
    }
}
