using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private int maxHitPoints = 10;
    [SerializeField] private int regenAmount = 2;
    [SerializeField] private float regenInterval = 1.0f;

    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();

        if (health != null)
        {
            StartCoroutine(RegenerateHealth());
        }
        else
        {
            Debug.LogError("Regenerate script: Missing Heath component!");
        }
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenInterval);

            if (health == null || health.IsDestroyed()) break;

            int currentHP = health.GetHitPoints();

            if (currentHP < maxHitPoints)
            {
                int healAmount = Mathf.Min(regenAmount, maxHitPoints - currentHP);
                health.Heal(healAmount);
                Debug.Log($"Enemy regenerates to {health.GetHitPoints()} HP");
            }
        }
    }

}
