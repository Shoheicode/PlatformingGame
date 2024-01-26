using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    public float jumpSpeed;

    private Rigidbody rb;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        Move();
        if (Input.GetButtonDown("Jump"))
        {
            tryJump();
        }
    }

    private void Move()
    {
        
        //Gets user input
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        if (xInput != 0 || zInput != 0)
        {
            Vector3 currface = new Vector3(xInput, 0, zInput);

            bool fac = currface != transform.forward;
            if (!tryForward() || fac) { 
                Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
                dir.y = rb.velocity.y;
                rb.velocity = dir;

                Vector3 facing = new Vector3(xInput, 0, zInput);

                if (facing.magnitude > 0)
                {
                    transform.forward = facing;
                }
            }
        }
    }

    private bool tryForward()
    {
        //Gets user input
        //float xInput = Input.GetAxis("Horizontal");
        //float zInput = Input.GetAxis("Vertical");
        //Vector3 facing = new Vector3(transform.forward, 0, zInput);

        Ray ray1 = new Ray(transform.position + new Vector3(0, 0, 0), transform.forward);
        Ray ray2 = new Ray(transform.position + new Vector3(0, 0.5f, 0), transform.forward);
        Ray ray3 = new Ray(transform.position + new Vector3(0, -0.5f, 0), transform.forward);
        //Ray ray2 = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), transform.forward);
        //Ray ray3 = new Ray(transform.position + new Vector3(0.5f, 1f, -0.5f), transform.forward);
        //Ray ray4 = new Ray(transform.position + new Vector3(-0.5f, 1f, -0.5f), transform.forward);

        bool cast1 = Physics.Raycast(ray1, 0.7f);
        bool cast2 = Physics.Raycast(ray2, 0.7f);
        bool cast3 = Physics.Raycast(ray3, 0.7f);
        //bool cast4 = Physics.Raycast(ray4, 0.7f);

        return cast1 || cast2 || cast3;
    }

    private void tryJump()
    {
        Ray ray1 = new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down);

        bool cast1 = Physics.Raycast(ray1, 0.7f);
        bool cast2 = Physics.Raycast(ray2, 0.7f);
        bool cast3 = Physics.Raycast(ray3, 0.7f);
        bool cast4 = Physics.Raycast(ray4, 0.7f);

        if (cast1 || cast2 || cast3|| cast4)
        {

            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.LoseGame();
        }
        if (other.CompareTag("Coin"))
        {
            //Add Score
            GameManager.instance.AddScore(1);
            GameUI.instance.UpdateScoreText();
            Destroy(other.gameObject);
            audioSource.Play();

        }
        if (other.CompareTag("Goal"))
        {
            GameManager.instance.LevelEnd();
        }
    }
}
