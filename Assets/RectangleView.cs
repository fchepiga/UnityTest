using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class RectangleView : MonoBehaviour {
   public SpriteRenderer renderer;
    public Vector3 screenPoint;
    public Vector3 offset;
    bool createdNow = true;

    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV();
	}



    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
        
	}

    void OnMouseDrag()
    {
        createdNow = false;   
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (createdNow == false)
        {
            Debug.Log("Crah");
        }            
        if (createdNow && gameObject == SceneManager.Rectangle)
        {
            Debug.Log("CrahandDestroy");
            Destroy(gameObject);
        }
    }



}

