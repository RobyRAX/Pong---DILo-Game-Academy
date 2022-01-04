using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    //Titik awal lintasan bola
    private Vector2 trajectoryOrigin;

    //Simpan laju awal bola
    [SerializeField]
    float initialBallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        trajectoryOrigin = transform.position;
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();

        //Mulai Game
        RestartGame();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LimitBallVelocity();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get
        {
            return trajectoryOrigin;
        }
    }

    void ResetBall()
    {
        //Reset posisi
        transform.position = Vector2.zero;

        //Reset kecepatan
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);

        if (randomDirection < 1.0f)
        {
            //Gerak ke kiri
            //rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));
            
        }
        else
        {
            //Gerak ke kanan
            //rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
            rigidBody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
            
        }
        Invoke("GetBallSpeed", 0.5f);
    }

    void RestartGame()
    {
        ResetBall();

        //Delay 2 detik
        Invoke("PushBall", 2);
    }
    
    void GetBallSpeed()
    {
        initialBallSpeed = rigidBody2D.velocity.magnitude;
    }

    void LimitBallVelocity()
    {
        if (rigidBody2D.velocity.magnitude > initialBallSpeed && initialBallSpeed != 0)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * initialBallSpeed;
        }
    }

}
