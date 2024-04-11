using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ExitManager : MonoBehaviour
{

    VariableStorageBehaviour yarnVariables;
    float studentsSaved = 0;
    public GameObject exit;
    // Start is called before the first frame update
    void Start()
    {
        yarnVariables = FindObjectOfType<VariableStorageBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        yarnVariables.TryGetValue("$studentCount", out studentsSaved);

        Debug.Log(studentsSaved);

        if (studentsSaved >= 5.0f)
        {
            exit.SetActive(false);
        }
    }
}
