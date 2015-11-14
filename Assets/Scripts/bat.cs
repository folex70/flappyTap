using UnityEngine;
using System.Collections;

public class bat : MonoBehaviour {

	public Vector2 jumpForce = new Vector2(0,200);
    private Rigidbody2D rb2d;
    public float maxSpeed = 5f;
   
    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //descolamento vertical
        if (Input.GetKeyUp("space")){
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().AddForce(jumpForce);
            //rigidbody2D.velocity = Vector2.zero;
            //rigidbody2D.addForce(jumpForce); 
            //para touch
            /* 
			   if(Input.touchCount >= 1)
        		{
		            if(Input.GetTouch(0).phase == TouchPhase.Ended)
		            {
		                rigidbody2D.velocity = Vector2.zero;
		                rigidbody2D.AddForce(jumpForce);
		            }
        		}
			 */
        }

        //descolamento horizontal
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Debug.Log("apertou pra frente");
            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        //condição de game over. sair da tela
        Vector2 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0) {
			GameOver();
		}

	}

	void OnCollisionEnter2D(Collision2D other){
		GameOver();
	}

	void GameOver(){
		Application.LoadLevel (Application.loadedLevel);
	}

}
