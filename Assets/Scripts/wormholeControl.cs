using UnityEngine;
using System.Collections;

public class wormholeControl : MonoBehaviour {

	WormholeSpawnSpot[] wormholeSpawnSpots;
	PowerUpControlCentre powerupControlCentre;

	
	void Start () {
		LineRenderer lr = GetComponent<LineRenderer>();
		lr.enabled = false;
		powerupControlCentre = GameObject.FindObjectOfType<PowerUpControlCentre> ();
		wormholeSpawnSpots = GameObject.FindObjectsOfType<WormholeSpawnSpot> ();
	}

	void OnCollisionEnter2D(Collision2D collider){
		Debug.Log (collider.gameObject.tag);
				if (collider.gameObject.tag == "Walls" || collider.gameObject.tag == "GameBall") {
						Destroy (this.gameObject);
				}
		}
	
	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "GameBall") {
		WormholeSpawnSpot thisSpawnSpot = wormholeSpawnSpots [Random.Range (0, wormholeSpawnSpots.Length)];
			Instantiate(trigger.gameObject, thisSpawnSpot.transform.position,Quaternion.identity);
			LineRenderer lr = GetComponent<LineRenderer>();
			lr.enabled = true;
			lr.SetPosition(0, trigger.transform.position);
			lr.SetPosition(1, thisSpawnSpot.transform.position);
			lr.SetWidth(10F,10F);
			lr.SetColors(Color.blue, Color.green);
			Destroy(trigger.gameObject);
			powerupControlCentre.wormholeSpawnlifeTime = 0;
		}
	}

	void Update(){

		if (powerupControlCentre.wormholeSpawned == true) {
			powerupControlCentre.wormholeSpawnlifeTime -= Time.deltaTime;
		}

		if (powerupControlCentre.wormholeSpawnlifeTime <= 0) {
			Destroy(this.gameObject);
			if(powerupControlCentre.p1WormholePicked == true && powerupControlCentre.p1WormholeShot == true){
			powerupControlCentre.p1WormholePicked = false;
			powerupControlCentre.p1WormholeShot = false;
			}
			if(powerupControlCentre.p2WormholePicked == true && powerupControlCentre.p2WormholeShot == true){
				powerupControlCentre.p2WormholePicked = false;
				powerupControlCentre.p2WormholeShot = false;
			}
			}
		}
}
