using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class RectangleView : MonoBehaviour {
   public SpriteRenderer renderer;
    public Vector3 screenPoint;
    public Vector3 offset;
    public Vector3 prePosition;
    int saveindexline1 = -1;
    int saveindexline2 = -1;

    public Vector3 curPosition;
    bool createdNow = true;
    LineRenderer lineRenderer;
    public GameObject Rectangleconnected;
    public bool block;

    private int countLines;


    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void CreateConnection(GameObject rConnected)
    {
        if (gameObject == rConnected || Rectangleconnected == rConnected)
            return;

        Rectangleconnected = rConnected;
        LineRend();
    } 

    private void LineRend()
    {       
        lineRenderer.positionCount = lineRenderer.positionCount + 2;
        lineRenderer.SetPosition(lineRenderer.positionCount - 2,transform.position);
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, Rectangleconnected.transform.position);
        saveindexline1 = lineRenderer.positionCount - 2;
        saveindexline2 = lineRenderer.positionCount - 1;

     

    }
    private void Update()
    {
     if(saveindexline1!=-1 && saveindexline2!=-1)
        {
            lineRenderer.SetPosition(saveindexline1, transform.position);
            lineRenderer.SetPosition(saveindexline2, Rectangleconnected.transform.position);
        }
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

