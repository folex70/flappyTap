using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int CurrentLife = 4;
    public int MaxLife = 4;
	public bool dead = false;
	public Animator anim;

    // Use this for initialization
    void Start () {
		GameObject view = this.transform.Find("view").gameObject ;
		anim = view.GetComponent<Animator>();
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
}
