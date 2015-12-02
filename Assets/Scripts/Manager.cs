using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    private int _currentScore;
	private int _maxScore;

    public static Manager instance;

    void Awake()
    {
        instance = this;
    }

    public void SetScore(int num)
    {
        _currentScore = _currentScore + num;
    }

    public int GetScore()
    {
        return _currentScore;
    }

	public void SetHiScore(int num)
	{
		_maxScore = num;
	}
	
	public int GetHiScore()
	{
		return _maxScore;
	}
   
}
