using UnityEngine;
using System.Collections;

//usar gameObject aqui faz referencia a esse game object, no caso, o obstaculo que esse script vai ser associado
public class ObstacleBottomBehavior : MonoBehaviour {

	public float range = 10;
	public float valorY = 0f;
	public float valorX = 0f;
	public float valorXBottom = 0f;
	public float valorYBottom = 0f;
	
	
	void OnEnable()
	{
        valorXBottom = GameObject.FindWithTag("spawn_bottom").transform.position.x;
        valorYBottom = GameObject.FindWithTag("spawn_bottom").transform.position.y;
		valorY = valorYBottom - range * Random.Range (0.1f, 0.4f);
		valorX = valorXBottom - range * Random.Range (0.1f, 0.4f);
		
		
		transform.position = new Vector3 ( valorX,
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