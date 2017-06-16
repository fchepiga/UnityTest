using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleView : MonoBehaviour {
   public SpriteRenderer renderer;
    

	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            renderer.color =Random.ColorHSV();
        }
	}

}
