using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingDrawer : MonsterHidingSpot, IInteractable
{
    public string popupDescription => "Hide";

    [SerializeField] private Transform exitTransform;
    [SerializeField] private float maximumHidingTime = 15f;
    [SerializeField] private bool containsMonster;
    [SerializeField] private float hideMonsterDuration = 30f;
    private Animator _animator;

    private Coroutine monsterDurationCoroutine;
    

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

    public override bool HideMonster()
    {
        if (containsMonster) return false;

        _animator.Play("HideMonster");
        containsMonster = true;
        if (monsterDurationCoroutine != null) StopCoroutine(monsterDurationCoroutine);
        StartCoroutine(MonsterDurationCoroutine());
        return true;
    }

    IEnumerator MonsterDurationCoroutine()
    {
        yield return new WaitForSeconds(hideMonsterDuration);
        _animator.Play("HideNothing");
        containsMonster = false;
    }

    public override bool ContainsMonster() => containsMonster;

}
