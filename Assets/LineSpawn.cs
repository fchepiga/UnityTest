using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawn : MonoBehaviour {

    LineRenderer lineRenderer;
    List<Vector3> ListVector = new List<Vector3>();
    Vector3 targetPos;
    Vector3 targetPos1;
    bool click;
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame

   void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ListVector.Add(targetPos);
            click = true;
        }

     if(click && Input.GetMouseButton(0))
        {
            var targetPos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ListVector.Add(targetPos1);
        }
    }
    private void OnMouseDown()
    {
        lineRenderer.SetPosition(0, ListVector[0]);
    }

}



