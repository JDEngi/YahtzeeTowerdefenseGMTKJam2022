
using Unity.VisualScripting;
using UnityEngine;

public class AbstractTower : MonoBehaviour
{
    public string NameOfEntityContainer;
    public float Range;
    public float Cooldown;
    public float Damage;

    private float currentCooldown;
    private GameObject EntityContainer;

    // Start is called before the first frame update
    void Start()
    {
        EntityContainer = GameObject.Find(NameOfEntityContainer);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            AbstractEnemy enemy = SearchEnemyToShoot();
            if (enemy == null) return;

            enemy.applyDamage(Damage);
            currentCooldown = Cooldown;
        }
    }

    private AbstractEnemy SearchEnemyToShoot()
    {
        AbstractEnemy[] allEnemies = EntityContainer.GetComponentsInChildren<AbstractEnemy>();
        foreach (AbstractEnemy enemy in allEnemies)
        {
            float distance = UnityEngine.Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < Range) return enemy;
        }

        return null;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
