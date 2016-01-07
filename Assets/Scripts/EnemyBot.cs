using UnityEngine;
//using System;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class EnemyBot : MonoBehaviour {


	public int CurrentLife = 4;
	public int MaxLife = 4;
	public bool dead = false;
	public Animator anim;
	public GameObject coracaoPrefab;
	public Vector3 direcao ;
	public float force = 250;
	//-------------
	//private GameObject enemy;
	private Rigidbody2D enemyBody;
	//-------------
	private GameObject spawnerBottom;
    //private GameObject player;
    //private Rigidbody2D playerBody;
    //-------------
    public float speed =3;
    //-------------
    //audio
    AudioSource audio;
    public AudioClip AudioDamage;
    public AudioClip AudioDie;
    public AudioClip AudioDash;
    //------------- random
    public Vector2 direction;
	public int randomValueType;
	public int randomValue;
    public int randomValue2;
	public int randomValue3;
	//----------------- TIMMER
	public float timeLeftFixed = 5f;
	public float timeLeft = 3f;
    //-----------------
    //casting
    public Transform pontoCastInicialDir;
    public Transform pontoCastFinalDir;
    public Transform pontoCastInicialEsq;
    public Transform pontoCastFinalEsq;
    public Transform pontoCastInicialCima;
    public Transform pontoCastFinalCima;
    public Transform pontoCastInicialBaixo;
    public Transform pontoCastFinalBaixo;
    public bool hasCollisionInCastWithObstaculo = false;
    public bool hasCollisionInCastWithObstaculoCima = false; 
    public bool hasCollisionInCastWithObstaculoBaixo = false;
    public bool hasCollisionInCastWithPlayerDir = false;
    public bool hasCollisionInCastWithPlayerEsq = false;
    public bool hasCollisionInCastWithPlayerCima = false;
    public bool hasCollisionInCastWithPlayerBaixo = false;
    public bool hasCollisionInCastWithLimiteCameraDir = false;
    public bool hasCollisionInCastWithLimiteCameraEsq = false;
	//----------------
	public bool isEsquerda = false;

    void Start(){

		//enemy = GameObject.FindGameObjectWithTag("Enemy_bot"); //referenciar de outra forma
        //player = GameObject.FindGameObjectWithTag("Player");
        spawnerBottom = GameObject.FindGameObjectWithTag("spawn_bottom");
		enemyBody = GetComponent<Rigidbody2D> ();
        //playerBody = player.GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
		randomValueType = Random.Range(3, 5);
		speed = randomValueType;
    }

	void FixedUpdate()
	{
		Vector2 enemyVel 	= enemyBody.velocity;
		enemyVel.x 		   	= speed;
		enemyBody.velocity 	= enemyVel;

		if (enemyBody.position.y < spawnerBottom.transform.position.y && dead == false) 
		{
            enemyFly();
        }

		enemyBody.rotation = 0f;

        //Random dash
        randomValue = Random.Range(0, 50);
		randomValue2 = Random.Range(0, 500);
		randomValue3 = Random.Range(0, 200);

        //random para dash
		if (randomValue2 == 5)
        {
            Dash(new Vector2 (Random.Range(-1f,1f), Random.Range(-1f, 1f)));
        }

		//random direçao
		if (randomValue3 == 5 && (randomValueType == 5 || 
		                          randomValueType == 4 || 
		                          randomValueType == 2)) 
		{
			//speed = Random.Range (-3, 3);
			if(speed > 0){
				speed = speed * (-1);
			}
			else{
				speed = Mathf.Abs(speed);
			}

		}

		if (speed < 0) 
		{
			isEsquerda = true;
			flipPersonagem ();
		} 
		else if(speed == 0)
		{
			speed = 3;
		}
		else 
		{
			isEsquerda = false;
			flipPersonagem ();
		}
        // Dash(new Vector2 (Random.Range(-1f,1f), Random.Range(-1f, 1f)));
        //Dash(new Vector2(0, 1));
        //Dash(new Vector2(1, 0)); 
        //Dash(new Vector2(1, -1));
        //Dash(new Vector2(-1, 1));
		//--------------timmer------------
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0)
		{	
			timeLeft = timeLeftFixed;
			if(speed <0)
			{
				speed = Mathf.Abs(speed);

			}
		}
		//--------------------------------

        RayCasting();

        if (dead == false)
        {
            if (hasCollisionInCastWithObstaculo || hasCollisionInCastWithObstaculoBaixo)
            {
                enemyFly();
            }
            if (hasCollisionInCastWithObstaculoCima)
            {
                Dash(new Vector2(0, -1));
                Dash(new Vector2(1, 0));
            }

            if (hasCollisionInCastWithPlayerDir && randomValue == 5)
            {
                Dash(new Vector2(1, 0));
            }
            if (hasCollisionInCastWithPlayerEsq && randomValue == 5)
            {
                Dash(new Vector2(-1, 0));
            }
            if (hasCollisionInCastWithPlayerCima && randomValue == 5)
            {
                Dash(new Vector2(0, 1));
            }
            if (hasCollisionInCastWithPlayerBaixo && randomValue == 5)
            {
                Dash(new Vector2(0, -1));
            }

            if (hasCollisionInCastWithLimiteCameraDir)
            {
				//Dash(new Vector2(-1, 0));
				speed = speed * (-1);
            }

            if (hasCollisionInCastWithLimiteCameraEsq)
            {
				Debug.Log ("caiu aqui no colisor com o lado esquerdo ");
				//Dash(new Vector2(1, 0));
				//speed = Random.Range(3, 6);
				speed = Mathf.Abs(speed);

            }
        }
        
    }

    void RayCasting()
    {
        //RaycastHit2D hit;
        Debug.DrawLine(pontoCastInicialDir.position, pontoCastFinalDir.position, Color.red);
        Debug.DrawLine(pontoCastInicialEsq.position, pontoCastFinalEsq.position, Color.red);
        Debug.DrawLine(pontoCastInicialCima.position, pontoCastFinalCima.position, Color.red);
        Debug.DrawLine(pontoCastInicialBaixo.position, pontoCastFinalBaixo.position, Color.red);

        //hasCollisionInCast = Physics2D.Linecast (pontoCastMeioInicial.position, pontoCastMeioFinal.position, 
        //                                     1 << LayerMask.NameToLayer("bloco"));
        //hit = Physics2D.Raycast(pontoCastMeioInicial.position, pontoCastMeioFinal.position);

        hasCollisionInCastWithObstaculo = Physics2D.Linecast(pontoCastInicialDir.position, pontoCastFinalDir.position, 1 << LayerMask.NameToLayer("Obstaculo"));
        hasCollisionInCastWithObstaculoCima = Physics2D.Linecast(pontoCastInicialCima.position, pontoCastFinalCima.position, 1 << LayerMask.NameToLayer("Obstaculo"));
        hasCollisionInCastWithObstaculoBaixo = Physics2D.Linecast(pontoCastInicialBaixo.position, pontoCastFinalBaixo.position, 1 << LayerMask.NameToLayer("Obstaculo"));
        hasCollisionInCastWithPlayerDir = Physics2D.Linecast(pontoCastInicialDir.position, pontoCastFinalDir.position, 1 << LayerMask.NameToLayer("Player"));
        hasCollisionInCastWithPlayerEsq = Physics2D.Linecast(pontoCastInicialEsq.position, pontoCastFinalEsq.position, 1 << LayerMask.NameToLayer("Player"));
        hasCollisionInCastWithPlayerCima = Physics2D.Linecast(pontoCastInicialCima.position, pontoCastFinalCima.position, 1 << LayerMask.NameToLayer("Player"));
        hasCollisionInCastWithPlayerBaixo = Physics2D.Linecast(pontoCastInicialBaixo.position, pontoCastFinalBaixo.position, 1 << LayerMask.NameToLayer("Player"));
        hasCollisionInCastWithLimiteCameraDir = Physics2D.Linecast(pontoCastInicialDir.position, pontoCastFinalDir.position, 1 << LayerMask.NameToLayer("LimiteCameraDir"));
        hasCollisionInCastWithLimiteCameraEsq = Physics2D.Linecast(pontoCastInicialEsq.position, pontoCastFinalEsq.position, 1 << LayerMask.NameToLayer("LimiteCameraEsq"));
    }


    private void enemyFly()
    {
        if (enemyBody.velocity.y > 0)
        {
            enemyBody.velocity = new Vector2(enemyBody.velocity.x, 0);
        }
        enemyBody.AddForce(Vector2.up * force);
    }


    void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "walls") 
		{
			if(dead)
			{
                audio.PlayOneShot(AudioDie, 0.7F);
				gameObject.SetActive(false);
			}
			else
			{
				takeDamage(1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll)  {
		if (coll.gameObject.tag == "colectble") {
			coll.gameObject.SetActive(false);
		}
	}
	
	public void takeDamage(int damage)	
	{
		CurrentLife = CurrentLife - damage;
		anim.Play("Enemy_hit");
        audio.PlayOneShot(AudioDamage, 0.7F);

        dropCoracao();

        if (CurrentLife == 0) {
            audio.PlayOneShot(AudioDie, 0.7F);
            anim.Play("Enemy_die");
            dead = true;
            // enemy.SetActive(false);
        }
	}

    //Exemplos: dash para direita(1,0)  dash esquerda(-1,0) dash baixo(0,-1) dash cima(0,1)
    public void Dash(Vector2 direction)
    {
        audio.PlayOneShot(AudioDash, 0.7F);
        //direction = new Vector2 (player.transform.position.x,player.transform.position.y);
        if (direction.x > 1)
        {
            direction = direction.normalized;
        }
        enemyBody.position = enemyBody.position + direction * 2;
    }

	void dropCoracao()
	{
		Instantiate (coracaoPrefab, transform.position, transform.rotation);
	}

	private void flipPersonagem()
	{
		
		//isEsquerda = !isEsquerda;
		this.gameObject.transform.localRotation = Quaternion.Euler(0, isEsquerda ? 180 : 0, 0);
	}
}
