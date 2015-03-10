using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseTrigger : MonoBehaviour {

	//Game Text & screens
	public Text gameOverText;
	public Text restartingInText;
	public Text countDownTimer;
	public int ballCount;
	public Text player1_CountText;
	public Text player2_CountText;
	public Text playerWinText;
	public Text playerPointWinCounterText;
	public Text playerOneWinCounterText;
	public Text playerTwoWinCounterText;
	public Text colonText;
	public int player1Score;
	public int player2Score;
	public GameObject EndOfRoundsScreen;
	public Text PlayerFinalWinText;
	public Text P1WinFinal;
	public Text P2WinFinal;
	public Text WinsText;
	public GameObject p1Light;
	public GameObject p2Light;
	public SpriteRenderer p1LightSprite, p2LightSprite;
	public Sprite empty, wormHole, multiBall, shrink, extend, lifeLine;
	//End Game Text

	//Necessary Scripts
	BallControl BallControl;
	RoundCounter RoundCounter;
	//End Necessary Scripts

	//Spawnspots
	SpawnSpot[] spawnSpots;
	PowerUpSpawn[] powerUpSpawnSpots;
	//End Spawnspots

	//Player Gameobjects & Transforms
	public GameObject player1Paddle;
	public GameObject player2Paddle;
	public GameObject player1PaddleAI;
    public GameObject player2PaddleAI;
	public GameObject player1PaddleGO; //Note only exists as a clone of the p1 paddle gameobject for extend/shrink
	public GameObject player2PaddleGO; //Note only exists as a clone of the p1 paddle gameobject for extend/shrink
	public Transform p1Spawn;
	public Transform p2Spawn;
	//End Player Gameobjects

	//Powerup prefabs
	public GameObject powerUpMultiBall;
	public GameObject powerUpExtendPaddle;
	public GameObject powerUpShrinkPaddle;
	public GameObject wormholePwrup;
	public GameObject barrierPwrup;
	//End Powerup prefabs

	//Ball info
	public Transform gameBall;
	//End ball info

	//Gameplay bools
	public bool singlePlayer;
    public bool AIMatch;
	public bool player1Wins;
	public bool player2Wins;
	public bool paddle1Frozen;
	public bool paddle2Frozen;
	public bool ghostModeP1Active;
	public bool ghostModeP2Active;
    public bool ballInLeftQuadrant;
    public bool ballInRightQuadrant;
   
	//End Gameplay bools

	//Gameplay floats & ints
	public float powerUpSpawnCooldown;
	public float paddle1FrozenTime;
	public float paddle2FrozenTime;
	public float paddleFrozenTimeSetPoint;
	public int powerUpSpawnRNG;
	//End floats and ints

	//Audioclips
	public AudioClip gameOverSound;
	//End audioclips


	void Start () {
		EndOfRoundsScreen.gameObject.SetActive (false);
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		powerUpSpawnSpots = GameObject.FindObjectsOfType<PowerUpSpawn> ();
		BallControl = GameObject.FindObjectOfType<BallControl> ();
		RoundCounter = GameObject.FindObjectOfType<RoundCounter>();
		gameOverText.text = "";
		restartingInText.text = "";
		countDownTimer.text = "";
		playerWinText.text = "";
		playerPointWinCounterText.text = "";
		playerOneWinCounterText.text = "";
		playerTwoWinCounterText.text = "";
		colonText.text = "";
		PlayerFinalWinText.text = "";
		P1WinFinal.text = "";
		P2WinFinal.text = "";
		WinsText.text = "";
		ballCount = 0;
		player1Score = 0;
		player2Score = 0;
		powerUpSpawnCooldown = 10;
		player1_CountText.text = player1Score.ToString();
		player2_CountText.text = player2Score.ToString();
		BallSpawn (1);
		//singlePlayer = true;
        AIMatch = true;
		if (singlePlayer) {
					player1PaddleGO = Instantiate(player1Paddle, p1Spawn.transform.position, Quaternion.identity) as GameObject;
					player2PaddleGO = Instantiate(player2PaddleAI, p2Spawn.transform.position, Quaternion.identity) as GameObject;
        }
        else if (!singlePlayer && !AIMatch)
        {
			player1PaddleGO = Instantiate(player1Paddle, p1Spawn.transform.position, Quaternion.identity) as GameObject;
			player2PaddleGO = Instantiate(player2Paddle, p2Spawn.transform.position, Quaternion.identity) as GameObject;
		}

        else if (!singlePlayer && AIMatch)
        {
            player1PaddleGO = Instantiate(player1PaddleAI, p1Spawn.transform.position, Quaternion.identity) as GameObject;
            player2PaddleGO = Instantiate(player2PaddleAI, p2Spawn.transform.position, Quaternion.identity) as GameObject;
        }
		paddle1FrozenTime = paddleFrozenTimeSetPoint;
		paddle2FrozenTime = paddleFrozenTimeSetPoint;
		paddle1Frozen = false;
		paddle2Frozen = false;
		ghostModeP1Active = false;
		ghostModeP2Active = false;

	}
	
void OnTriggerEnter2D(Collider2D collider){
	if (collider.tag == "GameBall") {
		ballCount--;
		if (ballCount == 0) {
				BallControl = GameObject.FindObjectOfType<BallControl> ();
				if(BallControl.player1LastHit == true){
					player1Wins = true;
					player2Wins = false;
				}
				else if(BallControl.player2LastHit == true){
					player1Wins = false;
					player2Wins = true;
				}
				else if(BallControl.player1LastHit == false && BallControl.player1LastHit == false){
					player1Wins = false;
					player2Wins = false;
				}
			Destroy(collider.gameObject);
			StartCoroutine (CountdownToRestart ());
			}
		}
		Destroy(collider.gameObject);
	}

IEnumerator CountdownToRestart() {
				if (player1Wins) {
						playerWinText.text = "Player 1 Wins With:";
						playerPointWinCounterText.text = player1Score.ToString () + " Points";
						RoundCounter.p1WinCounter++;
				} else if (player2Wins) {
						playerWinText.text = "Player 2 Wins With:";
						playerPointWinCounterText.text = player2Score.ToString () + " Points";
						RoundCounter.p2WinCounter++;
				} else if (!player1Wins && !player2Wins) {
						playerWinText.text = "Both Lose";
				}
				RoundCounter.roundNoCounter++;
				AudioSource.PlayClipAtPoint (gameOverSound, Camera.main.transform.position);
				if (RoundCounter.roundNoCounter < RoundCounter.roundNoMax) {
						gameOverText.text = "Round " + RoundCounter.roundNoCounter.ToString () + " Over";
						playerOneWinCounterText.text = RoundCounter.p1WinCounter.ToString ();
						playerTwoWinCounterText.text = RoundCounter.p2WinCounter.ToString ();
						colonText.text = ":";
						restartingInText.text = "Next Round Starting in:";
						for (int i = 3; i >= 1; i--) {
								countDownTimer.text = i.ToString ();
								yield return new WaitForSeconds (1);
						}
						Application.LoadLevel ("mainScene");
				} else if (RoundCounter.roundNoCounter == RoundCounter.roundNoMax) {
				EndOfRoundsScreen.gameObject.SetActive (true);
			if(RoundCounter.p1WinCounter > RoundCounter.p2WinCounter){
				PlayerFinalWinText.text = "Player 1 Wins";
				}
			else if(RoundCounter.p1WinCounter < RoundCounter.p2WinCounter){
				PlayerFinalWinText.text = "Player 2 Wins";
			}
			else if(RoundCounter.p1WinCounter == RoundCounter.p2WinCounter){
				PlayerFinalWinText.text = "Draw";
			}
			WinsText.text = "Wins";
					P1WinFinal.text = RoundCounter.p1WinCounter.ToString();
					P2WinFinal.text = RoundCounter.p2WinCounter.ToString();
				}	
		}

void Update(){
		powerUpSpawnCooldown -= Time.deltaTime;
		player1_CountText.text = player1Score.ToString();
		player2_CountText.text = player2Score.ToString();
		if (player1Score <= 0) {
			player1Score = 0;	
		}
		if (player2Score <= 0) {
			player2Score = 0;	
		}
		if (powerUpSpawnCooldown <= 0) {
			PowerUpSpawn ();
		}
	
	}


public void BallSpawn(int numberReq){
		for (int i = numberReq; i > 0; i--){
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		Instantiate(gameBall, mySpawnSpot.transform.position,Quaternion.identity);
        if (mySpawnSpot.gameObject.tag == "LeftQuadrant")
        {
            Debug.Log("Ball in Left");
           ballInLeftQuadrant = true;
        }
        else if (mySpawnSpot.gameObject.tag == "RightQuadrant")
        {
            Debug.Log("Ball in Right");
           ballInRightQuadrant = true;
        }
        
	}
        
		ballCount = ballCount + numberReq;
}

void PowerUpSpawn(){
		powerUpSpawnCooldown = 5;
		powerUpSpawnRNG = Random.Range (0, 10);
		PowerUpSpawn thisPowerUpSpawnSpot = powerUpSpawnSpots [Random.Range (0, powerUpSpawnSpots.Length)];
		switch (powerUpSpawnRNG) {
		case(0):
			Instantiate (powerUpMultiBall, thisPowerUpSpawnSpot.transform.position, Quaternion.identity);
			break;
		case(1):
			Instantiate (powerUpExtendPaddle, thisPowerUpSpawnSpot.transform.position, Quaternion.identity);
			break;
		case(2):
			Instantiate (powerUpShrinkPaddle, thisPowerUpSpawnSpot.transform.position, Quaternion.identity);
			break;
		case(3):
			Instantiate (wormholePwrup, thisPowerUpSpawnSpot.transform.position, Quaternion.identity);
			break;
		case(4):
			Instantiate (barrierPwrup, thisPowerUpSpawnSpot.transform.position, Quaternion.identity);
			break;
	}
		powerUpSpawnRNG = Random.Range (0, 10);
	}
}
