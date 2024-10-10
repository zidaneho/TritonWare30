using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{
    public Transform exitSpotTransform;
    public float maximumHideTime = 20f;
    public string popupDescription => "Hide";

    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    public void OnInteract(GameObject interactor)
    {
        var player = interactor.GetComponent<PlayerController>();

        if (player != null)
        {
            var input = player.GetComponent<InputBank>();
            var rb = player.GetComponent<Rigidbody2D>();
            if (player.isHiding)
            {
                player.isHiding = false;
                input.ToggleMoveInput(true);
                player.transform.position = exitSpotTransform.position;
                rb.isKinematic = false;
                _collider2D.isTrigger = false;
            }
            else
            {
                _collider2D.isTrigger = true;
                player.transform.position = transform.position;
                player.isHiding = true;
                rb.isKinematic = true;
                input.ToggleMoveInput(false);
            
                //Remember to play hiding animation here
                //In theory the player should not move at all
            }
        }
    }
}
