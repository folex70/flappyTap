using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControleInput : MonoBehaviour {
	
	public float velocidade;
	private GameObject player;
	private bool isEsquerda;
	private Rigidbody2D playerBody;
	private Player playerClass;
	public Animator anim;

	// Use this for initialization
	void Start () {
		//camera = GetComponent.<Camera>;
		player = GameObject.Find("player");
		// Recupera rigidbody do player
		playerBody = player.GetComponent<Rigidbody2D> ();

		playerClass = player.GetComponent<Player>();

		anim = player.GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update () {

		if(!playerClass.dead)
		{
		

			#if UNITY_EDITOR_WIN
			// if unity editor
			if (Input.GetMouseButtonDown(0))
			{		
				Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 touchPos = new Vector2(wp.x, wp.y);

				if(touchPos.x > playerBody.position.x)
				{
					direta_touch();
				}
				else
				{
					esquerda_touch();
				}
			}
			#endif
			player.transform.Translate (Vector2.right * velocidade * Time.deltaTime);
				
		}
	}

	public void direta_touch()
	{
		Debug.Log("apertou pra frente");
		voa ();
		if(isEsquerda)
		{
			flipPersonagem();
		}
	}

	public void esquerda_touch(){
		Debug.Log("apertou pra tras");
		voa ();
		if(!isEsquerda)
		{
			flipPersonagem();
		}
	}

	private void flipPersonagem()
	{
		Debug.Log ("Flip" + isEsquerda);
		isEsquerda = !isEsquerda;
		player.transform.localRotation = Quaternion.Euler(0, isEsquerda ? 180 : 0, 0);
	}

	private void voa()
	{
		playerBody.AddForce(new Vector2(0,300));
	}
}
