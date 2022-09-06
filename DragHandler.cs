using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private bool HoldingNow = false;

    public void Set(bool flag)
    {
        HoldingNow = flag;
    }

    public bool Get()
    {
        return HoldingNow;
    }
}
