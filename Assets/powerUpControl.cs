using UnityEngine;
using System.Collections;

public class powerUpControl : MonoBehaviour {

	public float powerUpSpeed;
	LoseTrigger LoseTrigger;
	PowerUpControlCentre powerUpControlCentre;
	public AudioClip powerUpCollect;
	private float extendPaddleTime;
	public Vector3 extendPaddleVector;
	public Vector3 shrinkPaddleVector;
	private float changePaddleSizeTime;
	private bool paddle1Extd, paddle2Extd;
	private bool paddle1Shrink, paddle2Shrink;
	public float paddleSizeChangePowerupTime;

	void Start () {
	rigidbody2D.AddForce (-Vector2.up * powerUpSpeed * Random.Range (1, 5));
	LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
	powerUpControlCentre = GameObject.FindObjectOfType<PowerUpControlCentre> ();
	changePaddleSizeTime = paddleSizeChangePowerupTime;
	}
			
	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Player1" || trigger.gameObject.tag == "Player2") {
		switch (gameObject.tag){
			case("MultiBall"):
			powerUpControlCentre.Multiball(1);
			break;
			case("ExtendPaddle"):
			if (trigger.gameObject.tag == "Player1"){
					powerUpControlCentre.PaddleSizeChange("Extend", 1);
					powerUpControlCentre.paddle1Extd = true;
				}
				else if (trigger.gameObject.tag == "Player2"){
						LoseTrigger.player2PaddleGO.transform.localScale += extendPaddleVector;
				
				}
			break;
			case("ShortenPaddle"):
				if (trigger.gameObject.tag == "Player1"){
					LoseTrigger.player1PaddleGO.transform.localScale += shrinkPaddleVector;
					changePaddleSizeTime = 3F;

				}
				else if (trigger.gameObject.tag == "Player2"){
					LoseTrigger.player2PaddleGO.transform.localScale += shrinkPaddleVector;
	
				}
				break;
			}
		AudioSource.PlayClipAtPoint(powerUpCollect, trigger.gameObject.transform.position);
		}
		Destroy(this.gameObject);
		}
	}





	

