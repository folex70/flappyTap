using UnityEngine;
using System.Collections;

//usar gameObject aqui faz referencia a esse game object, no caso, o obstaculo que esse script vai ser associado
public class ObstacleUpBehavior : MonoBehaviour {
	
	public Vector2 velocity = new Vector2(-5,0);
	public float range = 10;
	public float valorY = 0f;
	public float valorXTeto = 0f;
	public float valorYTeto = 0f;
	
	
	void OnEnable()
	{
		valorXTeto = GameObject.FindWithTag("spawn_up").transform.position.x;
		valorYTeto = GameObject.FindWithTag("spawn_up").transform.position.y;
		valorY = valorYTeto - range * Random.Range (0.1f, 0.4f);
		
		
		transform.position = new Vector3 (valorXTeto,
		                                  valorY,
		                                  transform.position.z);
		
	}

	void OnBecameInvisible()
	{
		Destroy ();
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
