using UnityEngine;
using System.Collections;
using Pathfinding;


public class AstarAITB : MonoBehaviour {
	//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
	//This line should always be present at the top of scripts which use pathfinding
	
	//The point to move to
	public Vector3 targetPosition;
	
	private Seeker seeker;
	//private CharacterController controller;
	
	//The calculated path
	public Path path;
	
	//The AI's speed per second
	public float speed = 40F;

	private float currentPosY;
    private float previousPosY;
    public float velocityY;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	//public GameObject target;

	BallControl ballcontrol;
	LoseTrigger Losetrigger;
	public Vector4 p1Ghost;
	public Vector4 p1;
	public Vector4 p2Ghost;
	public Vector4 p2;
	public bool ballNearby;

	
	public void Start () {
		seeker = GetComponent<Seeker>();
		ballcontrol = FindObjectOfType<BallControl> ();
		Losetrigger = FindObjectOfType<LoseTrigger> ();
		//targetPosition = ballcontrol.transform.position;
        if (this.gameObject.tag == "Player2")
        {
            if (Losetrigger.ballInLeftQuadrant == true)
            {
                StartCoroutine(AITarget("Home"));
            }
            else if (Losetrigger.ballInRightQuadrant == true)
            {
                StartCoroutine(AITarget("Ball"));
            }
        }
        else if (this.gameObject.tag == "Player1")
        {
            if (Losetrigger.ballInLeftQuadrant == true)
            {
                StartCoroutine(AITarget("Ball"));
            }
            else if (Losetrigger.ballInRightQuadrant == true)
            {
                StartCoroutine(AITarget("Home"));
            }
        }
		p1Ghost = new Color (1F, 0F, 0F, 0.3F);
		p1 = new Color (1F, 0F, 0F, 1F);
		p2Ghost = new Color (0F, 0F, 1F, 0.3F);
		p2 = new Color (0F, 0F, 1F, 1F);
		  
	}

	void OnCollisionEnter2D(Collision2D coll){
				if (coll.gameObject.tag == "GameBall") {

                    if (gameObject.tag == "Player1")
                    {
                        MeshRenderer ballColour = coll.transform.GetComponent<MeshRenderer>();
                        ballColour.material.color = Color.red;
                    }
								
                    if (gameObject.tag == "Player2") {
										MeshRenderer ballColour = coll.transform.GetComponent<MeshRenderer> ();
										ballColour.material.color = Color.blue;
								}						
				}
		}

