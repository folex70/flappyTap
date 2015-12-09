using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int CurrentLife = 4;
    public int MaxLife = 4;
	public bool dead = false;
	public bool exibirMenu = false;
	private Animator anim;
	public GameObject coracaoPrefab;
    //score foi transferido para classe manager.cs
    public int score;
    public Text textScore;

    // Use this for initialization
    void Start () {
		GameObject view = this.transform.Find("view").gameObject ;
		anim = view.GetComponent<Animator>();
        //invokerepeating nao aceita metodos com parametros
        // InvokeRepeating("setScore", 1, 1);
        InvokeRepeating("setScoreLocal", 1, 1);
    }
	
	// Update is called once per frame
	void Update () {


    }

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "walls") 
		{
			if(dead)
			{
				GameOver();
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
			setScoreLocal(100);
		}
	}

	void OnBecameInvisible() {
		GameOver ();
	}
	

	public void takeDamage(int damage)	{
		CurrentLife = CurrentLife - damage;
		anim.Play("player_hit");
		dropCoracao();

		if (CurrentLife == 0) {
			anim.Play("player_die");
			dead = true;
		}
	}

	void GameOver(){
		CancelInvoke();
		exibirMenu = true;

	}

    public void setScoreLocal()
    {
        //agora acessa a classe manager para alterar o score
        Manager.instance.SetScore(10);
        score = Manager.instance.GetScore();
        textScore.text = "" + score.ToString("0000000000");
    }


	public void setScoreLocal(int score)
	{
		//agora acessa a classe manager para alterar o score
		Manager.instance.SetScore(score);
		score = Manager.instance.GetScore();
		textScore.text = "" + score.ToString("0000000000");
	}

	void dropCoracao()
	{
		Instantiate (coracaoPrefab, transform.position, transform.rotation);
	}

}
