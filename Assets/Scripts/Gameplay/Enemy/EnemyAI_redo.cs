using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI_redo : MonoBehaviour
{
    #region Variables

    private enum State
    {
        Patrolling,
        Chase,
        Attack
    }
    
    public Transform player;

    public LayerMask whatsIsOcean, whatIsPlayer;

    [Header("Efectos")]
    [HideInInspector]
    public GameObject explosion;
    [HideInInspector]
    public GameObject debris;
    [HideInInspector]
    public GameObject explosionSfx;

    [SerializeField] CharacterInfo enemyStats;

    public GameObject bulletPrefab;
    public Transform aiSpawnPoint;
    public float bulletSpeed = 10;
    
    [SerializeField] int health;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    private State _state;
    public float sightRange, attackRange;

    [Header("Nav Mesh Variables")]
    public NavMeshAgent agent;
    
    //Patrolling needs
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    #endregion 
    
    #region Unity Methods
    private IEnumerator Start()
    {
        health = enemyStats.baseHealth;

        while (true)
        {
            switch ( _state)
            {
                case State.Patrolling:
                    Patrolling();
                    break;
                case State.Chase:
                    ChasePlayer();
                    break;
                case State.Attack:
                    AttackPlayer();
                    break;
            }
            
            yield return 0;
        }
    }

    private void Awake()
    {
        player = GameObject.Find("_Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
            CheckDeath();
        }
    }
    #endregion
    
    #region Other Methods
    private void CheckDeath()
    {
        var transform1 = transform;
        var position = transform1.position;
        var rotation = transform1.rotation;
        
        if (health <= 0)
        {
            Instantiate(explosion, position, rotation);
            Instantiate(explosionSfx, position, rotation);
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("enemiesDefeated", PlayerPrefs.GetInt("enemiesDefeated") + 1);
            PlayerPrefs.SetInt("gameScore", PlayerPrefs.GetInt("gameScore") + 25);
        }  
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Reached point
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        if (Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
        {
            _state = State.Chase;
        }
    }
    private void ChasePlayer()
    {
        Debug.Log("Chase Player");
        agent.SetDestination(player.position);

        if (Physics.CheckSphere(transform.position, attackRange, whatIsPlayer))
        {
            _state = State.Attack;
        }
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Instantiate(explosion, aiSpawnPoint.position, aiSpawnPoint.rotation);
            Instantiate(explosionSfx, aiSpawnPoint.position, aiSpawnPoint.rotation);
            var aimBullet = Instantiate(bulletPrefab, aiSpawnPoint.position, aiSpawnPoint.rotation);
            aimBullet.GetComponent<Rigidbody>().velocity = aiSpawnPoint.forward * bulletSpeed;
            Debug.Log("enemy Shooted");
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        if (!Physics.CheckSphere(transform.position, attackRange, whatIsPlayer))
        {
            _state = State.Chase;
        }
    }
    void SearchWalkPoint() {
        Debug.Log("SearchWalkPoint");
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);
        walkPointSet = true; 
    }
    
    void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    #endregion
}
