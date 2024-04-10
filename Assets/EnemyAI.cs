using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{



     public Transform target;
    public float forwardVectorComparison;
    public Animator anim;
    public float rotationSpeed = 5f;
    public ThirdPersonController moveDetect; 


    public bool angered = false;
    public bool seen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seen = true;
            Debug.Log("yello");
        }
    }



    private void OnTriggerExit(Collider other)
    {
        seen = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && seen)
        {
            // Get the direction to the target object
            Vector3 targetDirection = (target.position - transform.position).normalized;
            //targetDirection.y = 0f; // Lock rotation to Y-axis

            // Rotate towards the target direction
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


            Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.red);
            Debug.DrawRay(target.transform.position, target.transform.forward * 5.0f, Color.cyan);

            forwardVectorComparison = Vector3.Dot(target.transform.forward.normalized, transform.forward.normalized);

            if(forwardVectorComparison <= -0.97f && moveDetect.isMoving)
            {
                Debug.Log("BEEP");
                angered = true;
            }
            else
            {
                angered = false;
            }

            if (angered)
            {
                
                transform.position += targetDirection * 1.0f * Time.deltaTime;
                anim.SetFloat("MotionSpeed", 2.0f);
                anim.SetFloat("Speed", 2.0f);
            }
            else
            {
                anim.SetFloat("MotionSpeed", 0.0f);
                anim.SetFloat("Speed", 0.0f);
            }
        }
    }
}
