using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform jumpArea;

    private Rigidbody playerRb;
    private float verticalInput;
    private float speed = 6f;
    private float jumpForce = 10f;

    
    public float gravityModifier;
    public bool isOnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; 
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerRb.velocity = AddingVelocity(jumpArea.position, transform.position, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    Vector3 AddingVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;

        float distancePosY = distance.y;
        float distancePosXandZ = distanceXZ.magnitude;

        float velocityPosX = distancePosXandZ / time;
        float velocityPosY = distancePosY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= velocityPosX;
        result.y = velocityPosY;
        return result;
    }

    
}