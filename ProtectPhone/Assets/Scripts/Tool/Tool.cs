using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public int id;
    
    public virtual void Use(GameCharacter character){}
}
