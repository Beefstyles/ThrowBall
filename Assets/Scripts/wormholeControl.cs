using UnityEngine;
using System.Collections;

public class wormholeControl : MonoBehaviour {

	WormholeSpawnSpot[] wormholeSpawnSpots;

	// Use this for initialization
	void Start () {
		LineRenderer lr = GetComponent<LineRenderer>();
		lr.enabled = false;
		wormholeSpawnSpots = GameObject.FindObjectsOfType<WormholeSpawnSpot> ();
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
		}
	}

}
