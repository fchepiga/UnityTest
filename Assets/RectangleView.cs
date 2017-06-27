
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(BoxCollider2D))]//добовление объекта типа BoxCollider2D
/// <summary> Создание лииний между объектами,drag and drop, ограничения места для создания объектов </summary>
public class RectangleView : MonoBehaviour {
# region variables
    /// <summary> спрайт для объекта </summary>
    public SpriteRenderer renderer;
    /// <summary>расстояние от центра оси до прямоугоьника </summary>
    public Vector3 screenPoint;
    /// <summary> смещение объекта</summary>
    public Vector3 offset;
    /// <summary> предыдущая позиция прямоугольника </summary>
    public Vector3 prePosition; 
    /// <summary> Текущая позиция прямоугольника</summary>
    public Vector3 curPosition;
    /// <summary> проверка на наличее места для создания пямоугольника </summary>
    bool createdNow = true;
    /// <summary> Рисует линии связи</summary>
    LineRenderer lineRenderer;
    /// <summary> лист со связями </summary>
    public List <GameObject> Rectangleconnected=new List<GameObject>();
    /// <summary> блокирование при движение прямоугольника </summary>
    public bool block;
#endregion


    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV();
        lineRenderer = GetComponent<LineRenderer>();
        
    }
    /// <summary> Добовление связи в лист</summary>
    public void CreateConnection(GameObject rConnected)
    {
        var obj = Rectangleconnected.Find(o => o == rConnected); //Ищет объект который равен rConnected

        if (gameObject == rConnected || obj!=null)
            return;
         
       Rectangleconnected.Add (rConnected);
    }
    /// <summary>Создание линий между прямоугольниками </summary>
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
    /// <summary>Drag and drop </summary>
    # region Drag and drop
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
    /// <summary> Форма объекта и ограничение пространства на создание прямоугольника  </summary>
   #endregion
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (createdNow == false)
        {
            transform.position = prePosition;
            //Debug.Log("pos"+prePosition);позиция отката
            block = true;
           // Debug.Log("Crah");столкновение прямоугольников
        }            
        if (createdNow && gameObject == SceneManager.Rectangle)
        {
            //Debug.Log("CrahandDestroy");уничтожение если нет места для прямоугольника
            Destroy(gameObject);
        }
    }
}

