using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Vector3 distance;

    private void FixedUpdate()
    {
        this.gameObject.transform.position = player.transform.position + distance;
    }

}
