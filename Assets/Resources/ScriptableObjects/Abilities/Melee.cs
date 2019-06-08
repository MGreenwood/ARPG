﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class Melee : Ability
{
    int precision = 10; // how many raycasts to shoot

    [SerializeField]
    float _angle; // the attack angle
    [SerializeField]
    Effect.EffectType _effectType;
    [SerializeField]
    GameObject _particleSystem;

    public override bool Cast()
    {
        precision = (int)_angle / 5;

        Instantiate(_particleSystem, owner.transform.position, owner.transform.rotation);

        int target = owner.layer == LayerMask.NameToLayer("Player") ? LayerMask.NameToLayer("Enemy") : LayerMask.NameToLayer("Player");

        List<GameObject> hits = new List<GameObject>();

        for (int i = -precision / 2; i < precision / 2; i++)
        {
            float angle = i * (_angle * Mathf.Deg2Rad) / precision;
            Vector3 direction = owner.transform.TransformDirection(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)));

            if(GameManager.instance.Debug) // draw the raycasts
                Debug.DrawLine(owner.transform.position, owner.transform.position + direction.normalized * range, Color.red, 1f);

            RaycastHit hit;
            if(Physics.Raycast(owner.transform.position, direction.normalized, out hit, range) && !hits.Contains(hit.collider.gameObject))
            {
                hits.Add(hit.collider.gameObject);
            }
        }

        if (hits.Count > 0)
        {
            foreach (GameObject ob in hits)
            {
                if (ob.layer == target)
                {
                    bool isCrit = false; // TODO

                    ob.GetComponent<IDamageable>().Damage(damage, _effectType, isCrit);
                }
            }
        }

        return true;
    }
}