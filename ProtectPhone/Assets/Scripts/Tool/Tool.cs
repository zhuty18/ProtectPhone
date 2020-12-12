using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public int id;
    public Rigidbody2D body;
    public Collider2D collider;
    void Start() 
    {
        Sprite spriteB = Resources.Load<Sprite> ("tool-"+id);
        gameObject.GetComponent<SpriteRenderer> ().sprite = spriteB;
    }

    void Update() 
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Player player=other.gameObject.GetComponent<Player>();
        if(player!=null)
        {
            player.addTool(id);
            Destroy(this.gameObject);
        }
    }
    
    public void Use(GameObject bullet,GameCharacter character,int dir)
    {
        switch(id)
        {
            case 1:
                Debug.Log(bullet);
                GameObject f = Instantiate(bullet);
                DamageCarrier d=f.GetComponent<DamageCarrier>();
                d.SetPosition(character.transform.position,dir);
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
