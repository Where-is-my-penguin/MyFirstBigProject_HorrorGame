using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform MainDoor;
    public float openAngle = -175f;
    public float duration = 2.0f;

    private bool opened = false;

    public void Activate()
    {
        if (opened) return;

        opened = true;
        MainDoor.DORotate(new Vector3(0, openAngle, 0), duration, RotateMode.LocalAxisAdd);
    }
}
