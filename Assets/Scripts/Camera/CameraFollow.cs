using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 distance;
    [SerializeField] private float smoothness = 5.0f;

    private void LateUpdate()
    {
        if (target == null)
        {
            GameObject objectToFollow = GameObject.FindWithTag("Player"); // Find the object with the "Player" tag

            if (objectToFollow != null)
            {
                target = objectToFollow.transform;
            }
            else
            {
                Debug.LogError("Player object not found!");
            }
        }

        if (target != null)
        {
            Vector3 targetPosition = target.position + distance;
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, targetPosition.y, targetPosition.z), smoothness * Time.deltaTime);
        }
    }
}