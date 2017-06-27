using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary> Проверка объектов на сцене их создание.Создание связей,проверка двойного клика</summary>
public class SceneManager : MonoBehaviour
{
    #region variables
    /// <summary> граница движения по оси </summary>
    private float MinDelta = 0.1f;
    /// <summary> объект для создания </summary>
    public GameObject RectangleSpawn;
    /// <summary>позиция куда переместили объект </summary>
    Vector3 posOriginEnd;
    /// <summary>начальная позиция объекта </summary>
    Vector3 posOriginStart;
    /// <summary> Время для двойного клика</summary
    private float doubleClickTime = 0.2f;
    /// <summary> Время от последнего клика</summary>
    private float lastClickTime = -10f;
    /// <summary> значение raycast</summary>
    RaycastHit2D gO;
    /// <summary> проверка двойного клика </summary>
    private bool wasDoubleClick;
    /// <summary>проверка на создание линии </summary>
    bool Createline; 
     /// <summary> добовление lineRenderer</summary>
    LineRenderer lineRenderer;
    /// <summary> создаваемый объект </summary>
    public static GameObject Rectangle;
    /// <summary> Лист с объектами</summary>
    List<GameObject> ListClick = new List<GameObject>();
#endregion

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

                if (Math.Abs(difvector.x) > MinDelta || Math.Abs(difvector.y) > MinDelta) //Если изменилось положоние объекта 
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



