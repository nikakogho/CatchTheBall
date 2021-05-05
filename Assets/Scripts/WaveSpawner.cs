using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour {

	public Transform hat;
	public string selectorName = "levelSelector";
	public GameObject GameOverUI, startUI;

	public Text timeText, scoreText, highScoreText;

	public static int score = 0;
	public static bool gameOver = false;
	public GameObject bowlingBall, bomb;
	public float normalBallChance;
	public float timeLeft;

	private float maxWidth = 0;

	private bool justEnded = true;
	private bool started = false;

	[Header("Spawn Time")]
	public float min;
	public float max;

	[Header("Fast Ball")]
	public GameObject fastBall;
	public float fastBallChance;
	public int FastBallBonus;
	public static int fastBallBonus;

	[Header("Fast Bomb")]
	public GameObject fastBomb;
	public float fastBombChance;

	private float countdown = 0;

	void Start()
	{
		score = 0;
		gameOver = false;
		fastBallBonus = FastBallBonus;
		normalBallChance = LevelSelector.chanceNormal;
		fastBallChance = LevelSelector.chanceFastBall;
		fastBombChance = LevelSelector.chanceFastBomb;
		timeLeft = LevelSelector.seconds;
		min = LevelSelector.minTime;
		max = LevelSelector.maxTime;
	}

	void Update()
	{
		if (maxWidth == 0 && hatController.maxWidth > 0) 
		{
			maxWidth = hatController.maxWidth;
		}
		if (!started)
			return;
		if (Input.GetKeyDown ("r")) 
		{
			Restart ();
		}
		timeLeft -= Time.deltaTime;

		timeLeft = Mathf.Clamp (timeLeft, 0, Mathf.Infinity);

		if (timeLeft == 0)
			gameOver = true;

		if (!gameOver) 
		{
			countdown -= Time.deltaTime;

			if (countdown <= 0) 
			{
				Spawn ();
			}
		}

		if (gameOver && justEnded) 
		{
			Lose ();
		}
		string time = string.Format ("{00:00.00}", timeLeft);
		if (timeLeft > 60) 
		{
			time = "1:" + string.Format ("{00:00.00}", timeLeft - 60);
		}
		timeText.text = "Time Left :\n" + time;
		scoreText.text = "Score :\n" + score;
	}

	void Spawn()
	{
		countdown = Random.Range (min, max);

		GameObject spawn;

		float chance = Random.Range (0f, 1f);
		if (chance <= fastBombChance) 
		{
			spawn = fastBomb;
		} else if (chance <= fastBallChance) 
		{
			spawn = fastBall;
		} else if (chance <= normalBallChance) 
		{
			GameObject[] stuff = { bowlingBall, bomb };

			spawn = stuff [Random.Range (0, 2)];
		} else 
		{
			GameObject[] stuff = { fastBall, fastBomb };

			spawn = stuff [Random.Range (0, 2)];
		}
		float xSpawn = Random.Range (-maxWidth, maxWidth);
		if (spawn == fastBomb) 
		{
			xSpawn = hat.position.x;
		}
		Instantiate (spawn, new Vector3 (xSpawn, transform.position.y, 0), Quaternion.identity);
	}

	void Lose()
	{
		GameOverUI.SetActive (true);
		int Score = PlayerPrefs.GetInt ("HighScore" + LevelSelector.seconds + LevelSelector.chanceFastBall + LevelSelector.maxTime, 0);
		if (score > Score)
		{
			PlayerPrefs.SetInt ("HighScore" + LevelSelector.seconds + LevelSelector.chanceFastBall + LevelSelector.maxTime, score);
			Score = score;
		}
		highScoreText.text = "HighScore :\n" + Score;
		justEnded = false;
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void StartGame()
	{
		startUI.SetActive (false);
		started = true;
	}

	public void Reset()
	{
		PlayerPrefs.DeleteKey("HighScore" + LevelSelector.seconds + LevelSelector.chanceFastBall + LevelSelector.maxTime);
		highScoreText.text = "HighScore :\n??";
	}

	public void Menu()
	{
		SceneManager.LoadScene (selectorName);
	}
}
