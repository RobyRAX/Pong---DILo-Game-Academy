using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Tampilkan variabel physic
    private ContactPoint2D lastContactPoint;

    //Akses info contactPoint dari class lain
    public ContactPoint2D LastContactPoint
    {
        get
        {
            return lastContactPoint;
        }
    }

    // Tombol untuk menggerakkan ke atas
    public KeyCode upButton = KeyCode.W;

    // Tombol untuk menggerakkan ke bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10.0f;

    // Batas atas dan bawah game scene (Batas bawah menggunakan minus (-))
    public float yBoundary = 9.0f;

    // Rigidbody 2D raket ini
    private Rigidbody2D rigidBody2D;

    // Skor pemain
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ambil kecepatan raket
        Vector2 velocity = rigidBody2D.velocity;

        //Tekan atas 
        if(Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        //Tekan bawah
        else if(Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        //Gak tekan apa-apa
        else
        {
            velocity.y = 0.0f;
        }

        //Masukkan kembali velocity 
        rigidBody2D.velocity = velocity;

        //----------------------------------//
        
        //Dapatkan posisi raket
        Vector3 position = transform.position;

        //Jika posisi melewati batas(+), maka kembalikan ke batas tersebut
        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        //Jika posisi melewati batas(-), maka kembalikan ke batas tersebut
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        //Masukkan kembali nilai position ke posisi object
        transform.position = position;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
}
