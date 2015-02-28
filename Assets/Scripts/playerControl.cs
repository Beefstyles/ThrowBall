using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class playerControl : MonoBehaviour {

	public Boundary boundary;
	public float speed = 40F;
	public Texture player1;
	public Texture player2;
	public Texture player1Ghost;
	public Texture player2Ghost;
	private float playerTransp;
	public Vector4 p1Ghost;
	public Vector4 p1;
	public Vector4 p2Ghost;
	public Vector4 p2;
	LoseTrigger LoseTrigger;
	private float paddleVelocityx;
	BallControl BallControl;
	PowerUpControlCentre powerUpControlCentre;
	public float h, h2;
	public float v, v2;
	public float pwrupFireSpeed;

	
	void Start () {
		p1Ghost = new Color (1F, 0F, 0F, 0.3F);
		p1 = new Color (1F, 0F, 0F, 1F);
		p2Ghost = new Color (0F, 0F, 1F, 0.3F);
		p2 = new Color (0F, 0F, 1F, 1F);
		LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
		powerUpControlCentre = GameObject.FindObjectOfType<PowerUpControlCentre> ();
	}

	void OnCollisionEnter2D(Collision2D coll){
			if (coll.gameObject.tag == "GameBall") {
			if (LoseTrigger.ghostModeP1Active == false) {
								if (gameObject.tag == "Player1") {
										MeshRenderer ballColour = coll.transform.GetComponent<MeshRenderer> ();
										ballColour.material.color = Color.red;
		}
				else if (LoseTrigger.ghostModeP2Active == false) {
								} if (gameObject.tag == "Player2") {
										MeshRenderer ballColour = coll.transform.GetComponent<MeshRenderer> ();
										ballColour.material.color = Color.blue;
								}						
						}
				}
		}
		
void Update()
	{
				if (this.gameObject.tag == "Player1") {
						if (LoseTrigger.ghostModeP1Active == true) {
								this.collider2D.enabled = false;
								MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
								playerMesh.material.color = p1Ghost;
								ParticleSystem p1Particles = GetComponent<ParticleSystem> ();
								if (p1Particles.isPlaying == false) {
										p1Particles.Play (true);
										Debug.Log ("Play particles");
								}						
						} else if (LoseTrigger.ghostModeP1Active == false) {
								this.collider2D.enabled = true;
								MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
								playerMesh.material.color = p1;
								ParticleSystem p1Particles = GetComponent<ParticleSystem> ();
								if (p1Particles.isStopped == false) {
										p1Particles.Stop (true);
								}
						}
				} else if (this.gameObject.tag == "Player1") {
						if (LoseTrigger.ghostModeP2Active == true) {
								this.collider2D.enabled = false;
								MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
								playerMesh.material.color = p2Ghost;
								ParticleSystem p2Particles = GetComponent<ParticleSystem> ();
								if (p2Particles.isPlaying == false) {
										p2Particles.Play (true);
								}

						} else if (LoseTrigger.ghostModeP1Active == false) {
								this.collider2D.enabled = true;
								MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
								playerMesh.material.color = p2;
								ParticleSystem p2Particles = GetComponent<ParticleSystem> ();
								if (p2Particles.isStopped == false) {
										p2Particles.Stop (true);
								}

						}
				}
				if (Input.GetButtonDown ("Ghost")) {
						if (gameObject.tag == "Player1") {
								if (LoseTrigger.ghostModeP1Active == false) {
										LoseTrigger.ghostModeP1Active = true;
								} else if (LoseTrigger.ghostModeP1Active == true) {
										LoseTrigger.ghostModeP1Active = false;
								}
						}
				}
				if (Input.GetButtonDown ("Ghost2")) {
						if (gameObject.tag == "Player2") {
								if (LoseTrigger.ghostModeP2Active == false) {
										LoseTrigger.ghostModeP2Active = true;
								} else if (LoseTrigger.ghostModeP2Active == true) {
										LoseTrigger.ghostModeP2Active = false;
								}
						}
				}
				if (Input.GetButtonDown ("FirePwrup")) {
						if (gameObject.tag == "Player1") {
								if (powerUpControlCentre.p1WormholePicked == true){ 
												powerUpControlCentre.WormHoleShot ("Player1");
				}
			
										if (powerUpControlCentre.p1Lifelinepicked == true) {
														powerUpControlCentre.lifeLineSpawn (1);
												}
										}
								}
				
						if (Input.GetButtonDown ("FirePwrup2")) {
								if (gameObject.tag == "Player2") {
										if (powerUpControlCentre.p2WormholePicked == true) {	
												powerUpControlCentre.WormHoleShot ("Player2");
										} 
										if (powerUpControlCentre.p2Lifelinepicked == true) {
														powerUpControlCentre.lifeLineSpawn (2);
												}
										}
			
								}
			}
				

void FixedUpdate ()
	{
		if (gameObject.tag == "Player1") {
						if (!LoseTrigger.paddle1Frozen) {
								h = Input.GetAxis ("Horizontal");
								v = Input.GetAxis ("Vertical");
								if (LoseTrigger.ghostModeP1Active == false) {
										v = 0F;
								}
								Vector2 movement = new Vector2 (h, v);
								rigidbody2D.velocity = movement * speed;
						}
				} else if (gameObject.tag == "Player2") {
						if (!LoseTrigger.paddle2Frozen) {
								h2 = Input.GetAxis ("Horizontal2");
								v2 = Input.GetAxis ("Vertical2");
								if (LoseTrigger.ghostModeP2Active == false) {
										v2 = 0F;
								}
								Vector2 movement = new Vector2 (h2, v2);
								rigidbody2D.velocity = movement * speed;
						}
				}
			if(LoseTrigger.paddle1Frozen){
				LoseTrigger.paddle1FrozenTime -= Time.deltaTime;
				if(LoseTrigger.paddle1FrozenTime <= 0){
					rigidbody2D.velocity = Vector2.zero;
					LoseTrigger.paddle1Frozen = false;
					LoseTrigger.paddle1FrozenTime = LoseTrigger.paddleFrozenTimeSetPoint;
				}
			}
			else if(LoseTrigger.paddle2Frozen){
				LoseTrigger.paddle2FrozenTime -= Time.deltaTime;
				if(LoseTrigger.paddle2FrozenTime <= 0){
					rigidbody2D.velocity = Vector2.zero;
					LoseTrigger.paddle2Frozen = false;
					LoseTrigger.paddle2FrozenTime = LoseTrigger.paddleFrozenTimeSetPoint;
				}
			}
		rigidbody2D.position = new Vector2
		(
			Mathf.Clamp(rigidbody2D.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rigidbody2D.position.y, boundary.yMin, boundary.yMax));
		}
	}


