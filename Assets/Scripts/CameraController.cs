using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public float panSpeed = 30f;
	public float scrollBuffer = 15f;
	public float scrollSpeed = 5f;
	public float rotateSpeed = 10f;
	public int scrollFactor = 500;
	public float minY = 10f;
	public float maxY = 80f;
	private float minZ;
	private float maxZ;
	private float minX;
	private float maxX;

	private float halfZoom = 45f;
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManagement.isEndGame)
		{
			this.enabled = false;
			return;
		}

		//Set PAN boundaries
		if (transform.position.y <= maxY && transform.position.y >= halfZoom)
		{
			maxX = 60f;
			minX = 15f;
			maxZ = -75f;
			minZ = -120f;
		}
		else if(transform.position.y <= halfZoom && transform.position.y >= minY)
		{
			maxX = 70f;
			minX = 5f;
			maxZ = -25f;
			minZ = -90f;
		}

		//PAN
		if ((Input.GetKey("w") /*|| Input.mousePosition.y >= Screen.height - scrollBuffer*/) && transform.position.z <= maxZ)
		{
			transform.Translate (Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if ((Input.GetKey("s") /*|| Input.mousePosition.y <= scrollBuffer*/) && transform.position.z >= minZ)
		{
			transform.Translate (Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if ((Input.GetKey("a") /*|| Input.mousePosition.x <= scrollBuffer*/) && transform.position.x >= minX)
		{
			transform.Translate (Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}
		if ((Input.GetKey("d") /*|| Input.mousePosition.x >= Screen.width - scrollBuffer*/) && transform.position.x <= maxX)
		{
			transform.Translate (Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}

		//ROTATE
		/*if ( Input.GetKey("q"))
		{
			transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.Self);
		}
		else if ( Input.GetKey("e"))
		{
			transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
		}*/


		//ZOOM IN/OUT
		float scroll = Input.GetAxis ("Mouse ScrollWheel");

		Vector3 currentPos = transform.position;

		currentPos.y -= scroll * scrollFactor * scrollSpeed * Time.deltaTime;
		currentPos.y = Mathf.Clamp (currentPos.y, minY, maxY);
		transform.position = currentPos;
	}
}
