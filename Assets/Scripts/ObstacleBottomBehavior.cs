using UnityEngine;
using System.Collections;

//usar gameObject aqui faz referencia a esse game object, no caso, o obstaculo que esse script vai ser associado
public class ObstacleBottomBehavior : MonoBehaviour {
	
	public Vector2 velocity = new Vector2(-5,0);
	public float range = 10;
	public float valorY = 0f;
	public float valorXBottom = 0f;
	public float valorYBottom = 0f;
	
	
	void OnEnable()
	{
		GetComponent<Rigidbody2D>().velocity =velocity;
        valorXBottom = GameObject.FindWithTag("spawn_bottom").transform.position.x;
        valorYBottom = GameObject.FindWithTag("spawn_bottom").transform.position.y;
		valorY = valorYBottom - range * Random.Range (0.1f, 0.4f);
		
		
		transform.position = new Vector3 (valorXBottom,
		                                  valorY,
		                                  transform.position.z);
		
		Invoke ("Destroy", 7f);
		
	}
	
	void Destroy()
	{
		gameObject.SetActive (false);

	}
	
	void OnDisable()
	{
		CancelInvoke ();
	}
	
}