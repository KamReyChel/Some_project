using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    [SerializeField] private int numberOfTargets;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSomeTargets();
        Debug.Log("Test result: " + NumberOfTargetsTest());
    }

    bool NumberOfTargetsTest()
    {
        int localNumberOfTargets = GameObject.FindGameObjectsWithTag("Target_tag").Length;
        return localNumberOfTargets == 3;
    }

    void InitializeSomeTargets()
    {
        for(int i = 0; i < numberOfTargets; i++)
        {
            Instantiate(targetObject, new Vector3(Random.Range(0.0f, 15.0f), 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        }
    }

}
