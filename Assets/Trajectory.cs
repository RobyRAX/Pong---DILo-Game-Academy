using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    //Skrip, collider, rigidbody
    public BallControl ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigidbody;

    //Bola "bayangan" yang akan tampil
    public GameObject ballAtCollision;

    //status pantulan
    bool drawBallAtCollision = false;

    //Titik tumbukan yang digeser untuk menggambar ballAtCollision
    Vector2 offsetHitPoint = new Vector2();


    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Tentukan titik tumbuk dengan deteksi gerak lingkaran
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius, ballRigidbody.velocity.normalized);

        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if(circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                //tentukan titik tumbuk
                Vector2 hitPoint = circleCastHit2D.point;

                //tentukan normal di titik tumbuk
                Vector2 hitNormal = circleCastHit2D.normal;

                //tentukan offsetHitPoint
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

                // Gambar garis lintasan 
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                //kalo bukan Sidewall, maka gambar
                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    //hitung vektor datang
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

                    //hitung vektor keluar
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    //hitung dot product dari outVector dan hitNormal
                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if(outDot > -1.0f && outDot < 1.0f)
                    {
                        //Gambar lintasan pantulan
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint, offsetHitPoint + outVector * 10.0f);
                        drawBallAtCollision = true;
                    }
                }
                //Kalo sudah berhasil gambar, kaluar dari loop
                break;
            }            
        }

        if (drawBallAtCollision)
        {
            //gambar bola bayangan
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        }
        else
        {
            //sembunyikan
            ballAtCollision.SetActive(false);
        }
    }
}
