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
	public bool ghostModeActive;
	private MeshFilter playerMesh;
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
	
	void Start () {

		ghostModeActive = false;
		playerMesh = GetComponent<MeshFilter> ();
		p1Ghost = new Color (1F, 0F, 0F, 0.3F);
		p1 = new Color (1F, 0F, 0F, 1F);
		p2Ghost = new Color (0F, 0F, 1F, 0.3F);
		p2 = new Color (0F, 0F, 1F, 1F);
		LoseTrigger = GameObject.FindObjectOfType<LoseTrigger> ();
		LoseTrigger.p1Light.renderer.material.color = Color.white;
		LoseTrigger.p2Light.renderer.material.color = Color.white;

	}

	void OnCollisionEnter2D(Collision2D coll){
				if (coll.gameObject.tag == "GameBall") {
						if (ghostModeActive == true) {
						
						} else if (ghostModeActive == false) {
								if (gameObject.tag == "Player1") {
										MeshRenderer ballColour = coll.transform.GetComponent<MeshRenderer> ();
										ballColour.material.color = Color.red;
										if (LoseTrigger.singlePlayer == false) {
												LoseTrigger.p1Light.renderer.material.color = Color.white;
												LoseTrigger.p2Light.renderer.material.color = Color.blue;
										} else if (LoseTrigger.singlePlayer == true) {
												LoseTrigger.p1Light.renderer.material.color = Color.red;
												LoseTrigger.p2Light.renderer.material.color = Color.white;
										}
								} else if (gameObject.tag == "Player2") {
										MeshRenderer ballColour = coll.transform.GetComponent<MeshRenderer> ();
										ballColour.material.color = Color.blue;
										if (LoseTrigger.singlePlayer == false) {
												LoseTrigger.p1Light.renderer.material.color = Color.red;
												LoseTrigger.p2Light.renderer.material.color = Color.white;
										}
								}
						
								
						}
				}
		}
		
void Update()
	{
	if (Input.GetButtonDown ("Ghost")) {
						if (gameObject.tag == "Player1") {
								if (ghostModeActive == false) {
										ghostModeActive = true;
										this.collider2D.enabled = false;
										MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
										playerMesh.material.color = p1Ghost;
										ParticleSystem p1Particles = GetComponent<ParticleSystem> ();
										p1Particles.Play (true);
								} else if (ghostModeActive == true) {
										ghostModeActive = false;
										this.collider2D.enabled = true;
										MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
										playerMesh.material.color = p1;
										ParticleSystem p1Particles = GetComponent<ParticleSystem> ();
										p1Particles.Stop (true);
								}
						}
				}
			if (Input.GetButtonDown ("Ghost2")) {
			if (gameObject.tag == "Player2"){
				if (ghostModeActive == false) {
					ghostModeActive = true;
					this.collider2D.enabled = false;
					
					MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
					playerMesh.material.color = p2Ghost;
					ParticleSystem p2Particles = GetComponent<ParticleSystem> ();
					p2Particles.Play(true);
				} 
				else if (ghostModeActive == true) {
					ghostModeActive = false;
					this.collider2D.enabled = true;
					MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
					playerMesh.material.color = p2;
					ParticleSystem p2Particles = GetComponent<ParticleSystem> ();
					p2Particles.Stop(true);
				}
			}
				}

				}

void FixedUpdate ()
	{
		if (gameObject.tag == "Player1") {
			if (!LoseTrigger.paddle1Frozen) {
								float h = Input.GetAxis ("Horizontal");
								float v = Input.GetAxis ("Vertical");
								if (ghostModeActive == false) {
										v = 0F;
								}
								Vector2 movement = new Vector2 (h, v);
								rigidbody2D.velocity = movement * speed;
						}
				} else if (gameObject.tag == "Player2") {
			if (!LoseTrigger.paddle2Frozen) {
								float h2 = Input.GetAxis ("Horizontal2");
								float v2 = Input.GetAxis ("Vertical2");
								if (ghostModeActive == false) {
										v2 = 0F;
								}
								Vector2 movement = new Vector2 (h2, v2);
								rigidbody2D.velocity = movement * speed;
						}
			if(LoseTrigger.paddle1Frozen){
				LoseTrigger.paddle1FrozenTime -= Time.deltaTime;
				if(LoseTrigger.paddle1FrozenTime <= 0){
					rigidbody2D.velocity = -rigidbody2D.velocity;
					LoseTrigger.paddle1Frozen = false;
					LoseTrigger.paddle1FrozenTime = LoseTrigger.paddleFrozenTimeSetPoint;
				}
			}
			else if(LoseTrigger.paddle2Frozen){
				LoseTrigger.paddle2FrozenTime -= Time.deltaTime;
				if(LoseTrigger.paddle2FrozenTime <= 0){
					rigidbody2D.velocity = -rigidbody2D.velocity;
					LoseTrigger.paddle2Frozen = false;
					LoseTrigger.paddle2FrozenTime = LoseTrigger.paddleFrozenTimeSetPoint;
				}
			}

				}

			


		rigidbody2D.position = new Vector2
		(
			Mathf.Clamp(rigidbody2D.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rigidbody2D.position.y, boundary.yMin, boundary.yMax));
		}


	}