	IEnumerator AITarget(string target){
		if (target == "Ball") {
			if(ballcontrol == null){
				Debug.Log ("Wait!");
				StartCoroutine(AITarget("Home"));
			}
			targetPosition = ballcontrol.transform.position;
			Path pNew = seeker.GetNewPath (transform.position, targetPosition);
			seeker.StartPath (pNew, OnPathComplete);

			}
		if (target == "Home") {
            if (this.gameObject.tag == "Player1") {
                targetPosition = Losetrigger.p1Spawn.transform.position;
                Path pNew = seeker.GetNewPath(transform.position, targetPosition);
                seeker.StartPath(pNew, OnPathComplete);
                if (transform.position == Losetrigger.p1Spawn.transform.position)
                {
                    yield return new WaitForSeconds(1F);

                }
            }
            else if (this.gameObject.tag == "Player2")
            {
                targetPosition = Losetrigger.p2Spawn.transform.position;
                Path pNew = seeker.GetNewPath(transform.position, targetPosition);
                seeker.StartPath(pNew, OnPathComplete);
                if (transform.position == Losetrigger.p2Spawn.transform.position)
                {
                    yield return new WaitForSeconds(1F);

                }
            }
		}

	}
	public void OnPathComplete (Path p) {
		//Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
						path = p;
						//Reset the waypoint counter
						currentWaypoint = 0;
						return;	
		
						}
				}

	void OnTriggerStay2D(Collider2D collider){

		if (collider.gameObject.tag == "GameBallTrigger") {
           Losetrigger.ghostModeP2Active = false;
			ballNearby = true;
				}
		}

	void OnTriggerExit2D(Collider2D collider){

        if (collider.gameObject.tag == "GameBallTrigger")
        {
            Losetrigger.ghostModeP2Active = true;
			ballNearby = false;
		}
	}


	public void Update () {


        if (ballcontrol == null)
        {
            ballcontrol = FindObjectOfType<BallControl>();
            if (ballcontrol == null)
            {
                return;
             }
            else if (ballcontrol != null)
            {
                if (this.gameObject.tag == "Player1")
                {
                    if (Losetrigger.ballInLeftQuadrant == true)
                    {
                        StartCoroutine(AITarget("Ball"));
                    }
                    else if (Losetrigger.ballInRightQuadrant == true)
                    {
                        StartCoroutine(AITarget("Home"));
                    }
                }
              else if (this.gameObject.tag == "Player2")
                {
                    if (Losetrigger.ballInLeftQuadrant == true)
                    {
                        StartCoroutine(AITarget("Home"));
                    }
                    else if (Losetrigger.ballInRightQuadrant == true)
                    {
                        StartCoroutine(AITarget("Ball"));
                    }
                }
             }
        }
        
		if (path == null) {
			//We have no path to move after yet
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
		//Debug.Log ("End Of Path Reached");
			if(ballcontrol.player1LastHit == true){
                if (this.gameObject.tag == "Player1")
                {
                    StartCoroutine(AITarget("Home"));
                }

                else if (this.gameObject.tag == "Player2")
                {
                    StartCoroutine(AITarget("Ball"));
                } 
			}
		else if(ballcontrol.player2LastHit == true){
            if (this.gameObject.tag == "Player1")
                {
                    StartCoroutine(AITarget("Ball"));
                }

                else if (this.gameObject.tag == "Player2")
                {
                    StartCoroutine(AITarget("Home"));
                } 
			}

            if (ballcontrol == null)
            {
                StartCoroutine(AITarget("Home"));
            }
            return;
				}
			

		if (currentWaypoint == path.vectorPath.Count) {
			currentWaypoint++;
			//Debug.Log ("End Of Path Reached");
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.deltaTime;
		transform.Translate (dir);
        velocityY = dir.y;

        if (dir.y >= 0)
        {
            if (this.gameObject.tag == "Player1")
            {
                Losetrigger.ghostModeP1Active = true;
            }
            if (this.gameObject.tag == "Player2")
            {
                Losetrigger.ghostModeP2Active = true;
            }
        }
        else
        {
            if (this.gameObject.tag == "Player1")
            {
                Losetrigger.ghostModeP1Active = false;
            }
            if (this.gameObject.tag == "Player2")
            {
                Losetrigger.ghostModeP2Active = false;
            }
        }
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}

		if (this.gameObject.tag == "Player1") {
            if (ballcontrol.player1LastHit == true)
            {
                //Debug.Log ("Go for the ball!");
                StartCoroutine(AITarget("Home"));
            }
            if (ballcontrol.player2LastHit == true)
            {
                //Debug.Log ("Is this it?");
                StartCoroutine(AITarget("Ball"));
            }
				}
		if (this.gameObject.tag == "Player2") {
			if (ballcontrol.player1LastHit == true) {
				//Debug.Log ("Go for the ball!");
                StartCoroutine(AITarget("Ball"));
				}
			if (ballcontrol.player2LastHit == true) {
				//Debug.Log ("Is this it?");
                StartCoroutine(AITarget("Home"));
				}
			}


		if (this.gameObject.tag == "Player1") {
			if (Losetrigger.ghostModeP1Active == true) {
				//this.GetComponent<Collider2D>().enabled = false;
				//MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
				//playerMesh.material.color = p1Ghost;
				ParticleSystem p1Particles = GetComponent<ParticleSystem> ();
				if (p1Particles.isPlaying == false) {
					p1Particles.Play (true);
					Debug.Log ("Play particles");
				}						
			} else if (Losetrigger.ghostModeP1Active == false) {
				//this.GetComponent<Collider2D>().enabled = true;
				//MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
				//playerMesh.material.color = p1;
				ParticleSystem p1Particles = GetComponent<ParticleSystem> ();
				if (p1Particles.isStopped == false) {
					p1Particles.Stop (true);
				}
			}
		} if (this.gameObject.tag == "Player2") {
			if (Losetrigger.ghostModeP2Active == true) {
				//this.GetComponent<Collider2D>().enabled = false;
				//MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
				//playerMesh.material.color = p2Ghost;
				ParticleSystem p2Particles = GetComponent<ParticleSystem> ();
				if (p2Particles.isPlaying == false) {
					p2Particles.Play (true);
				}
				
			} if (Losetrigger.ghostModeP2Active == false) {
				//this.GetComponent<Collider2D>().enabled = true;
				//MeshRenderer playerMesh = this.transform.GetComponent<MeshRenderer> ();
				//playerMesh.material.color = p2;
				ParticleSystem p2Particles = GetComponent<ParticleSystem> ();
				if (p2Particles.isStopped == false) {
					p2Particles.Stop (true);
				}
				
			}
		}
	}
}



	

