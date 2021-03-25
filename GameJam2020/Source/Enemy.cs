using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public float maxHealth = 60f;
    public float health;
    public float damage = 8f;
    public float force = 3.5f;

    AudioSource audioSource;
    Rigidbody rb;
    Animator animator;

    private float maxTimer = .1f;
    private float timer;
    public float maxAttackTimer = 1f;
    private float attackTimer;

    AIDestinationSetter aIDestinationSetter;
    

    public bool sleeping = true;

    public enum EnemyType
    {
        Jelly,
        Shooter,
        Dog
    }

    public EnemyType enemyType;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        health = maxHealth;
        timer = 0f;
        attackTimer = maxAttackTimer;
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aIDestinationSetter.target = transform;
    }

    public void TakeDamage(float damage, Vector3 forceAndDir)
    {
        health -= damage;
        rb.AddForce(forceAndDir, ForceMode.Impulse);
        audioSource.Play();
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        if (health <= 0f)
        {
            Die();
        }

        if (GameController.hasWon)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && attackTimer <= 0f)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = dir.normalized;

            attackTimer = maxAttackTimer;
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);

            Camera.main.GetComponent<CameraShake>().CamShake(0.10f, 0.6f);
        }
    }

    public void WakeUp()
    {
        aIDestinationSetter.target = GameObject.FindWithTag("Player").transform;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Weapon" && timer <= 0f)
        {
            timer = maxTimer;

            SwordDamage sword = collider.GetComponent<SwordDamage>();
            if (sword.shouldDamage)
            {

                Vector3 dir = collider.transform.parent.parent.parent.position - transform.position;
                dir = -dir.normalized;

                TakeDamage(sword.damage, dir * sword.force);

                Camera.main.GetComponent<CameraShake>().CamShake(0.08f, 0.35f);
            }
        }
    }

    public void Die()
    {
        animator.SetBool("shouldDie", true);
    }
}
