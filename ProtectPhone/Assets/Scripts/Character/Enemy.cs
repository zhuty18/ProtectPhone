using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AstarPathfindingProject;

public class Enemy : GameCharacter
{
    static int maxId = 0;
    int id;

    public Player target;
    public GameObject resource;
    public PathFinderPlatformer pathFinder;

    public float height;
    public float width;

    public float gravity = -12f;
    private Vector2 velocity = Vector2.zero;
    private float lastAttack;

    public float maxDistToTarget = 60f;

    private BoxCollider2D cld;
    private Rigidbody2D rb;

    EnemySpawner enemySpawner;

    Vector2 jumpDst;
    Vector2 jumpSrc;
    bool isJumping;
    float jumpStartTime;
    float jumpVx = 15.0f;
    float jumpVy;
    float jumpT;

    // Start is called before the first frame update
    void Start()
    {
        id = ++maxId;
        Debug.Log($"enemy {id} spawns");
        // velocity = Vector2.zero;
        // moveSpeed = 10;
        height = 1.0f;
        width = 1.0f;
        // hp = 100;
        isJumping = false;

        // weapon = new Tool();
        // weapon.id=1;
        lastAttack=Time.time;

        cld = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.gravityScale = 1f;

    }

    // Update is called once per frame
    void Update(){
        HandleSelfDestruction();
        // if (target == null) {
        //     // player is dead
        //     if (isJumping) StopJumpThrough();
        //     BeIdle();
        //     return;
        // }
        // if (!isJumping) { 
        //     if (pathFinder != null) {
        //         MoveTowardsTarget();
        //     }
        if(InRange() && (Time.time - lastAttack>1.0f)){
            lastAttack = Time.time;
            Debug.Log($"enemy {id} attacks");
            Attack();
        }
        // }
        // UpdatePos();
    }

    public void SetEnemySpawner(EnemySpawner es) {
        enemySpawner = es;
    }

    bool InRange()
    {
        if (target == null) return false;
        double dx=target.transform.position.x-this.transform.position.x;
        double dy=target.transform.position.y-this.transform.position.y;
        double dis=System.Math.Sqrt((dx*dx)+(dy*dy));
        if(dis <= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MoveTowardsTarget() {
        // the following function moves this Enemy object
        // it might call JumpThrough, MoveLeft, MoveRight
        pathFinder.GetOptimalPath(this, target.transform);
    }

    void HandleSelfDestruction() {
        if (transform.position.x > 100f || transform.position.y > 100f) {
            isJumping = false;
            direction = 0;
            Debug.Log($"destroys enemy {id}");
            enemySpawner.OnEnemyDestroy(this);
            Destroy(transform.parent.gameObject);
            return;
        }
        if (target == null) return;
        if (DistToTarget() > maxDistToTarget) {
            Debug.Log($"destroys enemy {id}");
            enemySpawner.OnEnemyDestroy(this);
            Destroy(transform.parent.gameObject);
            return;
        }
    }

    float DistToTarget() {
        return DistTo(target.transform.position);
    }

    float DistTo(Vector2 target) {
        return Vector2.Distance(transform.position, target);
    }

    public void SetPathFinder(PathFinderPlatformer pathFinder) {
        this.pathFinder = pathFinder;
    }

    public void Die() {
        Debug.Log($"enemy {id} dies");
        if (enemySpawner != null) enemySpawner.OnEnemyDestroy(this);
        Tool res=Instantiate(resource,this.transform.position,this.transform.rotation).GetComponent<Tool>();
        res.amount=(int)Random.Range(1f, 10f);
        Destroy(gameObject);
    }

    public void GetDamage(float amount) {
        Debug.Log($"enemy {id} gets damage");
        if (hp <= (int) Mathf.RoundToInt(amount)) {
            hp = 0;
            Die();
        } else {
            hp -= (int) Mathf.RoundToInt(amount);
        }
    }
    
    public override void BeDamaged(DamageCarrier damageCarrier) 
    {
        // Debug.Log("attack enemy");
        GetDamage(50);
    }

    public void SetTarget(GameObject target) {
        // AIDestinationSetter dstSetter = GetComponent<AIDestinationSetter>();
        // dstSetter.target = target.transform;
        this.target = target.GetComponent<Player>();
    }

    public void Attack() {
        // if (weapon != null)
        //     weapon.Use(this);
        target.BeDamagedInt(atkDmg);
    }

    public void Jump() {
        if (IsOnGround()) {
            // velocity.y = jumpForce;
        }
    }

    public void MoveRight() {
        // velocity.x = moveSpeed;
        // if (direction != 1)
            // Debug.Log($"enemy {id} moves right");
        direction = 1;
    }

    public void MoveLeft() {
        // velocity.x = -moveSpeed;
        // if (direction != -1)
            // Debug.Log($"enemy {id} moves left");
        direction=-1;
    }

    public void BeIdle() {
        // velocity.x = 0;
        if (direction != 0)
            // Debug.Log($"enemy {id} becomes idle");
        direction = 0;
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

    public void StartJumpThrough(Vector2 target) {
        // Debug.Log($"enemy {id} jumps, target = ({target.x}, {target.y})");

        rb.isKinematic = true;
        cld.enabled = false;

        jumpDst = target;
        jumpSrc = transform.position;
        isJumping = true;
        jumpStartTime = Time.time;


        float x0 = transform.position.x;
        float y0 = transform.position.y;
        float t = (target.x - x0) / jumpVx;
        float vy = (target.y - y0 - 0.5f * gravity * t * t) / t;
        
        // Debug.Log($"pos0 = ({x0}, {y0}), target = ({target.x}, {target.y}), t0 = {jumpStartTime}, t = {t}, v = ({jumpVx}, {vy})");

        // velocity.y = vy;
        jumpVy = vy;
    }

    public void StopJumpThrough() {
        Debug.Log($"enemy {id} stops jumping");

        isJumping = false;
        // transform.position = jumpDst;
        // velocity.y = 0;
        rb.isKinematic = false;
        cld.enabled = true;
    }

    public void UpdatePos() {
        if (target == null) {
            return;
        }
        if (isJumping) {
            if (DistTo(jumpDst) < 0.1f) {
                StopJumpThrough();
            } else {
                float t = Time.time - jumpStartTime;
                float x = jumpVx * t;
                float y = jumpVy * t + 0.5f * gravity * t * t;
                transform.position = jumpSrc + new Vector2(x, y);
            }
        } else {
            body.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        }
    }
}
