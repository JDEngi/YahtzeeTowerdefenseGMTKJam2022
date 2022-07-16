using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractEnemy : MonoBehaviour
{
    public float speed = 10f;

    public float startHealth = 100;
    private float health;

    public int value = 25; // Reward for killing enemy

    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;

    public void Start()
    {
        target = Waypoints.points[0];

        health = startHealth;
    }

    public void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length -1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        // Todo: Add reward for killing enemy
        Destroy (gameObject);
    }
}
