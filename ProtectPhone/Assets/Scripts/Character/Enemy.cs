﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameCharacter, IDamagable
{
    public Transform target;
    public int hp = 100;
    public float moveSpeed = 2;  // distance per frame
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget() {
        Vector3 distToTarget = target.transform.position - transform.position;
        distToTarget.y = 0;
        Vector3 moveDir = distToTarget.normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
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
}
