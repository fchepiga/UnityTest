using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleView : MonoBehaviour {
   public SpriteRenderer renderer;
    public bool isDragging;
    private Vector3 handleToOrVector;

    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV();
	}



    void OnMouseDown()
    {
        handleToOrVector = transform.root.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
            renderer.color =Random.ColorHSV();
        
	}

    void OnMouseDrag()
    {
        transform.root.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + handleToOrVector;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }



}
