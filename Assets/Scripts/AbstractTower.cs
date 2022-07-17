using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractTower : MonoBehaviour
{
    protected GameObject entityContainer;

    [Header("Unity")] public string NameOfEntityContainer;

    [Header("Attributes")] public int BuildCost;
    public float Damage;
    public float Range;

    private MeshRenderer graphic;

    // Start is called before the first frame update
    public void Start()
    {
        entityContainer = GameObject.Find(NameOfEntityContainer);
        if (!entityContainer) throw new Exception("Could not find object with name " + NameOfEntityContainer);

        graphic = GetComponent<MeshRenderer>();
        if (!graphic) throw new Exception("Could not find SpriteRenderer");
    }

    protected abstract void Shoot();

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}

public abstract class SingleTargetTower : AbstractTower
{
    protected AbstractEnemy targetEntity;

    // Update is called once per frame
    public void Update()
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
}