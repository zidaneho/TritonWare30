using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1.5f;
    [SerializeField] private LayerMask interactLayer;

    private Collider2D[] results = new Collider2D[10];

    private void Update()
    {
        var interactable = GetClosestObject();

        if (interactable != null)
        {
            
        }
    }

    IInteractable GetClosestObject()
    {
        var size = Physics2D.OverlapCircleNonAlloc((Vector2)transform.position, interactionRadius, results, interactLayer);
        var shortestDistance = float.MaxValue;
        IInteractable closest = null;
        for (int i = 0; i < size; i++)
        {
            var interactable = results[i].GetComponent<IInteractable>();
            
            if (interactable != null && Vector3.Distance(transform.position, results[i].transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(transform.position, results[i].transform.position);
                closest = interactable;
            }
        }
        return closest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
