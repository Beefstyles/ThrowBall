using UnityEngine;
using System.Collections;

public class powerUpControl : MonoBehaviour {

	LoseTrigger LoseTrigger;
	public float powerUpSpeed;
	PowerUpControlCentre powerUpControlCentreScript;
	public AudioClip powerUpCollect;
	//public SpriteRenderer p1LightSprite, p2LightSprite;
	//public Sprite empty, wormHole;

	void Start () {
	LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
	rigidbody2D.AddForce (-Vector2.up * powerUpSpeed * Random.Range (1, 5));
	powerUpControlCentreScript = GameObject.FindObjectOfType<PowerUpControlCentre> ();
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
				}
				else if (trigger.gameObject.tag == "Player2"){
					powerUpControlCentreScript.PaddleSizeChange("Extend", 2);
					powerUpControlCentreScript.paddle2Extd = true;
					powerUpControlCentreScript.p2ExtendCount++;
				}
			break;
			case("ShortenPaddle"):
				if (trigger.gameObject.tag == "Player1"){
					powerUpControlCentreScript.PaddleSizeChange("Shrink", 2);
					powerUpControlCentreScript.paddle2Shrink = true;
					powerUpControlCentreScript.p2ShrinkCount++;
				}
				else if (trigger.gameObject.tag == "Player2"){
					powerUpControlCentreScript.PaddleSizeChange("Shrink", 1);
					powerUpControlCentreScript.paddle1Shrink = true;
					powerUpControlCentreScript.p1ShrinkCount++;
				}
			break;
			case("Wormhole"):
				if(trigger.gameObject.tag == "Player1"){
					powerUpControlCentreScript.p1WormholePicked = true;
					//p1LightSprite = LoseTrigger.p1Light.GetComponent<SpriteRenderer>();
					//p1LightSprite.sprite = wormHole;
				}
				else if(trigger.gameObject.tag == "Player2"){
					powerUpControlCentreScript.p2WormholePicked = true;
					//p2LightSprite = LoseTrigger.p2Light.GetComponent<SpriteRenderer>();
					//p2LightSprite.sprite = wormHole;
				}
				break;
			}
		AudioSource.PlayClipAtPoint(powerUpCollect, trigger.gameObject.transform.position);
		}
		Destroy(this.gameObject);
		}
	}





	

