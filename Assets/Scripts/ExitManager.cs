using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ExitManager : MonoBehaviour
{

    VariableStorageBehaviour yarnVariables;
    int studentsSaved = 0;
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

        if (studentsSaved >= 5)
        {
            exit.SetActive(false);
        }
    }
}
