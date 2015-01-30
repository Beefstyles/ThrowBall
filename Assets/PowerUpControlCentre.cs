using UnityEngine;
using System.Collections;

public class PowerUpControlCentre : MonoBehaviour {

	LoseTrigger LoseTrigger;
	public AudioClip powerUpCollect;
	private float extendPaddleTime;
	public Vector3 extendPaddleVector;
	public Vector3 shrinkPaddleVector;
	private float changePaddleSizeTime;
	public bool paddle1Extd, paddle2Extd;
	public bool paddle1Shrink, paddle2Shrink;
	public float paddleSizeChangePowerupTime;
	void Start () {
		LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
		changePaddleSizeTime = paddleSizeChangePowerupTime;
	}

	public void Multiball (int ballNo){
		LoseTrigger.BallSpawn(ballNo);
	}

	public void PaddleSizeChange(string ExtendOrShrink, int playerNo){
		if (ExtendOrShrink == "Extend" && playerNo == 1) {
			LoseTrigger.player1PaddleGO.transform.localScale += extendPaddleVector;
			if (paddle1Extd == true){
			changePaddleSizeTime = paddleSizeChangePowerupTime;
			paddle1Extd = false;
			}
		}
		else if (ExtendOrShrink == "Extend" && playerNo == 2) {
			LoseTrigger.player2PaddleGO.transform.localScale += extendPaddleVector;
			if (paddle2Extd == true){
				changePaddleSizeTime = paddleSizeChangePowerupTime;
				paddle2Extd = false;
			}
			else {
				paddle2Extd = true;
			}
		}
		else if (ExtendOrShrink == "Shrink" && playerNo == 1) {
			LoseTrigger.player1PaddleGO.transform.localScale -= extendPaddleVector;
			if (paddle1Shrink == true){
				changePaddleSizeTime = paddleSizeChangePowerupTime;
				paddle1Shrink = false;
			}
			else {
				paddle1Shrink = true;
			}
		}
		else if (ExtendOrShrink == "Shrink" && playerNo == 2) {
			LoseTrigger.player2PaddleGO.transform.localScale -= extendPaddleVector;
			if (paddle2Shrink == true){
				changePaddleSizeTime = paddleSizeChangePowerupTime;
				paddle2Shrink = false;
			}
			else {
				paddle2Shrink = true;
			}
		}
	}

	
	void Update(){
		
		if (paddle1Extd == true) {
			if (changePaddleSizeTime > 0) {
				changePaddleSizeTime -= Time.deltaTime;
			}
			else if(changePaddleSizeTime <= 0){
				PaddleSizeChange("Shrink", 1);
			}
		}

		if (paddle2Extd == true) {
			if (changePaddleSizeTime > 0) {
				changePaddleSizeTime -= Time.deltaTime;
			}
			else if(changePaddleSizeTime <= 0){
				PaddleSizeChange("Shrink", 2);
			}
		}
		
	}

	
	
}







