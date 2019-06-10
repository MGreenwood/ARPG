﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IKillable, IHasAttributes
{
    private float health, speed;
    public float MAX_HEALTH;
    public GameObject combatText;

    public event DamageTaken damageTaken;
    public event HealthChanged healthChanged;

    [SerializeField]
    Attributes _attributes;
    public EnemySpawner.EnemyTypes _enemyType;

    EnemyBehaviorManager _behaviorManager;

    private void Start()
    {
        health = MAX_HEALTH;
        _attributes = Instantiate(_attributes);
        _behaviorManager = GetComponent<EnemyBehaviorManager>();
    }

    public float Health
    {
        get { return health; }
    }

    public Enemy(float health_, float speed_, float MAX_HEALTH_)
    {
        health = health_;
        speed = speed_;
        MAX_HEALTH = MAX_HEALTH_;
    }

    public void Damage(float dmg, Effect.EffectType effectType, bool crit, GameObject abilityOwner)
    {
        health -= dmg;                       // remove damage from health
        health = health < 0 ? 0f : health;   // do not allow below 0

        if (health <= 0)
            Kill();

        damageTaken?.Invoke(dmg, effectType, crit); // inform subscribers that HP has changed
        healthChanged?.Invoke();
        _behaviorManager.Damaged(abilityOwner, dmg);
    }

    public void Kill()
    {
        // drop any items

        // activate death animation in subroutine


        // remove this and place in death subroutine TODO
        Destroy(gameObject);
    }

    public Attributes GetAttributes() => _attributes;
}
