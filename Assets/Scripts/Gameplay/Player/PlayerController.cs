using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player references")]
    [Tooltip("Reference to the player's rigidbody")]
    [SerializeField] Rigidbody rb;
    [SerializeField] CharacterInfo playerStats;
    private PlayerInput input;

    [Header("Player Stats")]
    [SerializeField] int health, attackSpeed, cannonsLevel;
    [SerializeField] float movementSpeed, maxSpeed, rotationSpeed;

    [Header("UI Reference")]
    public GameObject GameOverUI;
    public GameObject InGameUI;
    [SerializeField] PauseMenu PauseUI;
    [SerializeField] ScoreManager _ScoreManager;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject aimButtonYes, aimButtonNo, normalShootingButton, aimingShootingButton;

    [Header("FX")]
    public GameObject CoinParticle;
    public GameObject CoinSfx;
    public GameObject ExplosionSfx;

    [Header("ShootingSystem")]
    public GameObject bulletPrefab;
    public GameObject explosion;
    public Transform cannonPointR1;
    public Transform cannonPointL1;
    public Transform cannonPointF;
    public Transform aimSpawnPoint;
    public float bulletSpeed = 10;
    public float shootingDamage;
    public bool shotOnCooldown = false;
    
    [Header("Cannon Shooting Points")]
    public Transform[] cannonsPointsLeft;
    public Transform[] cannonsPointsRight;

    [Header("Mode Cannons")]
    public GameObject cannonRow2;
    public GameObject cannonRow3;
    public GameObject cannonRow4;
    public GameObject cannonRow5;
    public GameObject cannonRow6;

    [Header("What Player Is Doing")]
    public bool isTurningRight;
    public bool isTurningLeft;
    public bool isAiming;
    /*public bool isLookingRight;
    public bool isLookingLeft;
    public bool isLookingFront;*/

    public GameObject aimCamera;
    public GameObject freeCamera;

    // Variable for storing the active out of bounds coroutine
    // Used to stop the coroutine when the player is back in the map
    private Coroutine outOfBoundsCoroutine;

    #region Unity Methods
    void Start()
    {
        health = playerStats.baseHealth; //+ shop upgrade health
        healthBar.SetMaxHealth(health);
        attackSpeed = playerStats.baseAttackSpeed; //+ shop upgrade
        movementSpeed = playerStats.baseMovementSpeed;//+ shop upgrade
        rotationSpeed = playerStats.baseRotationSpeed;
        cannonsLevel = playerStats.cannonsLevel;
    
        maxSpeed = movementSpeed + 1;
    
        input = GetComponent<PlayerInput>();
    
        isAiming = false;
    
        CheckForUpdates();
    }
    
    private void Update()
    {
        //KeyboardAndGamepad();
        if (isTurningLeft)
        {
            transform.Rotate(0f, rotationSpeed * 0 - rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }
        if (isTurningRight)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }

        //Trailer, Borrar despues de Terminar
        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftDown();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightDown();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            LeftUp();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            RightUp();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Acelerar();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Frenar();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootCannons();
        }

        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
        //trailer
    }

    void FixedUpdate()
    {
        //ForwardMovement
        rb.velocity = (transform.forward * movementSpeed);
        rb.velocity.Normalize();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "LightHouse":
                SceneManager.LoadScene("TrailerBase");
                //PauseUI.Pause();
                break;
            case "Coin":
                _ScoreManager.IncreaseScore("gameScore", 100);
                health++;
                Destroy(other.gameObject);
                Instantiate(CoinParticle, this.transform.position, this.transform.rotation);
                Instantiate(CoinSfx, this.transform.position, this.transform.rotation);
                break;
            case "EnemyBullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(ExplosionSfx, this.transform.position, this.transform.rotation);
                MeMori();
                break;
            case "MapBounds":
                StopCoroutine(outOfBoundsCoroutine);
                Debug.Log("You are in the map");
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MapBounds"))
        {
            //Display return UI
            outOfBoundsCoroutine = StartCoroutine(OutOfBoundsTimer());
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Coin":
                _ScoreManager.IncreaseScore("gameScore", 100);
                health++;
                Destroy(collisionInfo.gameObject);
                Instantiate(CoinParticle, this.transform.position, this.transform.rotation);
                Instantiate(CoinSfx, this.transform.position, this.transform.rotation);
                break;

            case "EnemyBullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(ExplosionSfx, this.transform.position, this.transform.rotation);
                MeMori();
                break;

            default:
                Debug.Log(collisionInfo.gameObject.tag);
                break;
        }
    }
    #endregion

    #region Other Methods
    private void MeMori()
    {
        if (health == 0)
        {
            InGameUI.SetActive(false);
            Destroy(this.gameObject);
            //PauseUI.Pause();
            GameOverUI.SetActive(true);
            _ScoreManager.GameEndProcess();
        }
    }

    public void CheckForUpdates()
    {
        if (cannonsLevel >= 2)
        {
            cannonRow2.SetActive(true);
            Debug.Log("Nivel de Ca�ones = 2");
        }
        if(cannonsLevel >= 3)
        {
            cannonRow3.SetActive(true);
            Debug.Log("Nivel de Ca�ones = 3");
        }
        if (cannonsLevel >= 4)
        {
            cannonRow4.SetActive(true);
            Debug.Log("Nivel de Ca�ones = 4");
        }
        if (cannonsLevel >= 5)
        {
            cannonRow5.SetActive(true);
            Debug.Log("Nivel de Ca�ones = 5");
        }
        if (cannonsLevel >= 6)
        {
            cannonRow6.SetActive(true);
            Debug.Log("Nivel de Ca�ones = 6");
        }
    }

    public void ShootCannons()
    {
        if (shotOnCooldown == false)
        {
            {
                //Cannon F
                Instantiate(explosion, cannonPointF.position, cannonPointF.rotation);
                var bulletF = Instantiate(bulletPrefab, cannonPointF.position, cannonPointF.rotation);
                bulletF.GetComponent<Rigidbody>().velocity = cannonPointF.forward * bulletSpeed;
                Debug.Log("Shoot");
            }

            //Se llama la funcion de disparar actualizada
            for (int i = 1; i <= cannonsLevel; i++)
            {
                DisparaTest(i);
            }

            shotOnCooldown = true;
            StartCoroutine(WaitforCooldown());
        }
    }

    public void DisparaTest(int cannonLevel)
    {
        Instantiate(explosion, cannonsPointsLeft[cannonLevel-1].position, cannonsPointsLeft[cannonLevel - 1].rotation);
        Instantiate(ExplosionSfx, cannonsPointsLeft[cannonLevel-1].position, cannonsPointsLeft[cannonLevel - 1].rotation);
        var bulletL = Instantiate(bulletPrefab, cannonsPointsLeft[cannonLevel - 1].position, cannonsPointsLeft[cannonLevel - 1].rotation);
        bulletL.GetComponent<Rigidbody>().velocity = cannonsPointsLeft[cannonLevel - 1].forward * bulletSpeed;

        Instantiate(explosion, cannonsPointsRight[cannonLevel - 1].position, cannonsPointsRight[cannonLevel - 1].rotation);
        var bulletR = Instantiate(bulletPrefab, cannonsPointsRight[cannonLevel - 1].position, cannonsPointsRight[cannonLevel - 1].rotation);
        bulletR.GetComponent<Rigidbody>().velocity = cannonsPointsRight[cannonLevel - 1].forward * bulletSpeed;
    }
    
    public void Acelerar()
    {
        if (movementSpeed != maxSpeed)
        {
            movementSpeed ++;
        }
    }

    public void Frenar()
    {
        if (movementSpeed > 0)
        {
            movementSpeed --;
        }
    }

    public void RightDown()
    {
        isTurningRight = true;
        Debug.Log("isTurningRight");
    }
    public void RightUp()
    {
        isTurningRight = false;
    }
    public void LeftDown()
    {
        isTurningLeft = true;
        Debug.Log("isTurningLeft");
    }
    public void LeftUp()
    {
        isTurningLeft = false;
    }

    public void Repair()
    {
        Debug.Log("reparatus");
        transform.Rotate(0, 0, 0);
    }

    public void AimYes()
    {
        Debug.Log("apuntado");
        isAiming = true;
        freeCamera.SetActive(false);
        aimCamera.SetActive(true);
        aimButtonYes.SetActive(false);
        aimButtonNo.SetActive(true);
        normalShootingButton.SetActive(false);
        aimingShootingButton.SetActive(true);
    }

    public void AimNo()
    {
        Debug.Log("desapuntado");
        isAiming = false;
        freeCamera.SetActive(true);
        aimCamera.SetActive(false);
        aimButtonNo.SetActive(false);
        aimButtonYes.SetActive(true);
        normalShootingButton.SetActive(true);
        aimingShootingButton.SetActive(false);
    }

    public void AimShoot()
    {
        if (shotOnCooldown == false)
        {
            Instantiate(explosion, aimSpawnPoint.position, aimSpawnPoint.rotation);
            Instantiate(ExplosionSfx, aimSpawnPoint.position, aimSpawnPoint.rotation);
            var aimBullet = Instantiate(bulletPrefab, aimSpawnPoint.position, aimSpawnPoint.rotation);
            aimBullet.GetComponent<Rigidbody>().velocity = aimSpawnPoint.forward * bulletSpeed;
            Debug.Log("Aimed Shoot");

            shotOnCooldown = true;
            StartCoroutine(WaitforCooldown());
        }
    }

    private IEnumerator WaitforCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        shotOnCooldown = false;
    }
    
    private IEnumerator OutOfBoundsTimer()
    {
        //Save the last position and rotation of the player
        var lastPosition = transform.position;
        var lastRotation = transform.rotation;
        //Write in console every second for 15 seconds
        for (int i = 0; i < 15; i++)
        {
            Debug.Log("You are out of bounds, return to the map");
            yield return new WaitForSeconds(1);
        }
        //Return the player to the last position and rotation
        transform.position = lastPosition;
        transform.rotation = Quaternion.Inverse(lastRotation);
    }

    #endregion
}
