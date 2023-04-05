using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	// from https://www.youtube.com/watch?v=QkisHNmcK7Y

	public float backgroundSize;
	public float parallaxSpeed;

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 10;
	private int leftIndex;
	private int rightIndex;
	private float lastCameraX;


	// Start is called before the first frame update
	void Start()
	{
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;

		// create array the same size as the number of background images
		layers = new Transform[transform.childCount];
		// save all background images in array
		for (int i = 0; i < transform.childCount; i++)
			layers[i] = transform.GetChild(i);

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}

	// Update is called once per frame
	void Update()
	{

		var deltaX = cameraTransform.position.x - lastCameraX;
		transform.position += Vector3.right * (deltaX * parallaxSpeed);
		lastCameraX = cameraTransform.position.x;

		if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
			ScrollLeft();
		if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
			ScrollRight();

	}

	/// <summary> Adjust the backgrounds to the left </summary>
	private void ScrollLeft()
	{
		var lastRight = rightIndex;
		layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);

		leftIndex = rightIndex;
		rightIndex--;

		if (rightIndex < 0)
			rightIndex = layers.Length - 1;
	}

	/// <summary> Adjust the backgrounds to the left </summary>
	private void ScrollRight()
	{
		var lastLeft = leftIndex;
		layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);

		rightIndex = leftIndex;
		leftIndex++;

		if (leftIndex == layers.Length)
			leftIndex = 0;
	}

}
