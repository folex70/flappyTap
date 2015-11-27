using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ControleInput : MonoBehaviour {
	
	public float velocidade;
	public float forca = 250;
	public float offSet = 0.2f;
	private bool isInCantoEsquerdo;
	private bool isInCantoDireito;
	private GameObject player;
	private bool isEsquerda;
	private Rigidbody2D playerBody;
	private Player playerClass;
	private Rigidbody2D cameraBody;
	private float horzExtent;
	private float offSetEsquerda = 0;
	private Bounds playerBounds;
	public Animator anim;

	// Use this for initialization
	void Start () {
		//camera = GetComponent.<Camera>;
		player = GameObject.Find("player");

		cameraBody = Camera.main.GetComponent<Rigidbody2D>();
		// Recupera rigidbody do player
		playerBody = player.GetComponent<Rigidbody2D> ();

		playerBounds = player.GetComponent<BoxCollider2D>().bounds;

		playerClass = player.GetComponent<Player>();

		anim = player.GetComponent<Animator>();

		horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

		playerBody.velocity =  calculaVelocidadeDireita();

	}

	
	// Update is called once per frame
	void Update () {

		if(!playerClass.dead)
		{

			if(offSetEsquerda == 0)
			{
				offSetEsquerda = horzExtent;
			}

			if((transform.position.x - offSetEsquerda + offSet) > (player.transform.position.x))
			{
				isInCantoEsquerdo = true;
				playerBody.velocity = new Vector2(cameraBody.velocity.x, playerBody.velocity.y) ;
			}
			else
			{
				isInCantoEsquerdo = false;
			}

			if((transform.position.x + offSetEsquerda - offSet) < (player.transform.position.x))
			{
				isInCantoDireito = true;
				playerBody.velocity = new Vector2(cameraBody.velocity.x, playerBody.velocity.y) ;
			}
			else
			{
				isInCantoDireito = false;
			}


			Debug.Log(isInCantoEsquerdo);


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
			flipPersonagem();
		}
		if (isInCantoDireito) 
		{
			return;
		}
		if(playerBody.velocity.x < calculaVelocidadeDireita().x)
		{
			playerBody.velocity =  calculaVelocidadeDireita();
		}
	}

	public void esquerda_touch(){
		Debug.Log("apertou pra tras");
		voa ();

		if(!isEsquerda)
		{
			flipPersonagem();
		}

		if (isInCantoEsquerdo) 
		{
			return;
		}

		if(playerBody.velocity.x > (calculaVelocidadeEsquerda()).x)
		{
			playerBody.velocity =  calculaVelocidadeEsquerda();
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

		if (Input.GetMouseButtonDown(0))// verifica se mouse foi clicado
		{		
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			
			if(touchPos.x > transform.position.x)
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

	public Vector2 calculaVelocidadeDireita()
	{
		return ((Vector2.right * velocidade) + cameraBody.velocity);
	}

	public Vector2 calculaVelocidadeEsquerda()
	{
		return (Vector2.left * velocidade);
	}
}
