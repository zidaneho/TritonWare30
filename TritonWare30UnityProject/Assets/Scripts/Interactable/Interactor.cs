using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1.5f;
    [SerializeField] private LayerMask interactLayer;

    private InputBank _input;
    private Collider2D[] results = new Collider2D[10];

    private void Awake()
    {
        _input = GetComponent<InputBank>();
    }

    private void Update()
    {
        
        if (_input.wasInteractPressedThisFrame)
        {
            var interactable = GetClosestObject();
            interactable?.OnInteract(this.gameObject);
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

            var distance = Vector2.Distance(transform.position, results[i].transform.position);
            if (interactable != null && distance < shortestDistance)
            {
                shortestDistance = distance;
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
