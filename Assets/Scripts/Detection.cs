using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] float detectionTimer = 0.2f;
    [SerializeField] int memorySize = 10;
    [SerializeField] float detectionAngle = 90;
    [SerializeField] public LayerMask detectionMasks;
    public Action<Detection, GameObject> action;
    [SerializeField]private Transform eyes;

    private void Start()
    {
        StartCoroutine(CheckForFood());
    }

    private IEnumerator CheckForFood()
    {
        Collider[] results = new Collider[memorySize];
        Vector3 direction;
        RaycastHit hit;
        float angle;

        while (true)
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, results, detectionMasks) > 0)
            {
                
                float minDistance = float.MaxValue;
                GameObject bestObj = null;
                foreach (Collider obj in results)
                {
                    if (!obj)
                        continue;

                    if (obj.gameObject == this.transform.gameObject)
                        continue;

                    direction = obj.transform.position - eyes.position;
                    angle = Mathf.Abs(Vector3.Angle(transform.forward, direction));

                    if (angle > detectionAngle && angle < -detectionAngle)
                        continue;

                    //Debug.DrawRay(eyes.position, direction, Color.red, radius);
                    if (Physics.Raycast(eyes.position, direction, out hit, radius) && hit.transform.gameObject == obj.gameObject)
                    {
                        float distance = Vector3.Distance(this.transform.position, obj.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            bestObj = obj.gameObject;
                        }   
                    }
                }
                if (bestObj && this.enabled)
                    action.Invoke(this, bestObj);
            }

            yield return new WaitForSeconds(detectionTimer);
        }
    }
}