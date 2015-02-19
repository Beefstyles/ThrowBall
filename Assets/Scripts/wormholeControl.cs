using UnityEngine;
using System.Collections;

public class wormholeControl : MonoBehaviour {

	WormholeSpawnSpot[] wormholeSpawnSpots;
	public float lifeTime;
	public bool wormholeSpawned;
	private bool localWormholeSpawned;
	
	void Start () {
		wormholeSpawned = false;
		LineRenderer lr = GetComponent<LineRenderer>();
		lr.enabled = false;
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
			lifeTime = 0;
		}
	}

	void Update(){

		if (wormholeSpawned == true) {
			lifeTime -= Time.deltaTime;
		}

		if (lifeTime <= 0) {
			Destroy(this.gameObject);
			}
		}
}
