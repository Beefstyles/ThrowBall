using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	public float initialBallForce;
	public float wallBallForce;
	public float paddleForce;
	playerControl[] PlayerControl;
	LoseTrigger LoseTrigger;
	public bool player1LastHit;
	public bool player2LastHit;
	private int totalPlayerScore;
	public AudioClip paddleHitSound;
	public AudioClip wallHitSound;
	public AudioClip wrongPaddleHit;
	public GameObject wrongPaddleHitSpark;
	public float sidewaysForce;
	public Vector2 ballVelocity;
    
		
	void Start () {
		PlayerControl = GameObject.FindObjectsOfType<playerControl>();
		LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
		GetComponent<Rigidbody2D>().AddForce (-Vector2.up * initialBallForce);
	}

	void Update(){
		totalPlayerScore = LoseTrigger.player1Score + LoseTrigger.player2Score;
		ballVelocity = GetComponent<Rigidbody2D>().velocity;
		}
	
	void OnCollisionEnter2D(Collision2D coll){
				if (coll.gameObject.tag == "Walls") {
						AudioSource.PlayClipAtPoint (wallHitSound, coll.gameObject.transform.position);
						if (player1LastHit == true) {
								LoseTrigger.player1Score++;
						} else if (player2LastHit == true) {
								LoseTrigger.player2Score++;
						}
						GetComponent<Rigidbody2D>().AddForce (-Vector2.up * wallBallForce * totalPlayerScore);
				}
				if (coll.gameObject.tag == "Player1" || coll.gameObject.tag == "Player2") {
						GetComponent<Rigidbody2D>().AddForce (Vector2.up * paddleForce);

						if (coll.gameObject.tag == "Player1") {
								if (player1LastHit == true) {
										LoseTrigger.paddle1Frozen = true;
										AudioSource.PlayClipAtPoint (wrongPaddleHit, coll.gameObject.transform.position);
										GameObject wrongPaddleSpark = Instantiate (wrongPaddleHitSpark, LoseTrigger.player1PaddleGO.transform.position, Quaternion.identity) as GameObject;
										LoseTrigger.player1Score = LoseTrigger.player1Score - 5;
										Destroy (wrongPaddleSpark.gameObject, wrongPaddleSpark.GetComponent<ParticleSystem>().duration);
								}
								if (player1LastHit == false) {
										AudioSource.PlayClipAtPoint (paddleHitSound, coll.gameObject.transform.position);
								}
								player1LastHit = true;
								player2LastHit = false;
				if(LoseTrigger.singlePlayer == true){
					playerControl activePlayerControl = PlayerControl [0];
					if (activePlayerControl.gameObject.tag == "Player1") {
						Debug.Log ("P1 hit sideways");
						GetComponent<Rigidbody2D>().AddForce (Vector2.right * activePlayerControl.h * sidewaysForce);
					}
				}

                else if (LoseTrigger.singlePlayer == false && !LoseTrigger.AIMatch)
                {
					playerControl activePlayerControl = PlayerControl [1];
					if (activePlayerControl.gameObject.tag == "Player1") {
						Debug.Log ("P1 hit sideways");
						GetComponent<Rigidbody2D>().AddForce (Vector2.right * activePlayerControl.h * sidewaysForce);
					}
				}
								
						} else if (coll.gameObject.tag == "Player2") {
								if (player2LastHit == true) {
										LoseTrigger.paddle2Frozen = true;
										AudioSource.PlayClipAtPoint (wrongPaddleHit, coll.gameObject.transform.position);
										GameObject wrongPaddleSpark = Instantiate (wrongPaddleHitSpark, LoseTrigger.player2PaddleGO.transform.position, Quaternion.identity) as GameObject;
										LoseTrigger.player2Score = LoseTrigger.player2Score - 5;
										Destroy (wrongPaddleSpark.gameObject, wrongPaddleSpark.GetComponent<ParticleSystem>().duration);
								}
								if (player2LastHit == false) {
										AudioSource.PlayClipAtPoint (paddleHitSound, coll.gameObject.transform.position);
								}
								player1LastHit = false;
								player2LastHit = true;
								playerControl activePlayerControl = PlayerControl [0];
			
								if (activePlayerControl.gameObject.tag == "Player2") {
										Debug.Log ("P2 hit sideways");
										GetComponent<Rigidbody2D>().AddForce (Vector2.right * activePlayerControl.h2 * sidewaysForce);
								}
						}
				}
		}


	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "P1Back" && LoseTrigger.ghostModeP1Active == false) {
            LoseTrigger.ghostModeP1Active = true;
				} 
		else if (trigger.gameObject.tag == "P2Back" && LoseTrigger.ghostModeP2Active == false) {

            if (LoseTrigger.singlePlayer == false)
            {
                LoseTrigger.ghostModeP2Active = true;
            }
		
				}
    
	}
}


