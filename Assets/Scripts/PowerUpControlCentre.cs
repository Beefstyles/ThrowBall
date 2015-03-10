using UnityEngine;
using System.Collections;

public class PowerUpControlCentre : MonoBehaviour {

	LoseTrigger LoseTrigger;
	public AudioClip powerUpCollect;
	private float extendPaddleTime;
	public Vector3 paddleSizeChangeVector;
	public float changePaddle1ExtdTime, changePaddle2ExtdTime;
	public float changePaddle1ShrinkTime, changePaddle2ShrinkTime;
	public bool paddle1Extd, paddle2Extd;
	public bool paddle1Shrink, paddle2Shrink;
	public int p1ExtendCount, p2ExtendCount;
	public int p1ShrinkCount, p2ShrinkCount;
	public float paddleSizeChangePowerupTime;
	public GameObject wormholeObject;
	public GameObject wormholeGOP1, wormholeGOP2;
	public float pwrupFireSpeed;
	public bool p1WormholePicked, p2WormholePicked;
	public bool p1WormholeShot, p2WormholeShot;
	public bool wormholeSpawned;
	public bool p1Lifelinepicked, p2Lifelinepicked;
	public GameObject lifelineBarrier;
	public float lifeLinelifeTime;
	public float wormholeSpawnlifeTime;


	void Start () {
		LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
		p1ExtendCount = 0;
		p2ExtendCount = 0;
		p1ShrinkCount = 5;
		p2ShrinkCount = 0;
		p1WormholeShot = false;
		p2WormholeShot = false;
	}

	public void Multiball (int ballNo){
		LoseTrigger.BallSpawn(ballNo);
	}

	public void lifeLineSpawn(int playerNo){
		lifeLinelifeTime = 5F;
		if (playerNo == 1) {
			lifelineBarrier.SetActive (true);
			p1Lifelinepicked = false;
				}
		else if (playerNo == 2) {
			lifelineBarrier.SetActive (true);
			p2Lifelinepicked = false;

		}
		LoseTrigger.p1LightSprite.sprite = LoseTrigger.empty;
	}

	public void PaddleSizeChange(string ExtendOrShrink, int playerNo){
		if (ExtendOrShrink == "Extend" && playerNo == 1) {
			LoseTrigger.player1PaddleGO.transform.localScale += paddleSizeChangeVector;
			changePaddle1ExtdTime = paddleSizeChangePowerupTime;
		}
		else if (ExtendOrShrink == "Extend" && playerNo == 2) {
			LoseTrigger.player2PaddleGO.transform.localScale += paddleSizeChangeVector;
			changePaddle2ExtdTime = paddleSizeChangePowerupTime;
		}
		else if (ExtendOrShrink == "Shrink" && playerNo == 1) {
			LoseTrigger.player1PaddleGO.transform.localScale -= paddleSizeChangeVector;
			changePaddle1ShrinkTime = paddleSizeChangePowerupTime;
		}
		else if (ExtendOrShrink == "Shrink" && playerNo == 2) {
			LoseTrigger.player2PaddleGO.transform.localScale -= paddleSizeChangeVector;
			changePaddle2ShrinkTime = paddleSizeChangePowerupTime;
		}
	}

	public void WormHoleShot(string PlayerTag){
		if (PlayerTag == "Player1") {
						if (!p1WormholeShot) {
								wormholeGOP1 = Instantiate (wormholeObject, LoseTrigger.player1PaddleGO.transform.position, Quaternion.identity) as GameObject;
								wormholeGOP1.GetComponent<Rigidbody2D>().AddForce (Vector3.up * pwrupFireSpeed);
								p1WormholeShot = true;
								LoseTrigger.p1LightSprite.sprite = LoseTrigger.empty;
						} else if (p1WormholeShot) {
				wormholeGOP1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				wormholeGOP1.GetComponent<Collider2D>().isTrigger = true;
						}
				}
		if (PlayerTag == "Player2") {
		if (!p2WormholeShot) {
			
			wormholeGOP2 = Instantiate (wormholeObject, LoseTrigger.player2PaddleGO.transform.position, Quaternion.identity) as GameObject;
			wormholeGOP2.GetComponent<Rigidbody2D>().AddForce (Vector3.up * pwrupFireSpeed);
			p2WormholeShot = true;
			LoseTrigger.p1LightSprite.sprite = LoseTrigger.empty;
		}
		else if (p2WormholeShot) {
			wormholeGOP2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			wormholeGOP2.GetComponent<Collider2D>().isTrigger = true;
			p2WormholeShot = false;
		}
		}
		wormholeSpawnlifeTime = 5F;
		wormholeSpawned = true;
		}
	void Update()
	{
			if (paddle1Extd == true) {
			if (changePaddle1ExtdTime > 0) {
				changePaddle1ExtdTime -= Time.deltaTime;
			}
			else if(changePaddle1ExtdTime <= 0){
				p1ExtendCount--;
				if(p1ExtendCount == 0){
				PaddleSizeChange("Shrink", 1);
				paddle1Extd = false;
				LoseTrigger.p1LightSprite.sprite = LoseTrigger.empty;
				}
				else if(p1ExtendCount != 0){
				PaddleSizeChange("Shrink", 1);
			}
			}
		}

		if (paddle2Extd == true) {
						if (changePaddle2ExtdTime > 0) {
								changePaddle2ExtdTime -= Time.deltaTime;
			} 			else if(changePaddle1ExtdTime <= 0){
						p2ExtendCount--;
						if(p2ExtendCount == 0){
						PaddleSizeChange("Shrink", 2);
						paddle2Extd = false;
						LoseTrigger.p2LightSprite.sprite = LoseTrigger.empty;
				}
				else if(p1ExtendCount != 0){
					PaddleSizeChange("Shrink", 2);
					}
			}
				}

			if (paddle1Shrink == true) {
				if (changePaddle1ShrinkTime > 0) {
					changePaddle1ShrinkTime -= Time.deltaTime;
				}
				else if(changePaddle1ShrinkTime <= 0){
					p1ShrinkCount--;
				if(p1ShrinkCount == 0){
					PaddleSizeChange("Extend", 1);
					paddle1Shrink = false;
					LoseTrigger.p1LightSprite.sprite = LoseTrigger.empty;
				}
				else if(p1ShrinkCount != 0){
					PaddleSizeChange("Extend", 1);
				}
			}
		}
			
			if (paddle2Shrink == true) {
				if (changePaddle2ShrinkTime > 0) {
					changePaddle2ShrinkTime -= Time.deltaTime;
				}
				else if(changePaddle2ShrinkTime <= 0){
					p2ShrinkCount--;
				if(p2ShrinkCount == 0){
					PaddleSizeChange("Extend", 2);
					paddle2Shrink = false;
					LoseTrigger.p2LightSprite.sprite = LoseTrigger.empty;
				}
				else if(p2ShrinkCount != 0){
					PaddleSizeChange("Extend", 2);
				}
			}
		}	
	}
}








