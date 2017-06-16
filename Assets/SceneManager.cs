using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public GameObject RectangleSpawn;

    private bool needCheck;

    void Start() {

    }

    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 10))
        {

        }

            }
    void Update() {

        if (needCheck)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, 10))
            {
                needCheck = false;
                Debug.Log("raycast");
            }
        }

            if (Input.GetMouseButtonDown(0))
        {
                needCheck = true;
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = new Vector3(targetPos.x, targetPos.y, 1f);
            GameObject Rectangle = Instantiate(RectangleSpawn, targetPos, Quaternion.identity);

        }

    }
}
