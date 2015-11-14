using UnityEngine;
using System.Collections;

//usar gameObject aqui faz referencia a esse game object, no caso, o obstaculo que esse script vai ser associado
public class obstaculos : MonoBehaviour {

	public Vector2 velocity = new Vector2(-4,0);
	public float range = 4;

	// Use this for initialization
	void Start () {
	
		GetComponent<Rigidbody2D>().velocity =velocity;
		transform.position = new Vector3 (transform.position.x,
		                                  transform.position.y - range * Random.Range(0.1f,0.3f),
		                                  transform.position.z);
	}                
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, 7f);
	}


}
