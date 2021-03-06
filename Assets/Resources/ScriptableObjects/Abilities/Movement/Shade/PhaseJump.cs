﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseJump : MonoBehaviour
{
    [SerializeField]
    LayerMask BlockingMask;
    [SerializeField]
    float invulTime;
    float distance = 20f;
    float speed = 1.3f;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        Vector3 startPos = player.position;
        Vector3 goal = player.position + player.forward * distance;

        player.GetComponent<Player>().ActivateInvulnerability(invulTime);

        RaycastHit hit;

        if (Physics.Raycast(startPos, goal - startPos, out hit, distance, BlockingMask))
        {
            float collisionDistance = hit.distance;
            Vector3 inverseDir = (player.position - hit.point).normalized;
            goal = hit.point + inverseDir * 1f; // move away from impact point and go there instead
        }

        while (Vector3.Distance(player.position, goal) > 0.5f)
        {
            player.position = Vector3.MoveTowards(player.position, goal, speed);

            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}
