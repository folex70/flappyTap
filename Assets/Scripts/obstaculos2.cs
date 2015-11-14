using UnityEngine;
using System.Collections;

public class obstaculos2 : MonoBehaviour {

	public Vector2 velocity = new Vector2(-4,0);
	public float range = 4;
	public Vector2 jumpForce = new Vector2(0,300);
	public float count = 0.1f;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity =velocity;

	}                
	
	// Update is called once per frame
	void Update () {
		//faz os obstaculos se moverem verticalmente para aumentar a dificuldade
		if (count > 1f) {
			count=0.1f;
		} else {
			count = count + 0.009f;
		}
		transform.position = new Vector3 (transform.position.x,count,transform.position.z);

		Destroy (gameObject, 7f);


	}
}
