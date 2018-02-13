using UnityEngine;
using System.Collections;


public class ObjectMovement : MonoBehaviour
{
    public GameObject plane;
    public UnityEngine.AI.NavMeshAgent agent;


    [HideInInspector]
    public bool tracked = false;

    //private Plane raycastPlane;
    private Vector3 targetPos;
    private bool firstMovement = true;
    private Plane raycastPlane;
    void Start()
    {

        if (agent != null)
        {
            targetPos = agent.transform.position;
        }

        raycastPlane = new Plane(-Camera.main.transform.forward, Vector3.zero);
    }

    void Update()
    {
        if (tracked)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.tapCount == 2)
                    {
						Debug.Log ("A");

                        if(plane != null)
                        {
                            plane.SetActive(true);
                        }
                        if(agent != null)
                        {
                            agent.enabled = true;
                        }

                        raycastPlane = new Plane(-Camera.main.transform.forward, Vector3.zero);

                        Ray ray = Camera.main.ScreenPointToRay(touch.position);

                        float dist;
                        if (raycastPlane.Raycast(ray, out dist))
                        {
                            targetPos = ray.GetPoint(dist);
                        }

                        firstMovement = false;
                    }

                }
            }

            if (agent != null && !firstMovement)
            {
                if (agent.enabled)
                {
                    agent.SetDestination(targetPos);
                    
                    if (agent.gameObject.GetComponent<Animator>() != null)
                    {
						Debug.Log ("B");

                        agent.gameObject.GetComponent<Animator>().SetFloat("Move", agent.velocity.magnitude);
                    }
                }

            }

            
        }

    }

    public void StopMovement()
    {
        firstMovement = true;

        if (agent != null)
        {
            agent.enabled = false;

            if (agent.gameObject.GetComponent<Animator>() != null)
            {
                agent.gameObject.GetComponent<Animator>().SetFloat("Move", 0);
            }
        }

        if (plane != null)
        {
            plane.SetActive(false);
        }
    }

}