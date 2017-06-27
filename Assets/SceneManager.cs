using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private float DeltaX = 0.1f;//граница движения по X
    private float DeltaY = 0.1f;//граница движения по Y
    public GameObject RectangleSpawn;
    Vector3 posOriginEnd;//позиция куда переместили объект
    Vector3 posOriginStart;// начальная позиция объекта
    private float doubleClickTime = 0.2f;
    private float lastClickTime = -10f;
    RaycastHit2D gO;
    private bool wasDoubleClick;
    bool Createline;
    LineRenderer lineRenderer;
    public static GameObject Rectangle;
    List<GameObject> ListClick = new List<GameObject>();
  
 
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary> raycast объектов создание связий проверка на движение объектов </summary>
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            posOriginStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            posOriginEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gO = Physics2D.Raycast(posOriginEnd, Vector3.forward, 100);
            if (gO) //если луч встретил коллайдер
            {
                       
                Debug.Log("name of obj: " + gO.transform.name);
                Debug.Log("HIT");
                ListClick.Add(gO.transform.gameObject);

                Debug.Log("Creetline: " + Createline);
                if (Createline) CreateCommunicatiom();
                else Createline = true;

                var difvector = posOriginEnd - posOriginStart;

                if (Math.Abs(difvector.x) > DeltaX || Math.Abs(difvector.y) > DeltaY) //Если изменилось положоние объекта 
                {
                    ListClick.Clear();
                    Createline = false;
                }
            }
            else
            {
                SpawnRectangle();
            }

            if (CheckDoubleClick())
            {
                ListClick.Clear();
                Destroy(gO.transform.gameObject);
            }
           
        }
    } 
    /// <summary>Добовление прямоугольника</summary>
    void SpawnRectangle()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = new Vector3(targetPos.x, targetPos.y, 1f);
        Rectangle = Instantiate(RectangleSpawn, targetPos, Quaternion.identity);
        Debug.Log("Rectangle pos: " + Rectangle.transform.position);
    }
    /// <summary>Создание связи между двумя объектами</summary>
    void CreateCommunicatiom()
    {
        Debug.Log("LINE REND");
        if (ListClick.Count >= 2)// были созданы связи или нет 
        {
            ListClick[0].GetComponent<RectangleView>().CreateConnection(ListClick[1]);
            ListClick[1].GetComponent<RectangleView>().CreateConnection(ListClick[0]);
            ListClick.Clear();
        }
        Createline = false;
    }
    /// <summary> Проверка на двойное нажатие</summary>
    bool CheckDoubleClick()
    {
        float timeDelta = Time.time - lastClickTime;
        if (timeDelta < doubleClickTime) // если происходит двойной клик 
        {
            Debug.Log("double click" + timeDelta);
            lastClickTime = 0;
            wasDoubleClick = true;
        }
        else
        {
            wasDoubleClick = false;
            lastClickTime = Time.time;
        }
        return wasDoubleClick;
    }
}



