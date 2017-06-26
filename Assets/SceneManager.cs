using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject RectangleSpawn;
    Vector3 posOrigin;
    Vector3 posOrigin1;
    private float doubleClickTime = 0.2f;
    private float lastClickTime = -10f;
    RaycastHit2D gO;
    private bool wasDoubleClick;
  bool Creetline;
    LineRenderer lineRenderer;
    public static SceneManager Instance { get { if (instance == null) instance = FindObjectOfType<SceneManager>(); return instance; } }
    private static SceneManager instance;
    public static GameObject Rectangle;
    List<GameObject> ListClick = new List<GameObject>();
  
 
    void Awake()
    {
        instance = this; //Это  при эвейке скрипта сразу будет моей переменной
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            posOrigin1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) // если отжата левая кнопка мыши
        {
            
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            posOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gO = Physics2D.Raycast(posOrigin, Vector3.forward, 100);
            if (gO) //если луч встретил коллайдер
            {
                       
                Debug.Log("name of obj: " + gO.transform.name);
                Debug.Log("HIT");
                ListClick.Add(gO.transform.gameObject);

                Debug.Log("Creetline: " + Creetline);
                if (Creetline) CreateCommunicatiom();
                else Creetline = true;

                var difvector = posOrigin - posOrigin1;

                if (Math.Abs(difvector.x) > 0.01f || Math.Abs(difvector.y) > 0.01f) //Движение объекта 
                {
                    ListClick.Clear();
                    Creetline = false;
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
    void SpawnRectangle()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = new Vector3(targetPos.x, targetPos.y, 1f);
        Rectangle = Instantiate(RectangleSpawn, targetPos, Quaternion.identity);
        Debug.Log("Rectangle pos: " + Rectangle.transform.position);
    }

    void CreateCommunicatiom()
    {
        Debug.Log("LINE REND");
        if (ListClick.Count >= 2)// были созданы связи или нет 
        {
            ListClick[0].GetComponent<RectangleView>().CreateConnection(ListClick[1]);
            ListClick[1].GetComponent<RectangleView>().CreateConnection(ListClick[0]);
            ListClick.Clear();
        }
        Creetline = false;
    }
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



