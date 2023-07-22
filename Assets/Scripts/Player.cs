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
    [SerializeField] [Range(0f, 20f)] float moveSpeed;
    private bool moving;
    private bool holdingBall;
    private bool restrictedArea;
    private Transform currentBall;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 100, layersToHit))
            {                
                worldPosition = hit.point;
                worldPosition.y = transform.position.y;
                moving = true;
                StartRotating();
            }
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, moveSpeed * Time.deltaTime);

            if(transform.position == worldPosition)
            {
                moving = false;
            }
        }

        if (restrictedArea) // player touches the box
        {
            if (holdingBall)
            {
                currentBall.parent = null;
                currentBall.position = new Vector3(Random.Range(-0.1f, 0.1f), 3.0f, Random.Range(-0.1f, 0.1f));
                currentBall.GetComponent<Rigidbody>().isKinematic = false;
                holdingBall = false;
            }

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
