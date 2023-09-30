using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target; // The object to follow
    public Vector3 distance;
    public float smoothness = 5.0f;
    private void LateUpdate()
    {
        if (target == null)
        {
            // If there's no target, try to find the target
            GameObject objectToFollow = GameObject.FindWithTag("Player"); // Find the object with the "Player" tag

            if (objectToFollow != null)
            {
                target = objectToFollow.transform; // Assign the target when found
            }
            else
            {
                // If the target is still not found, you can take other actions
                // For example, you can print an error message.
                Debug.LogError("Player object not found!");
            }
        }

        // Calculate the position where the camera should move towards the target
        if (target != null)
        {
            Vector3 targetPosition = target.position + distance;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,targetPosition.y,targetPosition.z), smoothness * Time.deltaTime);
        }
    }
}