using System;
using UnityEngine;

public class IKHeadFollow : MonoBehaviour
{
    [Range(0,1f)] public float lookatWeight = 1f;
    private Vector3 mousePos;
    private Animator anim;
    private Camera cam;

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mouseCoords = Input.mousePosition;
        if (cam is not null)
        {
            mouseCoords.z = cam.farClipPlane;
            mousePos = cam.ScreenToWorldPoint(mouseCoords);
        }
    }

    private void OnAnimatorIK()
    {
        anim.SetLookAtWeight(lookatWeight);
        anim.SetLookAtPosition(mousePos);
    }
}
