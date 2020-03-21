using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2DPlatformer : MonoBehaviour
{

    public Transform target; // target - podazanie kamery
    public float smoothing; //jak szybko podaza

    Vector3 offset; //odleglosc postaci i kamery

    float lowY; //najnizszy mozliwy punkt kamery

    void Start()
    {
       
        offset = transform.position - target.position;
        lowY = transform.position.y;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing*Time.deltaTime);
        //Lerp - smooth motion; deltaTime - czas od poprzedniej klatki;

        if (transform.position.y < lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        //jesli ponizej lowY czyli najnizszej, to spada;

    }


}
