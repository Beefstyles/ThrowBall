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

	void Start () {
		LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
		p1ExtendCount = 0;
		p2ExtendCount = 0;
		p1ShrinkCount = 0;
		p2ShrinkCount = 0;
	}

	public void Multiball (int ballNo){
		LoseTrigger.BallSpawn(ballNo);
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

	
	void Update(){
		
		if (paddle1Extd == true) {
			if (changePaddle1ExtdTime > 0) {
				changePaddle1ExtdTime -= Time.deltaTime;
			}
			else if(changePaddle1ExtdTime <= 0){
				p1ExtendCount--;
				if(p1ExtendCount == 0){
				PaddleSizeChange("Shrink", 1);
				paddle1Extd = false;
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
						if(p1ExtendCount == 0){
						PaddleSizeChange("Shrink", 2);
						paddle1Extd = false;
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
				}
				else if(p2ShrinkCount != 0){
					PaddleSizeChange("Extend", 2);
				}
			}
		}	
	}
}








