using UnityEngine;
using System.Collections;

public class powerUpControl : MonoBehaviour {

	public float powerUpSpeed;
	LoseTrigger LoseTrigger;
	public AudioClip powerUpCollect;

	void Start () {
	rigidbody2D.AddForce (-Vector2.up * powerUpSpeed * Random.Range (1, 5));
	LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
	}
			
	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Player1" || trigger.gameObject.tag == "Player2") {
		switch (gameObject.tag){
			case("MultiBall"):
			LoseTrigger.BallSpawn(1);
			break;
			case("ExtendPaddle"):
			if (trigger.gameObject.tag == "Player1"){
			//Losetrigger.player1Paddle.transform.scale = (10);
				}
			break;
			}
		AudioSource.PlayClipAtPoint(powerUpCollect, trigger.gameObject.transform.position);
		}

		Destroy(this.gameObject);
		}
	}
	

