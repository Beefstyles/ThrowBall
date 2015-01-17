using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseTrigger : MonoBehaviour {

	public Text gameOverText;
	public Text restartingInText;
	public Text countDownTimer;
	public int ballCount;
	public Text player1_CountText;
	public Text player2_CountText;
	public Text playerWinText;
	public Text playerWinCounterText;
	public int player1Score;
	public int player2Score;
	BallControl BallControl;
	SpawnSpot[] spawnSpots;
	public Transform gameBall;
	public GameObject p1Light;
	public GameObject p2Light;
	public bool singlePlayer;
	public Transform p1Spawn;
	public Transform p2Spawn;
	public GameObject player1Paddle;
	public GameObject player2Paddle;
	public bool player1Wins;
	public bool player2Wins;
	PowerUpSpawn[] powerUpSpawnSpots;
	public GameObject powerUpMultiBall;
	public float powerUpSpawnCooldown;
	public int powerUpSpawnRNG;
	public AudioClip gameOverSound;
	public bool paddle1Frozen;
	public bool paddle2Frozen;
	public float paddle1FrozenTime;
	public float paddle2FrozenTime;
	public float paddleFrozenTimeSetPoint;


	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		BallControl = GameObject.FindObjectOfType<BallControl> ();
		powerUpSpawnSpots = GameObject.FindObjectsOfType<PowerUpSpawn> ();
		gameOverText.text = "";
		restartingInText.text = "";
		countDownTimer.text = "";
		playerWinText.text = "";
		playerWinCounterText.text = "";
		ballCount = 0;
		player1Score = 0;
		player2Score = 0;
		powerUpSpawnCooldown = 5;
		player1_CountText.text = player1Score.ToString();
		player2_CountText.text = player2Score.ToString();
		BallSpawn (1);
		singlePlayer = false;
		if (singlePlayer) {
					Instantiate (player1Paddle, p1Spawn.transform.position, Quaternion.identity);	
				} else if (!singlePlayer) {
			Instantiate(player1Paddle, p1Spawn.transform.position, Quaternion.identity);
			Instantiate(player2Paddle, p2Spawn.transform.position, Quaternion.identity);
		}
		paddle1FrozenTime = paddleFrozenTimeSetPoint;
		paddle2FrozenTime = paddleFrozenTimeSetPoint;
		paddle1Frozen = false;
		paddle2Frozen = false;
						
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
			StartCoroutine (CountdownToRestart ());
			}
		}
		Destroy(collider.gameObject);
	}

IEnumerator CountdownToRestart() {
			if (player1Wins) {
				playerWinText.text = "Player 1 Wins With:";
				playerWinCounterText.text = player1Score.ToString () + " Points";
			}
			else if (player2Wins) {
			playerWinText.text = "Player 2 Wins With:";
			playerWinCounterText.text = player2Score.ToString () + " Points";
		}
		else if (!player1Wins && !player2Wins) {
			playerWinText.text = "Both Lose";
		}
		AudioSource.PlayClipAtPoint (gameOverSound, Camera.main.transform.position);
		gameOverText.text = "Game Over";
		restartingInText.text = "Restarting in:";
		for (int i = 3; i >= 1; i--) {
		countDownTimer.text = i.ToString();
		yield return new WaitForSeconds (1);
		}
		Application.LoadLevel ("mainScene");
	}

void Update(){
		powerUpSpawnCooldown -= Time.deltaTime;
		player1_CountText.text = player1Score.ToString();
		player2_CountText.text = player2Score.ToString();

		if (powerUpSpawnCooldown <= 0) {
					PowerUpSpawn ();
				}
	}


public void BallSpawn(int numberReq){
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		for (int i = numberReq; i > 0; i--){
		Instantiate(gameBall, mySpawnSpot.transform.position,Quaternion.identity);
	}
		ballCount = ballCount + numberReq;
}

void PowerUpSpawn(){
		powerUpSpawnCooldown = 5;
		powerUpSpawnRNG = Random.Range (0, 10);
		PowerUpSpawn thisPowerUpSpawnSpot = powerUpSpawnSpots [Random.Range (0, powerUpSpawnSpots.Length)];
		if (powerUpSpawnRNG == 1) {
			Instantiate (powerUpMultiBall, thisPowerUpSpawnSpot.transform.position, Quaternion.identity);
		}
		powerUpSpawnRNG = Random.Range (0, 10);
	}
}
