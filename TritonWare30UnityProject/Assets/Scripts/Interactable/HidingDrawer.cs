using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingDrawer : MonoBehaviour, IInteractable
{
    public string popupDescription => "Hide";

    [SerializeField] private Transform exitTransform;
    [SerializeField] private float maximumHidingTime = 15f;
    [SerializeField] private bool containsMonster;
    private Animator _animator;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnInteract(GameObject interactor)
    {
       var player = interactor.GetComponent<PlayerController>();

       if (player != null)
       {
           if (!player.IsHiding)
           {
               _animator.Play("HidePlayer");
               player.Hide(gameObject, maximumHidingTime,true); 
               player.transform.position = exitTransform.position;
           }
           else
           {
               _animator.Play("HideNothing");
               player.transform.position = exitTransform.position;
               player.Unhide();
           }
           
       }
       
    }
    
}
