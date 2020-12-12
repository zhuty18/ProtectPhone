using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCarrier : MonoBehaviour
{
    public Rigidbody2D body;
    public Collider2D collider;
    public int speed=30;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameCharacter attack=other.gameObject.GetComponent<GameCharacter>();
        if(attack!=null)
        {
            Debug.Log(attack);
            attack.BeDamaged(this);
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void SetPosition(Vector2 pos,int dir)
    {
        transform.position=pos+new Vector2(dir,0);
        body.velocity=new Vector2(dir*speed,0);
    }

}