using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float rotationSpeed = 1f;

    private float deltaZ = -90f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPosition - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + deltaZ;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            transform.rotation = new Quaternion(0f, 0f, -90f, 0f);
        }
    }
}
