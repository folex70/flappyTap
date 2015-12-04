using UnityEngine;
using System.Collections;

public class BugMovement : MonoBehaviour {


	public float movementSpeedMax;
	public float rotationTimeMax;
	public float directionTimeMax;
	public float rotationTimeMin;
	public float directionTimeMin;
	public Vector3 direcao;
	private bool isEsquerda = false;
	private bool isCima = false;

	public Vector2 velocity = new Vector2(-5,0);
	public float range = 10;
	private float valorX = 0f;
	private float valorY = 0f;
	
	
	void OnEnable()
	{
		valorX = GameObject.FindWithTag("spawn_middle").transform.position.x;
		valorY = GameObject.FindWithTag("spawn_middle").transform.position.y;
		
		
		transform.position = new Vector3 (valorX,
		                                  valorY,
		                                  transform.position.z);
	}

	
	void Start() {

		ChangeRotationRandom();
		ChangeDirectionRandom();
	}

	void ChangeRotationRandom() {
		if(Random.value > 0.5f)  {
			ChangeRotation();
		}
		Invoke("ChangeRotationRandom",Random.Range(rotationTimeMin,rotationTimeMax));
	}

	void ChangeRotation(){
		isEsquerda = !isEsquerda;
		direcao = new Vector3 ((isEsquerda ? Vector2.left : Vector2.right).x * Random.Range(0.1f,movementSpeedMax) * Time.deltaTime ,direcao.y ,0);
		transform.localRotation = Quaternion.Euler(0, isEsquerda ? 180 : 0, 0);
	}

	void ChangeDirectionRandom(){
		if (Random.value > 0.5f) {
			ChangeDirection();
		}
		Invoke ("ChangeDirectionRandom", Random.Range(directionTimeMin,directionTimeMax));
	}

	void ChangeDirection(){
		isCima = !isCima;
		direcao = new Vector3(direcao.x, (isCima ? Vector2.up : Vector2.down).y * Random.Range(0.1f,movementSpeedMax) * Time.deltaTime, 0);

	}

	void OnTriggerEnter2D(Collider2D coll)  {
		if (coll.gameObject.tag == "walls") {
			CancelInvoke();
			ChangeRotation();
			Invoke("ChangeRotationRandom",rotationTimeMax);
			ChangeDirection();
			Invoke ("ChangeDirectionRandom", directionTimeMax);
		}

	}

	void Update() {

		transform.position += direcao;
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
		gameObject.SetActive (false);
	}
}
