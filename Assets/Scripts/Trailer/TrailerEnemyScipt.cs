using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerEnemyScipt : MonoBehaviour
{
    [Header("Efectos")]
    public GameObject Explosion;
    public GameObject debris;
    public GameObject Sfx;

    [Tooltip("Reference to the player's rigidbody")]
    [SerializeField] Rigidbody rb;

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Player":
                Destroy(this.gameObject);
                Instantiate(Explosion, this.transform.position, this.transform.rotation);
                Instantiate(Sfx, this.transform.position, this.transform.rotation);
                break;
            case "Bullet":
                Instantiate(Explosion, this.transform.position, this.transform.rotation);
                Instantiate(Sfx, this.transform.position, this.transform.rotation);
                break;
            default: //Debug.Log(collisionInfo.gameObject.tag);
                break;
        }
    }
}
