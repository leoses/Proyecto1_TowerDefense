using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direccion : MonoBehaviour {

    private Camera cam;
    Vector2 mousePos;
    Vector3 point, mouse;

    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void OnGUI()
    {
        //Para calcular la posición del ratón en el mundo (según la cámara)
        Event currentEvent = Event.current;
        mousePos = new Vector2();
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
        point = new Vector3();
        //point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.farClipPlane));
        mouse = new Vector3(0, 0, 1);

        //Debug.Log(point + " y " + mousePos.y);

    }


    private void Update()
    {
        if (Time.timeScale > 0)
            transform.LookAt(point, mouse);
    }

}
