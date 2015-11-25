using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {


	public void play () {
		Application.LoadLevel ("level_caverna_dark");
	}

	public void exit()
	{
		Application.Quit ();
	}

}
