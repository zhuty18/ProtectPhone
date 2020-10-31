using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    public string name;
    public int hp;
    public int maxHp;
    public float moveSpeed;

    private List<Effect> effects;
    private List<Tool> backpack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GetDir() {}
    public void GetPos() {}

    public void AddEffect(Effect effect) {}

    public void IncreaseHp(int amount) {}

    public void IncreaseMaxHp(int amount) {}

    public void IncreaseMoveSpeed(float amount) {}

    public void IncreaseEnergy(int amount) {}

    public void GainReward(Pack pack) {}

    public void BeDamaged(DamageCarrier damageCarrier) {}

    public void AfterAttack(GameCharacter damaged) {}

    private void UseTool(Tool tool) {}

    private void IsDead() {}

    private void IsOnGround() {}    
}
