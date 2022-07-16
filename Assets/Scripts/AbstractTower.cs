
using System;
using Unity.VisualScripting;
using UnityEngine;

public class AbstractTower : MonoBehaviour
{
    public string NameOfEntityContainer;
    public float Range;
    public float Cooldown;
    public float Damage;

    private float currentCooldown;
    private GameObject entityContainer;
    private AbstractEnemy targetEntity;

    // Start is called before the first frame update
    public void Start()
    {
        entityContainer = GameObject.Find(NameOfEntityContainer);
        if (!entityContainer) throw new Exception("Could not find object with name " + NameOfEntityContainer);
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            // if we have a target, check whether it is valid
            if (targetEntity != null && !targetEntity.IsDestroyed())
            {
                float currentDistance = Vector3.Distance(targetEntity.transform.position, transform.position);
                if (currentDistance > Range)
                {
                    targetEntity = null;
                }
            }

            // if we have no valid target, search a new one
            if (targetEntity == null)
            {
                targetEntity = SearchEnemyToShoot();
            }

            // if we have no new target, do nothing
            if (targetEntity == null)
            {
                currentCooldown = 0;
                return;
            }

            Debug.Log("Shoot!");

            targetEntity.ApplyDamage(Damage);
            currentCooldown += Cooldown;
        }
    }

    private AbstractEnemy SearchEnemyToShoot()
    {
        AbstractEnemy[] allEnemies = entityContainer.GetComponentsInChildren<AbstractEnemy>();
        foreach (AbstractEnemy enemy in allEnemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < Range) return enemy;
        }

        return null;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
