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
    LineRenderer lineRenderer;
  public List <GameObject> Rectangleconnected=new List<GameObject>();
    public bool block;
 


    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV();
        lineRenderer = GetComponent<LineRenderer>();
        
    }
    public void CreateConnection(GameObject rConnected)
    {
        var obj = Rectangleconnected.Find(o => o == rConnected); //Ищет объект который равен rConnected

        if (gameObject == rConnected || obj!=null)
            return;
         
       Rectangleconnected.Add (rConnected);
    }
    private void Update()
    {
        lineRenderer.positionCount = Rectangleconnected.Count * 2; //колличество точек 

        for (int i = 0; i <lineRenderer.positionCount; i++)
        {
            if (i == 0 || i % 2 == 0) //Проверка на четную позицию 
            {
                lineRenderer.SetPosition(i, transform.position);
            }
            else
            {
                if (Rectangleconnected[i / 2] == null)
                {
                    Rectangleconnected.RemoveAt(i / 2);   //удаление связи
                    break;
                }
                lineRenderer.SetPosition(i, Rectangleconnected[i / 2].transform.position);
            }
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

