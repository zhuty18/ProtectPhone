using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public int id;
    public int amount=0;
    public Rigidbody2D body;
    public Collider2D collider;
    void Start() 
    {
        Sprite spriteB = Resources.Load<Sprite> ("tool-"+id);
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
        if (sr != null)
            sr.sprite = spriteB;
    }

    void Update() 
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Player player=other.gameObject.GetComponent<Player>();
        if(player!=null)
        {
            if(id==2&&amount==0)
            {
                player.backpack.addTool(id,30);
            }
            else if(id==2){
                player.backpack.addTool(id,amount);
            }
            else{
            player.backpack.addTool(id);
            }
            Destroy(this.gameObject);
        }
    }
    
    public void Use(GameCharacter character)
    {
        switch(id)
        {
            case 1:
                GameObject bullet = GameObject.Find("Player").GetComponent<Player>().bullet;
                GameObject f = Instantiate(bullet);
                DamageCarrier d=f.GetComponent<DamageCarrier>();
                d.SetPosition(character.transform.position,character.direction);
                // Debug.Log(character.direction);
                break;
        }
    }
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
