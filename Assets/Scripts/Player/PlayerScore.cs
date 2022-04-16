using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [Header("Scores")]
    [SerializeField] private float startingScore;
    public float MaxScore { get; private set; }
    public float Score { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Score = startingScore;

        if (MaxScore == 0)
        {
            MaxScore = FindObjectsOfType<ScoreItem>().Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function is called when the object becomes enabled and active
    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        Score = startingScore;
    }

    public void AddScore(int value)
    {
        Score += value;
    }
}
