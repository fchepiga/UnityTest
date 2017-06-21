using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public GameObject RectangleSpawn;
    Vector3 posOrigin;
    private float doubleClickTime = 0.2f;
    private float lastClickTime = -10f;
    RaycastHit2D gO;
    private bool wasDoubleClick;
    bool Creetline;
    LineRenderer lineRenderer;
    public static SceneManager Instance { get { if (instance == null) instance = FindObjectOfType<SceneManager>(); return instance; } }
    private static SceneManager instance;

    List<Transform> listOfRectangles = new List<Transform>();
    
    void Awake()
    {
        instance = this; //Это  при эвейке скрипта сразу будет моей переменной
        lineRenderer = GetComponent <LineRenderer>();
    }
    public static GameObject Rectangle;
   

    void Start() {
      
    }
    void LineRend()
    {
        Debug.Log("LINE REND");
        lineRenderer.positionCount = lineRenderer.positionCount + 2;
        lineRenderer.SetPosition(lineRenderer.positionCount-2, listOfRectangles[0].transform.position);
        lineRenderer.SetPosition(lineRenderer.positionCount-1, listOfRectangles[1].transform.position);

        listOfRectangles.Clear();

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

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // если нажата левая кнопка мыши
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
       

            posOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gO = Physics2D.Raycast(posOrigin, Vector3.forward, 100);
            if (gO ) //если луч встретил коллайдер
            {
                
                Debug.Log("name of obj: " + gO.transform.name);
                Debug.Log("HIT");
                listOfRectangles.Add(gO.transform);

                Debug.Log("Creetline: " + Creetline);
                if (Creetline) LineRend();
                else Creetline = true;

            }
            else
            {
                
                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos = new Vector3(targetPos.x, targetPos.y, 1f);
                Rectangle = Instantiate(RectangleSpawn, targetPos, Quaternion.identity);
                Debug.Log("Rectangle pos: " + Rectangle.transform.position);
               
               
               
            }

            if (CheckDoubleClick())
                Destroy(gO.transform.gameObject);


        }
    }

   }
    


    