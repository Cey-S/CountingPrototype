using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask layersToHit;

    private Coroutine LookCoroutine;
    [SerializeField] [Range(0f, 5f)] private float rotationSpeed;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Vector3 ballDropPosition;
    [SerializeField] [Range(0f, 20f)] float moveSpeed;
    private bool moving;
    private bool holdingBall;
    private bool movingToDropBall; // for when the player clicks on the box
    private bool restrictedArea;
    private Transform currentBall;
    // Start is called before the first frame update
    void Start()
    {
        ballDropPosition = new Vector3(0, transform.position.y, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 100, layersToHit))
            {
                if (hit.collider.CompareTag("box"))
                {
                    worldPosition = ballDropPosition; //behind the box
                    movingToDropBall = true;
                }
                else
                {
                    worldPosition = hit.point;
                    worldPosition.y = transform.position.y;
                    movingToDropBall = false;
                }                
                
                //transform.position = worldPosition;
                moving = true;
                StartRotating();
            }
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, moveSpeed * Time.deltaTime);

            if(transform.position == worldPosition)
            {
                if (movingToDropBall && holdingBall)
                {
                    currentBall.parent = null;
                    currentBall.position = new Vector3(Random.Range(-0.1f, 0.1f), 3.0f, Random.Range(-0.1f, 0.1f));
                    currentBall.GetComponent<Rigidbody>().isKinematic = false;
                    holdingBall = false;
                }

                moving = false;
            }
        }

        if (restrictedArea)
        {
            moving = false;
            transform.position -= transform.forward * 0.01f; // creates a bumping effect around the box;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("ball"))
        {
            if (!holdingBall)
            {
                currentBall = collision.transform;
                currentBall.SetParent(transform);
                currentBall.localPosition = new Vector3(0, 0.8f, 0.7f); // holds above head
                currentBall.GetComponent<Rigidbody>().isKinematic = true;

                holdingBall = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("restricted"))
        {
            restrictedArea = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("restricted"))
        {
            restrictedArea = false;
        }
    }

    private void StartRotating()
    {
        if(LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(worldPosition - transform.position, Vector3.up);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * rotationSpeed;

            yield return null;
        }
    }
}
