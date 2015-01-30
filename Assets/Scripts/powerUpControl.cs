using UnityEngine;
using System.Collections;

public class powerUpControl : MonoBehaviour {

	public float powerUpSpeed;
	PowerUpControlCentre powerUpControlCentre;
	public AudioClip powerUpCollect;


	void Start () {
	rigidbody2D.AddForce (-Vector2.up * powerUpSpeed * Random.Range (1, 5));
	powerUpControlCentre = GameObject.FindObjectOfType<PowerUpControlCentre> ();
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
					powerUpControlCentre.p1ExtendCount++;
				}
				else if (trigger.gameObject.tag == "Player2"){
					powerUpControlCentre.PaddleSizeChange("Extend", 2);
					powerUpControlCentre.paddle2Extd = true;
					powerUpControlCentre.p2ExtendCount++;
				}
			break;
			case("ShortenPaddle"):
				if (trigger.gameObject.tag == "Player1"){
					powerUpControlCentre.PaddleSizeChange("Shrink", 2);
					powerUpControlCentre.paddle2Shrink = true;
					powerUpControlCentre.p2ShrinkCount++;
				}
				else if (trigger.gameObject.tag == "Player2"){
					powerUpControlCentre.PaddleSizeChange("Shrink", 1);
					powerUpControlCentre.paddle1Shrink = true;
					powerUpControlCentre.p1ShrinkCount++;
				}
				break;
			}
		AudioSource.PlayClipAtPoint(powerUpCollect, trigger.gameObject.transform.position);
		}
		Destroy(this.gameObject);
		}
	}





	

