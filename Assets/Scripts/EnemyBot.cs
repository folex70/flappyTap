using UnityEngine;
using System.Collections;

public class EnemyBot : MonoBehaviour {

	//-------------
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
	//-------------

	public int CurrentLife = 4;
	public int MaxLife = 4;
	public bool dead = false;
	public Animator anim;
	public GameObject coracaoPrefab;

	// Use this for initialization
	void Start () {
		//direcao = new Vector3 (Vector2.right.x * movementSpeedMax * Time.deltaTime ,Vector2.up.y * movementSpeedMax * Time.deltaTime ,0);
	}
	
	// Update is called once per frame
	void Update () {
		//
		transform.position += new Vector3(0.1f,0.15f,0f);

	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "walls") 
		{
			if(dead)
			{
				//Destroy();
			}
			else
			{
				takeDamage(1);
			}
		}
	}

	public void takeDamage(int damage)	
	{
		CurrentLife = CurrentLife - damage;
		anim.Play("Enemy_hit");

		dropCoracao();
		
		if (CurrentLife == 0) {
			anim.Play("Enemy_die");
			dead = true;
		}
	}


	void dropCoracao()
	{
		Instantiate (coracaoPrefab, transform.position, transform.rotation);
	}
}
