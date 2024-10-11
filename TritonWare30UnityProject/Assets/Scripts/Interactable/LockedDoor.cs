using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private int keyRequirement = 1;
    [SerializeField] private Collider2D[] openedDoorCollider;
    
    public string popupDescription => "Open";
    
    private Animator _animator;
    private BoxCollider2D _collider;
    

    private bool opened;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public void OnInteract(GameObject interactor)
    {
        var keyComponent = interactor.GetComponent<KeyComponent>();
        if (!opened && keyComponent != null && keyComponent.Keys >= keyRequirement)
        {
            opened = true;
            _collider.enabled = false;
            keyComponent.RemoveKeys(keyRequirement);
            _animator.Play("OpenDoor");
        }
    }
}
