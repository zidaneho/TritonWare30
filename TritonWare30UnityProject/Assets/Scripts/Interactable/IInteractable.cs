using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string popupDescription { get; }
    public void OnInteract(GameObject interactor);
}