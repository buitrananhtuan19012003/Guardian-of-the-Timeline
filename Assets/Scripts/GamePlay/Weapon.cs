using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform firePos;
    [SerializeField]
    private float TimeBtwFire;
    [SerializeField]
    private float bulletForce;
    [SerializeField]
    private Camera cam;
    Vector2 mousePos;
    Vector2 dir;

    private float timeBtwFire;
    
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        //RotateGun();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        timeBtwFire -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timeBtwFire < 0)
        {
            FireBullet();
        }
        
    }
    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + dir * playerSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rigidBody.rotation = angle;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1, -1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
    }
    //void RotateGun()
    //{
    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 lookDir = mousePos - transform.position;
    //    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

    //    Quaternion rotation = Quaternion.Euler(0, 0, angle);
    //    transform.rotation = rotation;
    //    if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
    //    {
    //        transform.localScale = new Vector3(1, -1, 0);
    //    }
    //    else
    //    {
    //        transform.localScale = new Vector3(1, 1, 0);
    //    }
    //}

    void FireBullet()
    {
        timeBtwFire = TimeBtwFire;
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        Rigidbody2D rigidbody = bulletTmp.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }
}
