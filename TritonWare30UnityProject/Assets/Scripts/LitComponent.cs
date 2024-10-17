using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitComponent : MonoBehaviour
{
    public int litObjects;
    public bool isLit => litObjects > 0;
}
