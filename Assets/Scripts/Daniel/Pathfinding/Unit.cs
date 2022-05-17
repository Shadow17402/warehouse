using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour
{

	public Camera cam;
	public Vector3 target;
	public float speed = 1;
	public float rotationSpeed = 1;
	public bool moving = false;
	Vector3[] path;
	int targetIndex;

	void Start()
	{
		
	}

	void Update()
    {
		if (Input.GetMouseButtonDown(0) && !moving)
        {
          Ray ray = cam.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

          if(Physics.Raycast(ray, out hit))
          {
				newPath(hit.point);
          }
        }
	}

	public void newPath(Vector3 target)
    {
		this.target = target;
		Debug.Log(this.target.x + " " + this.target.y + " " + this.target.z);
		moving = true;
		InvokeRepeating("RequestPath", 0, 2);
	}

	private void RequestPath()
    {
		PathRequestManager.RequestPath(transform.position, target, OnPathFound);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			Debug.Log(newPath.Length);
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		targetIndex = 0;
		while (true)
		{
			currentWaypoint.y = 0.53f;
			if (V3Equal(currentWaypoint, transform.position))
			{
				targetIndex++;
				if (targetIndex == path.Length)
				{
					CancelInvoke("RequestPath");
					StopCoroutine("FollowPath");
					moving = false;
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
			Vector3 targetDir = currentWaypoint - this.transform.position;
			float step = this.rotationSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;

		}
	}
	public bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < 0.1;
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
}