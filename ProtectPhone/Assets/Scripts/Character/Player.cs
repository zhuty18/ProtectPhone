using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameCharacter
{
    public int jumpTime;
    public int strength;
    public int maxStrength;
    public int maxSpeed;
    public int commonSpeed;
    public float healthRecoveryTimer;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        hp = 50;
        maxHp = 100;
        moveSpeed = 10;
        maxSpeed = 20;
        commonSpeed = 10;
        maxStrength = 200;
        strength = 0;
        healthRecoveryTimer = 0.5f;
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
        
        Vector3 oScale = transform.localScale;
        transform.localScale = new Vector3(direction * System.Math.Abs(oScale.x), oScale.y, oScale.z);

        Debug.Log(IsGrounded());
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
    }

    public bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.5f;
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        Debug.Log(hit.collider);
        if (hit.collider != null) {
            return true;
        }

        return false;
    }

    public void Fire()
    {
        GameObject f = Instantiate(bullet);
        DamageCarrier d=f.GetComponent<DamageCarrier>();
        d.SetPosition(transform.position,direction);
    }
}
