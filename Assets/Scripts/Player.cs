using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask layersToHit;

    private Vector3 screenPosition;
    private Vector3 worldPosition;
    [SerializeField] [Range(0f, 20f)] float moveSpeed;
    private bool moving;
    // Start is called before the first frame update
    void Start()
    {

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
                worldPosition = hit.point;
                worldPosition.y = transform.position.y;
                
                //transform.position = worldPosition;
                moving = true;
            }
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, moveSpeed * Time.deltaTime);

            if(transform.position == worldPosition)
            {
                moving = false;
                Debug.Log("moving false");
            }
        }
    }
}
