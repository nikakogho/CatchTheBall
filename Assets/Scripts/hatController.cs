using UnityEngine;

public class hatController : MonoBehaviour {

	private Rigidbody2D rb;
	private Renderer rend;
	public static float maxWidth = 0;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();
	}

    float MaxWidth()
	{
		Vector3 topCorner = new Vector3 (Screen.width, Screen.height, 0);
		Vector3 targetWidth = Camera.main.ScreenToWorldPoint (topCorner);

		float hatWidth = rend.bounds.extents.x;

		return targetWidth.x - hatWidth;
	}


	void Update()
	{
		maxWidth = MaxWidth ();
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 targetPosition = new Vector3 (mousePosition.x, transform.position.y, transform.position.z);

		float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);

        targetPosition.x = targetWidth;

		rb.MovePosition (targetPosition);
	}
}
