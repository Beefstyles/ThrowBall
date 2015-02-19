using UnityEngine;
using System.Collections;

public class powerUpControl : MonoBehaviour {

	LoseTrigger LoseTrigger;
	public float powerUpSpeed;
	PowerUpControlCentre powerUpControlCentreScript;
	public AudioClip powerUpCollect;


	void Start () {
	LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
	rigidbody2D.AddForce (-Vector2.up * powerUpSpeed * Random.Range (1, 5));
	powerUpControlCentreScript = GameObject.FindObjectOfType<PowerUpControlCentre> ();
	LoseTrigger.p1LightSprite = LoseTrigger.p1Light.GetComponentInChildren<SpriteRenderer>();
	LoseTrigger.p2LightSprite = LoseTrigger.p2Light.GetComponentInChildren<SpriteRenderer>();
	}
	
			
	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Player1" || trigger.gameObject.tag == "Player2") {
		switch (gameObject.tag){
			case("MultiBall"):
				powerUpControlCentreScript.Multiball(1);
			break;
			case("ExtendPaddle"):
			if (trigger.gameObject.tag == "Player1"){
					powerUpControlCentreScript.PaddleSizeChange("Extend", 1);
					powerUpControlCentreScript.paddle1Extd = true;
					powerUpControlCentreScript.p1ExtendCount++;
					LoseTrigger.p1LightSprite.sprite = LoseTrigger.extend;
				}
				else if (trigger.gameObject.tag == "Player2"){
					powerUpControlCentreScript.PaddleSizeChange("Extend", 2);
					powerUpControlCentreScript.paddle2Extd = true;
					powerUpControlCentreScript.p2ExtendCount++;
					LoseTrigger.p2LightSprite.sprite = LoseTrigger.extend;
				}
			break;
			case("ShortenPaddle"):
				if (trigger.gameObject.tag == "Player1"){
					powerUpControlCentreScript.PaddleSizeChange("Shrink", 2);
					powerUpControlCentreScript.paddle2Shrink = true;
					powerUpControlCentreScript.p2ShrinkCount++;
					LoseTrigger.p2LightSprite.sprite = LoseTrigger.shrink;
				}
				else if (trigger.gameObject.tag == "Player2"){
					powerUpControlCentreScript.PaddleSizeChange("Shrink", 1);
					powerUpControlCentreScript.paddle1Shrink = true;
					powerUpControlCentreScript.p1ShrinkCount++;
					LoseTrigger.p1LightSprite.sprite = LoseTrigger.shrink;
				}
			break;
			case("Wormhole"):
				if(trigger.gameObject.tag == "Player1"){
					powerUpControlCentreScript.p1WormholePicked = true;
					LoseTrigger.p1LightSprite.sprite = LoseTrigger.wormHole;
				}
				else if(trigger.gameObject.tag == "Player2"){
					powerUpControlCentreScript.p2WormholePicked = true;
					LoseTrigger.p2LightSprite.sprite = LoseTrigger.wormHole;
				}
			break;
			case("Barrier"):

				break;
			}
		AudioSource.PlayClipAtPoint(powerUpCollect, trigger.gameObject.transform.position);
		}
		Destroy(this.gameObject);
		}
	}





	

