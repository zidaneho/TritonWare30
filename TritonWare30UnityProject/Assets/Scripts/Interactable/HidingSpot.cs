using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite closedSprite;
    public Transform exitSpotTransform;
    public float maximumHideTime = 20f;
    public string popupDescription => "Hide";

    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnInteract(GameObject interactor)
    {
        var player = interactor.GetComponent<PlayerController>();

        if (player != null)
        {
            var rb = player.GetComponent<Rigidbody2D>();
            if (player.IsHiding)
            {
                _spriteRenderer.sprite = openedSprite;
                player.Unhide();
                player.transform.position = exitSpotTransform.position;
                rb.isKinematic = false;
                _collider2D.isTrigger = false;
            }
            else
            {
                _spriteRenderer.sprite = closedSprite;
                player.Hide(gameObject, maximumHideTime,true);
                _collider2D.isTrigger = true;
                player.transform.position = transform.position;
                rb.isKinematic = true;
            
                //Remember to play hiding animation here
                //In theory the player should not move at all
            }
        }
    }
}
