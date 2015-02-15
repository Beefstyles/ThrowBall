using UnityEngine;
using System.Collections;

public class BG_Movement : MonoBehaviour {

	public GameObject BGSprite;
	public float rotationSpeed;

	void Update () {
		BGSprite.transform.Rotate (Vector3.forward * Time.deltaTime * rotationSpeed, Space.World);
	
		}
	}


