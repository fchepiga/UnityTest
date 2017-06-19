using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public GameObject RectangleSpawn;
    Vector3 posOrigin;
    private float doubleClickTime = 0.2f;
    private float lastClickTime = -10f;

    private bool wasDoubleClick;

    void Start() {
        
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // если нажата левая кнопка мыши
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
           
            posOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var gO = Physics2D.Raycast(posOrigin, Vector3.forward, 100);
            if (gO) //если луч встретил коллайдер
            {
                Debug.Log("name of obj: " + gO.transform.name);
                Debug.Log("HIT");
                if (wasDoubleClick) Destroy(gO.transform.gameObject);
            }
            else
            {
                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos = new Vector3(targetPos.x, targetPos.y, 1f);
                GameObject Rectangle = Instantiate(RectangleSpawn, targetPos, Quaternion.identity);
                Debug.Log("Rectangle pos: " + Rectangle.transform.position);

            }
        }

    }

   }
    


    