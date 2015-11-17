using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] LifeSprites;
    public Image LifeUI;
    private Player player;
    //private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
         LifeUI.sprite = LifeSprites[player.CurrentLife];
         Debug.Log(player.CurrentLife);
    }
}
