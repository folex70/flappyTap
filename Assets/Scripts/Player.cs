using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int CurrentLife = 4;
    public int MaxLife = 4;
	public bool dead = false;
	public Animator anim;
    //score
    public int score;
    public Text textScore;

    // Use this for initialization
    void Start () {
		GameObject view = this.transform.Find("view").gameObject ;
		anim = view.GetComponent<Animator>();
        //invokerepeating nao aceita metodos com parametros
        InvokeRepeating("setScore", 1, 1);
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

	void OnBecameInvisible() {
		GameOver ();
	}
	

	public void takeDamage(int damage)	{
		if (CurrentLife < 2) {
			anim.Play("player_die");
			CurrentLife = 0;
			dead = true;
		} else {
			CurrentLife = CurrentLife - damage;
			anim.Play("player_hit");
		}
	}

	void GameOver(){
		Application.LoadLevel (Application.loadedLevel);
	}

    void setScore()
    {
        score = score + 10;
        textScore.text = " "+score.ToString("0000000000"); ;
    }

    public int getScore()
    {
        return score;
    }
}
