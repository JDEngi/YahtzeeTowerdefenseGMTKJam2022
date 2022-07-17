using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractEnemy : MonoBehaviour
{
    public float startSpeed = 10f;
    private float speed;

    public float startHealth = 100;
    private float health;

    public int killValue = 1;

    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;

    public void Start()
    {
        target = Waypoints.points[0];

        speed = startSpeed;
        health = startHealth;
    }

    public void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        float distance = Vector3.Distance(transform.position, target.position);

        // Update speed
        if (distance >= -1f && distance <= 1f)
        {
            speed = startSpeed / 2;
        }
        else
        {
            speed = startSpeed;
        }

        // Update waypoint
        if (distance <= 0.1f)
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
        GameManager.AddScore(killValue);

        Destroy (gameObject);
    }
}
