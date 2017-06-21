using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class RectangleView : MonoBehaviour {
   public SpriteRenderer renderer;
    public Vector3 screenPoint;
    public Vector3 offset;
    public Vector3 prePosition;
    public Vector3 curPosition;
    bool createdNow = true;
    bool CreateLine;
    RaycastHit2D gO;
    Vector3 posOrigin;
    LineRenderer lineRenderer;
    public bool block;
    List<Transform> listOfRectangles = new List<Transform>();
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
        if (block) return;
        createdNow = false;   
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        prePosition = gameObject.transform.position;
        transform.position = curPosition;

    }
     

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (createdNow == false)
        {
            transform.position = prePosition;
            //Debug.Log("pos"+prePosition);позиция отката
            block = true;
           // Debug.Log("Crah");столкновение объектов
        }            
        if (createdNow && gameObject == SceneManager.Rectangle)
        {
            //Debug.Log("CrahandDestroy");уничтожение если нет места для объекта
            Destroy(gameObject);
        }
    }


}

