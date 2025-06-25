using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretSlomo : MonoBehaviour {

	[Header("References")]
	[SerializeField] private LayerMask enemyMask;

	[Header("Attribute")]
	[SerializeField] private float targetingRange = 5f;
	[SerializeField] private float aps = 0.25f;
	[SerializeField] private float slowTime = 0.1f;
	[SerializeField] private float slowFactor = 0.5f; // 0.5 = 50% speed
	
	private float timeUntilFire;

	private void Update()
	{
		timeUntilFire += Time.deltaTime;

		if (timeUntilFire >= 1f / aps)
		{
			SlowEnemies();
			timeUntilFire = 0f;
		}
	}

	private void SlowEnemies()
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

		foreach (RaycastHit2D hit in hits)
		{
			EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
			if (em != null)
			{
				em.UpdateSpeed(slowFactor); // Slow instead of freeze
				StartCoroutine(ResetEnemySpeed(em));
			}
		}
	}

	private IEnumerator ResetEnemySpeed(EnemyMovement em)
	{
		yield return new WaitForSeconds(slowTime);
		em.ResetSpeed();
	}

	private void OnDrawGizmosSelected()
	{
		Handles.color = Color.cyan;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}
}
