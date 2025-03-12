using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Transform player;

    public LayerMask whatIsPlayer;

    [Header("Efectos")]
    public GameObject explosion;
    public GameObject debris;
    public GameObject explosionSfx;

    public GameObject bulletPrefab;
    public Transform aiSpawnPoint;
    public float bulletSpeed = 20;
    public int health;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("_Player").transform;
    }

    private void Update()
    {
        AttackPlayer();

        if (health <= 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("enemiesDefeated", PlayerPrefs.GetInt("enemiesDefeated") + 1);
            PlayerPrefs.SetInt("gameScore", PlayerPrefs.GetInt("gameScore") + 25);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Bullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
                break;
            default: //Debug.Log(collisionInfo.gameObject.tag);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
                break;
            default:
                break;
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Attack");

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
    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
