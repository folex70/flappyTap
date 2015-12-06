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
	public float force = 200;
	//-------------
	private GameObject enemy;
	private Rigidbody2D enemyBody;
	//-------------
	private GameObject spawnerBottom;
	//-------------
	public float speed;
    //-------------
    //audio
    AudioSource audio;
    public AudioClip AudioDamage;
    public AudioClip AudioDie;
    

    void Start(){

		enemy = GameObject.FindGameObjectWithTag("Enemy_bot");
		spawnerBottom = GameObject.FindGameObjectWithTag("spawn_bottom");
		enemyBody = enemy.GetComponent<Rigidbody2D> ();
        audio = GetComponent<AudioSource>();
    }

	void FixedUpdate()
	{
		Vector2 enemyVel = enemyBody.velocity;
		enemyVel.x = speed;
		enemyBody.velocity = enemyVel;

		if (enemyBody.position.y < spawnerBottom.transform.position.y && dead == false) 
		{
			enemyBody.AddForce(Vector2.up * force);
		}

		enemyBody.rotation = 0f;
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


	void dropCoracao()
	{
		Instantiate (coracaoPrefab, transform.position, transform.rotation);
	}
}
