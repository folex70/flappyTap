using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControleInput : MonoBehaviour {
	
	public float velocidade;
	public float forca = 250;
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

		playerBody.velocity =  (Vector2.right * velocidade);
	}

	
	// Update is called once per frame
	void Update () {

		if(!playerClass.dead)
		{
		

			#if UNITY_EDITOR_WIN
			processaInputWindows();
			#endif
			processaInputMobile();

			//player.transform.Translate (Vector2.right * velocidade * Time.deltaTime);
				
		}
	}

	public void direta_touch()
	{
		Debug.Log("apertou pra frente");
		voa ();
		if(isEsquerda)
		{
			playerBody.velocity =  (Vector2.right * velocidade);
			flipPersonagem();
		}
	}

	public void esquerda_touch(){
		Debug.Log("apertou pra tras");
		voa ();
		if(!isEsquerda)
		{
			playerBody.velocity =  (Vector2.left * velocidade);
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
		if (playerBody.velocity.y > 0) {
			playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
		}
		playerBody.AddForce(Vector2.up * forca);
	}

	private void processaInputWindows(){

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
	}

	private void processaInputMobile(){
		
		// if unity editor
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{		
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
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
	}
}
