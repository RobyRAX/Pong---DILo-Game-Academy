using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Pemain 1
    public PlayerControl player1;
    private Rigidbody2D player1RigidBody;

    //Pemain 2
    public PlayerControl player2;
    private Rigidbody2D player2RigidBody;

    //Bola
    public BallControl ball;
    private Rigidbody2D ballRigidBody;
    private CircleCollider2D ballCollider;

    //Skor maksimal
    public int maxScore;

    //Debug Window
    private bool isDebugWindowShown = true;

    //Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;
    

    // Start is called before the first frame update
    void Start()
    {
        player1RigidBody = player1.GetComponent<Rigidbody2D>();
        player2RigidBody = player2.GetComponent<Rigidbody2D>();
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        //Tampilkan skor pemain
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        //Tombol restart
        if(GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            //Tombol ditekan --> reset skor kedua pemain
            player1.ResetScore();
            player2.ResetScore();

            //restart game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        //Pemain 1 menang
        if(player1.Score == maxScore)
        {
            //PopUp player one win
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            //reset bola ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //Pemain 2 menang
        else if(player2.Score == maxScore)
        {
            //PopUp player one win
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            //reset bola ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //Tampilkan area debug
        if(isDebugWindowShown)
        {
            trajectory.enabled = true;
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            //Simpan variabel physic yang akan ditampilkan
            float ballMass = ballRigidBody.mass;
            Vector2 ballVelocity = ballRigidBody.velocity;
            float ballSpeed = ballRigidBody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            //Debug text
            string debugText =
                "Ball Mass = " + ballMass + "\n" +
                "Ball Velocity = " + ballVelocity + "\n" +
                "Ball Speed = " + ballSpeed + "\n" +
                "Ball Momentum = " + ballMomentum + "\n" +
                "Ball Friction = " + ballFriction + "\n" +
                "Last Impulse From Player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last Impulse From Player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            // Tampilkan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            // Kembalikan warna lama GUI
            GUI.backgroundColor = oldColor;

            // Toggle nilai debug window ketika pemain mengeklik tombol ini.
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
            {
                isDebugWindowShown = false;
            }
        }

        //sembunyikan area debug
        else
        {
            trajectory.enabled = false;
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
            {
                isDebugWindowShown = true;
            }
        }
    }
}
