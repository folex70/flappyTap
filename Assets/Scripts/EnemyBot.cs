using UnityEngine;
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
	private GameObject enemy;
	private Rigidbody2D enemyBody;
	//-------------
	private GameObject spawnerBottom;
    private GameObject player;
    private Rigidbody2D playerBody;
    //-------------
    public float speed;
    //-------------
    //audio
    AudioSource audio;
    public AudioClip AudioDamage;
    public AudioClip AudioDie;
    public AudioClip AudioDash;
    //-------------
    public Vector2 direction;
    public int randomValue;


    void Start(){

		enemy = GameObject.FindGameObjectWithTag("Enemy_bot");
        player = GameObject.FindGameObjectWithTag("Player");
        spawnerBottom = GameObject.FindGameObjectWithTag("spawn_bottom");
		enemyBody = enemy.GetComponent<Rigidbody2D> ();
        playerBody = player.GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

	void FixedUpdate()
	{
		Vector2 enemyVel = enemyBody.velocity;
		enemyVel.x = speed;
		enemyBody.velocity = enemyVel;

		if (enemyBody.position.y < spawnerBottom.transform.position.y && dead == false) 
		{
            enemyFly();
        }

		enemyBody.rotation = 0f;

        //Random dash
        randomValue = Random.Range(0, 500);
        if (randomValue == 5) {
            Dash(new Vector2 (Random.Range(-1f,1f), Random.Range(-1f, 1f)));
        }
        // Dash(new Vector2 (Random.Range(-1f,1f), Random.Range(-1f, 1f)));
        //Dash(new Vector2(0, 1));
        //Dash(new Vector2(1, 0)); 
        //Dash(new Vector2(1, -1));
        //Dash(new Vector2(-1, 1));

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
                enemy.SetActive(false);
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
}
