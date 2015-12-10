using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ObstacleUpBehavior : MonoBehaviour {
	
	//public Vector2 velocity = new Vector2(-5,0);
	public float range = 10;
	public float valorY = 0f;
	public float valorXTeto = 0f;
	public float valorYTeto = 0f;
    public int Random1 = 0;
    public int localLevel;
    public Rigidbody2D rb;
    public float timeLeft = 6f;
    public float velocity = 0.05f;
    public float obstaculoY;
    public float startObstacleX;
    //audio
    AudioSource audio;
    public AudioClip AudioRock;
    public bool playSoundOneTme = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        startObstacleX = transform.position.x;
    }

    void Update()
    {
        localLevel = Manager.instance.GetLevel();
        obstaculoY = transform.position.y;
        

        if (localLevel >= 10 && Random1 == 5  || 
           (localLevel >= 15 && (Random1 == 5 || Random1 == 10)) || 
           (localLevel >= 20 && (Random1 == 1 || Random1 == 5    || Random1 == 10)) ||
           (localLevel >= 25 && (Random1 == 1 || Random1 == 2    || Random1 == 5    || Random1 == 10)) ||
           (localLevel >= 30 && (Random1 == 1 || Random1 == 2    || Random1 == 3    || Random1 == 5    || Random1 == 10)) ||
           (localLevel >= 35 && (Random1 == 1 || Random1 == 2    || Random1 == 3    || Random1 == 4    || Random1 == 5    || Random1 == 10)) ||
           (localLevel >= 40 && (Random1 == 1 || Random1 == 2    || Random1 == 3    || Random1 == 4    || Random1 == 5    || Random1 == 6    || Random1 == 10)) ||
           (localLevel >= 45 && (Random1 == 1 || Random1 == 2    || Random1 == 3    || Random1 == 4    || Random1 == 5    || Random1 == 6    || Random1 == 7    || Random1 == 10)) ||
           (localLevel >= 50 && (Random1 == 1 || Random1 == 2    || Random1 == 3    || Random1 == 4    || Random1 == 5    || Random1 == 6    || Random1 == 7    || Random1 == 8    || Random1 == 10)) ||
           (localLevel >= 55)
           )
        {
            timeLeft -= Time.deltaTime;
            
            if (timeLeft < 0)
            {
                obstaculoY = obstaculoY - velocity;
                velocity += Random.Range(0.001f, 0.009f);
                transform.position = new Vector3(transform.position.x, obstaculoY, transform.position.z);
                if (!playSoundOneTme)
                {
                    audio.PlayOneShot(AudioRock, 0.7F);
                    playSoundOneTme = true;
                }
            }
            
        }

    }

    void OnEnable()
	{
        velocity = 0.05f;
        Random1 = Random.Range(1, 10);
        timeLeft = Random.Range(6, 11);
        playSoundOneTme = false;

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
