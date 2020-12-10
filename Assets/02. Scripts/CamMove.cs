using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{

    public Transform target;
    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
