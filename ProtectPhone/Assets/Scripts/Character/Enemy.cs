using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameCharacter
{
    public GameObject target;
    public Weapon weapon;
    public PathFinderPlatformer pathFinder;

    public float height;
    public float width;

    public float gravity = -9.81f;
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero;
        height = 1.0f;
        width = 1.0f;
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    void FixedUpdate() {
        if (!IsOnGround()) {
            velocity.y += gravity * Time.deltaTime;
        } else {
            if (velocity.y < 0) {
                velocity.y = 0;
            }
        }

        if (velocity.x < 0) {
            if (IsOnLeftWall()) {
                velocity.x = 0;
            }
        } else {
            if (IsOnRightWall()) {
                velocity.x = 0;
            }
        }

        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
    }

    void MoveTowardsTarget() {
        pathFinder.GetOptimalPath(this, target.transform);
        // Vector3 distToTarget = target.transform.position - transform.position;
        // if (distToTarget.x >= 0) {
        //     MoveRight();
        // } else {
        //     MoveLeft();
        //     // Jump();
        // }
    }

    public void SetPathFinder(PathFinderPlatformer pathFinder) {
        this.pathFinder = pathFinder;
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void GetDamage(float amount) {
        if (hp <= (int) Mathf.RoundToInt(amount)) {
            hp = 0;
            Die();
        } else {
            hp -= (int) Mathf.RoundToInt(amount);
        }
    }
    
    public new void BeDamaged(DamageCarrier damageCarrier) 
    {
        GetDamage(50);
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }

    public void Attack() {
        weapon.Use(this);
    }

    public void Jump() {
        if (IsOnGround()) {
            velocity.y = jumpForce;
        }
    }

    public void MoveRight() {
        velocity.x = moveSpeed;
    }

    public void MoveLeft() {
        velocity.x = -moveSpeed;
    }

    public void BeIdle() {
        velocity.x = 0;
    }

    public bool IsOnGround() {
        Vector2 position = transform.position;
        float distance = ((float) height) / 2.0f + 0.02f;
        
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

    public bool IsOnLeftWall() {
        Vector2 position = transform.position;
        float distance = ((float) width) / 2.0f + 0.02f;
        Vector2 size = new Vector2(width, height);
        
        RaycastHit2D hit = Physics2D.BoxCast(position, size, 0, Vector2.left, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

        public bool IsOnRightWall() {
        Vector2 position = transform.position;
        float distance = ((float) width) / 2.0f + 0.02f;
        Vector2 size = new Vector2(width, height);
        
        RaycastHit2D hit = Physics2D.BoxCast(position, size, 0, Vector2.right, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }
}
