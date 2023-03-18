using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float dmgValue = 4;
	public GameObject throwableObject;
	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
    public bool canAttack2 = true;
    public bool isTimeToCheck = false;
	public int attackDamage = 40;

	public GameObject cam;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
        {
			Attacking();
		}

		if (Input.GetKeyDown(KeyCode.V))
		{
            canAttack2 = false;
            GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f, 0.3f), Quaternion.identity) as GameObject; 
			Vector2 direction = new Vector2(transform.localScale.x, 0);
			throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
			throwableWeapon.name = "ThrowableWeapon";
            StartCoroutine(RangedCooldown());
        }
	}

	void Attacking()
	{
		animator.SetTrigger("Attack");

		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		foreach(Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return; 

		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(0.25f);
		canAttack = true;
	}

    IEnumerator RangedCooldown()
    {
        yield return new WaitForSeconds(10f);
        canAttack2 = true;
    }

    public void DoDashDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				/*if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
				cam.GetComponent<CameraFollow>().ShakeCamera();*/
			}
		}
	}
}
