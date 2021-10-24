using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerRigidbody : MonoBehaviour
{
    public float speed = 2f;
    public float rotSpeed = 20f;
    Rigidbody rb;
    float newRotY = 0;
    public GameObject prefabsBullet;
    public Transform gunPosition;
    public float gunPower = 15f;
    public float gunCooldown = 2f;
    public float gunCooldownCount = 0;
    public bool hasGun = false;
    public int BulletCount = 0;

    public int coincount = 0;
    public PlaygroundSceneManager manager;
    public AudioSource audioCoin;
    public AudioSource audioFire;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<PlaygroundSceneManager>();
        if(manager == null)
        {
            print("Manager Not Found");
        }
    }


    void FixedUpdate()
    { 
        float horizontal = Input.GetAxis("Horizontal")* speed;
        float vertical = Input.GetAxis("Vertical")* speed;
        if(horizontal > 0)
        {
            newRotY = 90;
        }
        else if(horizontal < 0)
        {
            newRotY = 90;
        }
        if(vertical > 0)
        {
            newRotY = 0;
        }
        else if(vertical < 180)
        {
            newRotY = 180;
        }

        rb.AddForce(horizontal, 0, vertical, ForceMode.VelocityChange);
        transform.rotation = Quaternion.Lerp(
                                         Quaternion.Euler(0, newRotY, 0),
                                         transform.rotation,
                                         rotSpeed * Time.deltaTime
                                        );


    }

    private void Update()
    {
        gunCooldownCount = gunCooldownCount + Time.deltaTime;
        if (Input.GetButtonDown("Fire1")&& (BulletCount > 0) && (gunCooldownCount >= gunCooldown))
        {
            gunCooldownCount = 0;
            GameObject bullet = Instantiate(prefabsBullet, gunPosition.position, gunPosition.rotation);
            Rigidbody bRb = bullet.GetComponent<Rigidbody>();
            bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);
            Destroy(bullet, 2f);

            BulletCount--;
            manager.SetTextBullet(BulletCount);
            audioFire.Play();

        }
        
    }
    private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Collectable")
            {
                Destroy(collision.gameObject);
            }

        }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if(other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            coincount++;
            manager.SetTextCoin(coincount);
            audioCoin.Play();
        }
        if(other.gameObject.name == "Gun")
        {
            print("You Found A Gun");
            Destroy(other.gameObject);
            hasGun = true;
            BulletCount += 10;
            manager.SetTextBullet(BulletCount);
        }
    }



}