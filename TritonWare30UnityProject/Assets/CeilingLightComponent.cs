using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLightComponent : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("yo");
        var litComponent = collision.gameObject.GetComponent<LitComponent>();

        if (litComponent != null)
        {
            litComponent.litObjects++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var litComponent = collision.gameObject.GetComponent<LitComponent>();

        if (litComponent != null)
        {
            litComponent.litObjects--;
        }
    }

}
