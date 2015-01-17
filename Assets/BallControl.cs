using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	public float initialBallForce;
	public float wallBallForce;
	public float paddleForce;
	playerControl PlayerControl;
	LoseTrigger LoseTrigger;
	public bool player1LastHit;
	public bool player2LastHit;
	private int totalPlayerScore;
	public AudioClip paddleHitSound;
	public AudioClip wallHitSound;
	public AudioClip wrongPaddleHit;
	public GameObject wrongPaddleHitSpark;
	
	void Start () {
		PlayerControl = GameObject.FindObjectOfType<playerControl> ();
		LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
		rigidbody2D.AddForce (-Vector2.up * initialBallForce);
	}

	void Update(){
		totalPlayerScore = LoseTrigger.player1Score + LoseTrigger.player2Score;
		}
	
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Walls") {
			AudioSource.PlayClipAtPoint(wallHitSound, coll.gameObject.transform.position);
			if(player1LastHit == true){
				LoseTrigger.player1Score++;
			}
			else if (player2LastHit == true){
				LoseTrigger.player2Score++;
			}
			rigidbody2D.AddForce (-Vector2.up * wallBallForce * totalPlayerScore);
			}
		if (coll.gameObject.tag == "Player1" || coll.gameObject.tag == "Player2") {
			rigidbody2D.AddForce (Vector2.up * paddleForce);

			if (coll.gameObject.tag == "Player1") {
				if (player1LastHit == true){
				LoseTrigger.paddle1Frozen = true;
				AudioSource.PlayClipAtPoint(wrongPaddleHit, coll.gameObject.transform.position);
				Instantiate(wrongPaddleHitSpark,coll.transform.position, Quaternion.identity);
				}
				if (player1LastHit == false){
				AudioSource.PlayClipAtPoint(paddleHitSound, coll.gameObject.transform.position);
				
				}
				player1LastHit = true;
				player2LastHit = false;
			} else if (coll.gameObject.tag == "Player2") {
				if (player2LastHit == true){
					LoseTrigger.paddle2Frozen = true;
					AudioSource.PlayClipAtPoint(wrongPaddleHit, coll.gameObject.transform.position);
					Instantiate(wrongPaddleHitSpark,coll.transform.position, Quaternion.identity);
				}
				if (player2LastHit == false){
					AudioSource.PlayClipAtPoint(paddleHitSound, coll.gameObject.transform.position);
					}
				player1LastHit = false;
				player2LastHit = true;
			}
		}
	}
}


