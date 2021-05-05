using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

	public string LevelName = "main", chooseTimeText = "Select Time", chooseModeText = "Select Mode", chooseSpeedText = "Select Speed";
	public Text text;
	public GameObject timeUI, ItemUI, SpeedUI;
	public static float seconds = 0, chanceNormal = 0, chanceFastBall = 0, chanceFastBomb = 0, minTime = 0, maxTime = 0;
	private bool choosed = false, ended = false;

	void Start()
	{
		text.text = chooseTimeText;
	}

	public void Seconds(float secs)
	{
		seconds = secs;
		timeUI.SetActive (false);
		ItemUI.SetActive (true);
		text.text = chooseModeText;
	}

	public void NormalMode()
	{
		chanceNormal = 1;
		chanceFastBall = 0;
		chanceFastBomb = 0;
		SpeedTurn ();
	}

	public void All()
	{
		chanceNormal = 1;
		chanceFastBall = 0.1f;
		chanceFastBomb = 0.025f;
		SpeedTurn ();
	}

	public void FastMode()
	{
		chanceNormal = 0;
		chanceFastBall = 1;
		chanceFastBomb = 0.5f;
		SpeedTurn ();
	}

	void SpeedTurn()
	{
		SpeedUI.SetActive (true);
		ItemUI.SetActive (false);
		text.text = chooseSpeedText;
	}

	public void Slow()
	{
		minTime = 2;
		maxTime = 5;
		Chosen ();
	}

	public void Normal()
	{
		minTime = 1;
		maxTime = 3;
		Chosen ();
	}

	public void Fast()
	{
		minTime = 0.75f;
		maxTime = 2;
		Chosen ();
	}

	void Chosen()
	{
		choosed = true;
	}

	void Update()
	{
		if (choosed && !ended) 
		{
			ended = true;
			SceneManager.LoadScene (LevelName);
		}
	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void ResetAll()
	{
		PlayerPrefs.DeleteAll ();
	}
}
