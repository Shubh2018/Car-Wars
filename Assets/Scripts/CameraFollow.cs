using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _translateSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private GameObject[] _cars;

    Ray ray;
    public LayerMask env;   // Layer to be checked
    private bool behindBuilding = false;

    [SerializeField] private Material initialMat;   // The default material of the hit gameobject
    [SerializeField] private Material transparentMat; // The material to be applied

    private MeshRenderer hitRenderer;
    private MeshRenderer previousRenderer;

    private void Start()
    {
        foreach(var car in _cars)
        {
            if (car.activeInHierarchy)
                _target = car.transform;
        }
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, env))   // Raycast in infinite distance
        {
            if (!hit.transform.CompareTag("Player") && !behindBuilding) // if player not behind buildings and the ray cant hit player
            {
                hitRenderer = hit.transform.GetComponent<MeshRenderer>();
                initialMat = hitRenderer.material;
                behindBuilding = true;

                if (hitRenderer)
                    previousRenderer = hitRenderer;
            }

            else if (!hit.transform.CompareTag("Player") && behindBuilding) // if player still behind buildings
            {
                hitRenderer.material = transparentMat;

                hitRenderer = hit.transform.GetComponent<MeshRenderer>();

                if (hitRenderer != previousRenderer)
                {
                    previousRenderer.material = initialMat;
                    previousRenderer = hitRenderer;
                    initialMat = hitRenderer.material;
                }
            }

            else if (hit.transform.CompareTag("Player")) // if ray colliders with the player
            {
                if (initialMat)
                    hitRenderer.material = initialMat;

                behindBuilding = false;
            }
        }
    }

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        var targetPosition = _target.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = _target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}
