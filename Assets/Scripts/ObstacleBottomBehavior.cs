using UnityEngine;
using System.Collections;

//usar gameObject aqui faz referencia a esse game object, no caso, o obstaculo que esse script vai ser associado
public class ObstacleBottomBehavior : MonoBehaviour {

	public float range = 10;
	public float valorY = 0f;
	public float valorX = 0f;
	public float valorXBottom = 0f;
	public float valorYBottom = 0f;
    public float obstaculoYStartValue;
    public float obstaculoY;
    public int localLevel;
    private GameObject spawnerBottom;
    private GameObject limitBottom;
    public bool voltar = false;
    public float velocity = 0.05f;
    public int Random1 = 0;

    void Start()
    {
        obstaculoYStartValue = transform.position.y;
        spawnerBottom = GameObject.FindGameObjectWithTag("spawn_bottom");
        limitBottom = GameObject.FindGameObjectWithTag("LimiteCameraBottom");
        Random1 = Random.Range(1, 10);

    }

    void Update()
    {
        
        localLevel = Manager.instance.GetLevel();
        
        if (localLevel >= 10 && Random1 == 5)
        {
            //faz os obstaculos se moverem verticalmente para aumentar a dificuldade
            
            obstaculoY = transform.position.y;

            if (obstaculoY < spawnerBottom.transform.position.y && !voltar)
            {
                //sobe
                obstaculoY = obstaculoY + velocity;
            }
            else
            {
                voltar = true;
                //desce
                obstaculoY = obstaculoY - velocity;
               
            }

            if (obstaculoY < limitBottom.transform.position.y)
            {
                voltar = false;
            }


            transform.position = new Vector3(transform.position.x, obstaculoY, transform.position.z);

            //Destroy(gameObject, 7f);

        }


    }

    void OnEnable()
	{
        valorXBottom = GameObject.FindWithTag("spawn_bottom").transform.position.x;
        valorYBottom = GameObject.FindWithTag("spawn_bottom").transform.position.y;
		valorY = valorYBottom - range * Random.Range (0.1f, 0.4f);
		valorX = valorXBottom - range * Random.Range (0.1f, 0.4f);
		
		
		transform.position = new Vector3 ( valorX,
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