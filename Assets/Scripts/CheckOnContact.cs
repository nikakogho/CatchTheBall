using UnityEngine;

public class CheckOnContact : MonoBehaviour {

	public GameObject explodedBomb;
	public float explodeDuration = 2;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Bomb"))
		{
			WaveSpawner.gameOver = true;
			Destroy (Instantiate (explodedBomb, other.transform.position, Quaternion.identity), explodeDuration);
			Destroy (other.gameObject);
			Destroy (gameObject);
			return;
		}
		if (!WaveSpawner.gameOver) 
		{
			if (other.CompareTag ("FastBall"))
			{
				WaveSpawner.score += WaveSpawner.fastBallBonus;
			} else
			{
				WaveSpawner.score++;
			}
		}

		Destroy (other.gameObject);
	}
}
