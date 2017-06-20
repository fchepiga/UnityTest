using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class RectangleView : MonoBehaviour {
   public SpriteRenderer renderer;
    public Vector3 screenPoint;
    public Vector3 offset;
    public Vector3 prePosition;
    bool createdNow = true;

    public bool block;

    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV();
	}

    void OnMouseDown()
    {
        block = false;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            
	}

    void OnMouseDrag()
    {
        if (block)
            return;

        createdNow = false;   
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        Vector3 prePosition = curPosition;

        transform.position = curPosition;

    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (createdNow == false)
        {
            
            block = true;
            Debug.Log("Crah");
        }            
        if (createdNow && gameObject == SceneManager.Rectangle)
        {
            Debug.Log("CrahandDestroy");
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Crah2");
        block = false;
    }
}

