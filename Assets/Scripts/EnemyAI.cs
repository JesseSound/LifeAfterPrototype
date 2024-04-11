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
    public bool playerTP = false; //tp the player in third person controller
    
   public bool angered = false;
    bool seen = false;

    public Transform originalSpawn;


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
        angered = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        angered = false;
        seen = false;
        
    }




    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, target.position);


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

            if (forwardVectorComparison <= -0.917f && moveDetect.isMoving)
            {
                Debug.Log("BEEP");
                angered = true;

            }
            else if (!moveDetect.isMoving && forwardVectorComparison > -0.917f)
            {
                angered = false;
            }
            if (angered)
            {
              //  StartCoroutine(Angered(targetDirection));
                anim.SetFloat("MotionSpeed", 2.0f);
                anim.SetFloat("Speed", 3.0f);
                transform.position += targetDirection * 1.5f * Time.deltaTime;
            }
            else
            {
                
                anim.SetFloat("MotionSpeed", 0.0f);
                anim.SetFloat("Speed", 0.0f);
                anim.StopPlayback();
                
            }
            if(distance < 1.0f && angered)
            {
                Debug.Log("Close!");
                playerTP = true;
            }
            if(distance <3.0f && distance >1.0f)
            {
                Debug.Log("this range");
            }
            
        }
    }

    public IEnumerator Angered(Vector3 targetDirection)
    {

        anim.SetFloat("MotionSpeed", 2.0f);
        anim.SetFloat("Speed", 3.0f);
        transform.position += targetDirection * 2.0f * Time.deltaTime;
        
        yield return new WaitForSeconds(3.0f);
      
    }

}
