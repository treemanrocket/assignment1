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

    Vector2 movement;
    Vector2 mousePos;

    public int bubble;

    AudioSource audioSource;

    public AudioClip PickUpSound;
    void Start()
    {
        //GameOver = false;

        CanTeleport = false;

        CurrentLife = maxLife;

        bubble = 0;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // tamkes the mouse position from the screen and transfers to the world 
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.X) )
        {
            if ((CanTeleport != false) && (bubble == 1))
            {
                SceneManager.LoadScene("Scene2");
            }
        }

        if (Life == 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeBubble() //public method to change the bubble amount for the current scene
    {
        bubble += 1;
        PlaySound(PickUpSound);
    }

    public void ChangeLife(int amount) // public method to change the bubble amount
    {
        CurrentLife = Mathf.Clamp(CurrentLife + amount, 0, maxLife);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime);  

        Vector2 lookDir = mousePos - rb.position; // math for the look direction
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; // math to find the angle from the player and the mouse position by suptracting the vectors of the player and the mouse
        rb.rotation = angle; 
    }
}
