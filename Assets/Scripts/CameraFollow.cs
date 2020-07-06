using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	Transform cam;
	Vector3 offset;

	void Start ()
	{
		cam = Camera.main.transform;
		offset = cam.position - target.position;
	}

	void LateUpdate ()
	{
		cam.position = target.position + offset;
	}
}
