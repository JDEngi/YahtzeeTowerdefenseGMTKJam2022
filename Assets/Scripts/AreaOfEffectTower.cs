using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaOfEffectTower : AbstractTower
{
    [Header("Unity")]
    public GameObject EffectGameObject;

    [Header("Attributes")]
    public float FireRate = 1f;
    private float FireCountdown = 0f;

    public float FadeOutTime = 1f;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        if (FireCountdown > 0)
        {
            FireCountdown -= Time.deltaTime;
        }
        else
        {

            ICollection<AbstractEnemy> targets = FindAllEnemiesInRange().AsReadOnlyCollection();

            // if we have no new target, do nothing
            if (targets.Count == 0)
            {
                FireCountdown = 0;
                return;
            }

            ShowDamageArea();
            foreach (AbstractEnemy enemy in targets)
            {
                enemy.ApplyDamage(Damage);
            }

            FireCountdown = 1f / FireRate;
        }
    }

    private void ShowDamageArea()
    {
        GameObject instance = Instantiate(EffectGameObject, transform);
        instance.transform.localScale = new Vector3(Range, Range, Range);
        StartCoroutine(FadeOutDamageArea(instance));
    }

    IEnumerator FadeOutDamageArea(GameObject gameObject)
    {
        int fadeSteps = 100;
        for (int i = 0; i < fadeSteps; i++)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, i / fadeSteps);
        }
        yield return new WaitForSeconds(FadeOutTime / fadeSteps);
    }

    private IEnumerable<AbstractEnemy> FindAllEnemiesInRange()
    {
        AbstractEnemy[] allEnemies = entityContainer.GetComponentsInChildren<AbstractEnemy>();
        foreach (AbstractEnemy enemy in allEnemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < Range) yield return enemy;
        }
    }
}
