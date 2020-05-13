using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTheGame : MonoBehaviour
{
    public GoalTarget car1;
    public GoalTarget car2;
    public GoalTarget car3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (car1.PutToCar && car2.PutToCar && car3.PutToCar)
        {
            Debug.Log("Complete!");
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }
}
