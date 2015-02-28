using UnityEngine;
using System.Collections;

public class lifeLineScript : MonoBehaviour {

	PowerUpControlCentre powerupControlCentre;

	void Start(){
		powerupControlCentre = FindObjectOfType<PowerUpControlCentre> ();
		}
	void Update(){
		powerupControlCentre.lifeLinelifeTime -= Time.deltaTime;

		if (powerupControlCentre.lifeLinelifeTime <= 0) {
			this.gameObject.SetActive(false);
			}
	}
}
