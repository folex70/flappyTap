using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {
	
	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
	public float yPosRestriction = -1;
	public float xPosRestriction = -1;
	public bool podeVoltar = false;
	
	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;
	float oldxMoveDelta = 0;
	float xMoveDelta = 0;

	float nextTimeToSearch = 0;
	
	// Use this for initialization
	void Start () {
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {

		if (target == null) {
			FindPlayer ();
			return;
		}

		oldxMoveDelta = xMoveDelta;

		// only update lookahead pos if accelerating or changed direction
		xMoveDelta = (target.position - lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
		}
		
		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

		float y = Mathf.Clamp (newPos.y, yPosRestriction, yPosRestriction);
		if (!podeVoltar &&  newPos.x < oldxMoveDelta) {
			newPos = new Vector3 (oldxMoveDelta, y , newPos.z);
		} else {
			float x = Mathf.Clamp (newPos.x, xPosRestriction, Mathf.Infinity);
			newPos = new Vector3 (x, y , newPos.z);
		}

		transform.position = newPos;
		
		lastTargetPosition = target.position;		
	}

	void FindPlayer () {
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null)
				target = searchResult.transform;
			nextTimeToSearch = Time.time + 0.5f;
		}
	}
}
