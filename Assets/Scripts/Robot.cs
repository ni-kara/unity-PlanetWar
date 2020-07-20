using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private int maxLife = 3;
    private int currentLife;
    private int attackDamage = 1;
    private float speedAttackTime = 1f;
    private void Awake() => this.currentLife = this.maxLife;  
    public int MaxLife() => this.maxLife;
    public int CurrentLife() => this.currentLife;
    public float SpeedAttackTime() => this.speedAttackTime;
    public void Damage() => this.currentLife -= this.attackDamage;    
}
