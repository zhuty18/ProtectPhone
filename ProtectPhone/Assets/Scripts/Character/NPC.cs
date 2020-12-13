using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : GameCharacter
{
    public Mission myMission;
    public Dialog dia;
    public string hello;
    public string want;
    public Player player;

    void Start()
    {
        this.name="小明";
        this.hello="我是一个无害的NPC！";
    }

    void Update()
    {
        if (this.player==null)
        {
            player=GameObject.Find("Player").GetComponent<Player>();
        }
        if (this.dia==null)
        {
            dia=GameObject.Find("Dialog").GetComponent<Dialog>();
        }
        if(Input.GetKeyDown(KeyCode.I)){
            double dx=player.transform.position.x-this.transform.position.x;
            double dy=player.transform.position.y-this.transform.position.y;
            double dis=System.Math.Sqrt((dx*dx)+(dy*dy));
            if(this.dia.Visiable())
            {
                this.dia.Hide();
            }
            else if(dis<2)
            {
                this.BeInteracted();
            }
        }
    }

    public void BeInteracted() 
    {
        dia.intereacting=this;
        dia.name.text=this.name;
        this.want="我需要1个"+Tool.GetName(myMission.want)+"！";
        dia.content.text=this.hello+'\n'+this.want;
        dia.Show();
    }

    public void Check()
    {
        if(this.player.backpack.SubmitTool(myMission.want))
        {
            dia.content.text="谢谢你！";
            player.GainReward(myMission.reward);
            myMission.Finish();
            player.score+=100;
        }
        else
        {
            dia.content.text="你身上没有带我要的东西……";
        }
        this.dia.Bye();
    }

    public void Refuse()
    {
        dia.content.text="好吧，希望下次你能给我。";
        this.dia.Bye();
    }
}
