using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : GameCharacter
{
    public Tool tool;
    public int jumpTime;
    public int strength;
    public int maxStrength;
    public int maxSpeed;
    public int commonSpeed;
    public float healthRecoveryTimer;
    public Collider2D groundCollider;
    public GameObject bullet;
    public Animator anim;
    public Text HPShow;
    public EndView ev;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100;
        hp = maxHp;
        moveSpeed = 10;
        maxSpeed = 20;
        commonSpeed = 10;
        maxStrength = 200;
        strength = 0;
        healthRecoveryTimer = 0.5f;
        anim = this.GetComponent<Animator>();
        jumpForce = 8;
        jumpTime = 0;
        score=0;
        ev.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
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
        Vector3 oScale = transform.localScale;
        transform.localScale = new Vector3(direction * System.Math.Abs(oScale.x), oScale.y, oScale.z);

        //Move
        if(hztMove != 0 && (!collider.IsTouchingLayers(groundLayer) || IsGrounded())) 
        {
            //body.velocity.x = hztMove*moveSpeed;
            //body.AddForce(new Vector2(hztMove*moveSpeed,body.velocity.y));
            
            anim.SetFloat("speed", 1);
            body.velocity = new Vector2(hztMove*moveSpeed,body.velocity.y);
        }
        else{
            anim.SetFloat("speed", 0);
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
        // Strength
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(strength > 0){
                strength--;
                moveSpeed = maxSpeed;
            } else{
                moveSpeed = commonSpeed;
            }
        } else {
            moveSpeed = commonSpeed;
            if(strength < maxStrength){
                strength++;
            }
        }

        // HP
        healthRecoveryTimer -= Time.deltaTime;
        if (healthRecoveryTimer <= 0) {
            if(hp < maxHp)
            {
                hp += 1;
            }
            healthRecoveryTimer = 2.0f;
        }
        HPShow.text="HP: "+hp+" / "+maxHp;
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("UIi");
        }
    }

    public bool IsGrounded()
    {
        // Vector2 position = transform.position;
        // Vector2 direction = Vector2.down;
        // float distance = 1.0f;
        
        // RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        // if (hit.collider != null) {
        //     return true;
        // }
        // return false;
        return groundCollider.IsTouchingLayers(groundLayer);
    }

    public void Fire()
    {
        // GameObject f = Instantiate(bullet);
        // DamageCarrier d=f.GetComponent<DamageCarrier>();
        // d.SetPosition(transform.position,direction);
        // Tool t=new Tool();
        // t.id=1;
        anim.SetTrigger("attack");
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
        // Destroy(gameObject);
        ev.gameObject.SetActive(true);
        ev.info.text="游戏结束！你的得分为"+score+"！\n按R重新开始！";
    }
}
