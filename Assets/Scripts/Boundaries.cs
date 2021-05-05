using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenbound;
    private float objectWidth;
    private float objectHeight;
    // Start is called before the first frame update
    void Start()
    {
        screenbound = Camera.main.ScreenToWorldPoint(transform.position);
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x/7;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenbound.x + objectWidth , screenbound.x*-1 - objectWidth );
        //viewPos.y = Mathf.Clamp(viewPos.y, screenbound.y + objectHeight, screenbound.y*-1 - objectHeight);
        transform.position = viewPos;
    }
}
