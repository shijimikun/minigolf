using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    bool left_ = false;
    bool right_ = false;
    bool shot = false;
    public GameObject camera_;
    Vector3 camerapos;
    public GameObject goal;
    public float shotpower = 10;
    float shotpower_y;
    int increase = 15;
    float angle;
    Vector3 respawn;
    Rigidbody rb;
    int trigger = 0;
    AudioSource audioSource;
    public AudioClip goalsound1;
    public AudioClip goalsound2;
    public AudioClip goalsound3;
    float rotatespeed = 30f;
    float rotate;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        camerapos = camera_.transform.position - gameObject.transform.position;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        camera_.transform.position = gameObject.transform.position + camerapos;
        if (shot)
        {
            shotpower_();
        }
        if (transform.position.y <= -0.5)
        {
            transform.position = respawn;
            rb = gameObject.GetComponent<Rigidbody>();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = Vector3.zero;
        }
        shotpower_y = shotpower * 1.5f;
        if (5 > shotpower)
        {
            increase = 15;
        }
        if (80 < shotpower)
        {
            increase = -15;
        }
        if (gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
        {
            trigger = 0;
            if (right_ == true)
            {
                angle = transform.localEulerAngles.y;
                transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
                trigger = 1;
            }
            if (left_ == true)
            {
                angle = transform.localEulerAngles.y;
                transform.Rotate(new Vector3(0, -30 * Time.deltaTime, 0));
                trigger = 1;
            }
            if (trigger == 0)
            {
                rb = gameObject.GetComponent<Rigidbody>();
                transform.rotation = Quaternion.Euler(0, angle, 0);
                rb.velocity = Vector3.zero;
                trigger = 1;
            }
        }
    }
    public void shot_()
    {
        shot = true;
    }
    public void shotUp()
    {
        shot = false;
        boalpower();
    }
    void boalpower()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
        {
            count++;
            trigger = 0;
            rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shotpower);
            rb.AddForce(0, shotpower_y, 0);
            shotpower = 5;
            respawn = this.transform.position;
        }
    }
    public void left()
    {
        left_ = true;
    }
    public void leftup()
    {
        left_ = false;
    }
    public void right()
    {
        right_ = true;
    }
    public void rightup()
    {
        right_ = false;
    }
    public void shotpower_()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
        {
            shotpower += increase * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "goal")
        {
            if (count < 2)
            {
                audioSource.PlayOneShot(goalsound3);
            }
            if (count < 4 && count > 1)
            {
                audioSource.PlayOneShot(goalsound2);
            }
            if (count > 3)
            {
                audioSource.PlayOneShot(goalsound1);
            }
        }
    }
}
