using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameCharacter
{
    public int jumpTime;
    public GameObject bullet;
    public Tool tool;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        moveSpeed = 10;
        jumpForce = 8;
        jumpTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float hztMove = Input.GetAxis("Horizontal");
        float dir=Input.GetAxisRaw("Horizontal");
        if(dir<0){
            direction=-1;
        }
        else if(dir>0){
            direction=1;
        }

        if(body.velocity.y<=0&&IsGrounded())
        {
            jumpTime=0;
        }
        //deltatime for 不同电脑兼容 

        //Move
        if(hztMove != 0 ) 
        {
            //body.velocity.x = hztMove*moveSpeed;
            //body.AddForce(new Vector2(hztMove*moveSpeed,body.velocity.y));
            body.velocity = new Vector2(hztMove*moveSpeed,body.velocity.y);
        }

        if(Input.GetButtonDown("Jump")&&jumpTime<2)
        {
            jumpTime++;
            //body.AddForce(new Vector2(0, jumpForce));
            body.velocity = new Vector2(body.velocity.x,jumpForce);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            Fire();
        }
    }

    public bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

    public void Fire()
    {
        // GameObject f = Instantiate(bullet);
        // DamageCarrier d=f.GetComponent<DamageCarrier>();
        // d.SetPosition(transform.position,direction);
        // Tool t=new Tool();
        // t.id=1;
        if(this.backpack.tools[0]>0&&this.backpack.tools[1]>0)
        {
            tool.Use(this);
            backpack.tools[1]--;
        }
        else
        {
            Debug.Log("You don't have weapon!");
        }
    }
    public override void BeDamaged(DamageCarrier damageCarrier) 
    {
        Debug.Log("player is damaged");
        BeDamagedInt(50);
    }

    public void BeDamagedInt(int amount) {
        Debug.Log("player is damaged");
        if (hp <= (int) Mathf.RoundToInt(amount)) {
            hp = 0;
            Die();
        } else {
            hp -= (int) Mathf.RoundToInt(amount);
        }
    }

    public void Die() {
        Debug.Log("player dies");
        direction = 0;
        Destroy(gameObject);
    }
}
