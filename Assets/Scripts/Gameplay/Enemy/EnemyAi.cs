using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    #region Variables
    
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

    public AroundSpawner aroundSpawner;
    
    

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    [Header("Nav Mesh Variables")]
    public NavMeshAgent agent;
    
    //Patrolling needs
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    #endregion 
    
    #region Unity Methods
    private void Start()
    {
        aroundSpawner = GameObject.Find("EnemySpawnPoints").GetComponent<AroundSpawner>();
        health = enemyStats.baseHealth;
    }

    private void Awake()
    {
        player = GameObject.Find("_Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        var rotation = transform1.rotation;
       
        playerInSightRange = Physics.CheckSphere(position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(position, attackRange, whatIsPlayer);
        
        SetBehaviour();

        if (health <= 0)
        {
            Instantiate(explosion, position, rotation);
            Instantiate(explosionSfx, position, rotation);
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("enemiesDefeated", PlayerPrefs.GetInt("enemiesDefeated") + 1);
            PlayerPrefs.SetInt("gameScore", PlayerPrefs.GetInt("gameScore") + 25);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Bullet"))
        {
            health--;
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosionSfx, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
        }
    }
    #endregion
    #region Other Methods
    
    //Esto debería de ser una maquina de estados
    private void SetBehaviour() 
    {
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    private void Patrolling()
    {
        Debug.Log("Patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Reached point
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void ChasePlayer()
    {
        Debug.Log("Chase Player");
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        Debug.Log("Attack");
        //make sure enemy doesn't move
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
    }
    void SearchWalkPoint() {
        Debug.Log("SearchWalkPoint");
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatsIsOcean))
            walkPointSet = true;
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    #endregion
}
