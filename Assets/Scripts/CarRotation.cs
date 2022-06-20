using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotation : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up, 30.0f * Time.deltaTime);
    }
}
