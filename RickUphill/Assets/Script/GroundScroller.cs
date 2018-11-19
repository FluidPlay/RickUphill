using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour {

    Vector2 _startPos;
	public Vector2 DestinationPos;
	public float ScrollSpeed;

	void Start ()
	{
		_startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector2.MoveTowards(transform.position,
			DestinationPos, ScrollSpeed);
		
		if (Mathf.Approximately(Vector2.Distance( transform.position, DestinationPos ), 0f))
			ResetPosition();
	}

	void ResetPosition()
	{
		transform.position = _startPos;
	}
}
