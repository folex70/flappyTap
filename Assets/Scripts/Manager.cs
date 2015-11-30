using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    private int _currentScore;

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
   
}
