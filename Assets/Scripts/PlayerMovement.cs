using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;

    public Rigidbody2D rb;

    public Camera cam;

    //private bool GameOver;

    public bool CanTeleport;

    public int Life { get { return CurrentLife; } }

    public int maxLife = 3;

    public int CurrentLife;

    public int score;

    Vector2 movement;
    Vector2 mousePos;


    void Start()
    {
        //GameOver = false;

        CanTeleport = false;

        CurrentLife = maxLife;

        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.X) && (CanTeleport != false) )
        {
            SceneManager.LoadScene("Scene2");
        }

        if (Life == 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScoreEnemy()
    {
        score += 100;
    }

    public void ChangeScoreSpawner()
    {
        score += 200;
    }

    public void ChangeLife(int amount)
    {
        CurrentLife = Mathf.Clamp(CurrentLife + amount, 0, maxLife);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime); 

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle; 
    }
}
