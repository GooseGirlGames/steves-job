using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject camera;
    public float parallaxEffect;
    void Start()
    {
        startpos = camera.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float tmp = camera.transform.position.x*(1-parallaxEffect);
        float distance = camera.transform.position.x*parallaxEffect;
        transform.position = new Vector3(startpos+distance, transform.position.y, transform.position.z);

        if(tmp > startpos + length) startpos += length;
        else if(tmp < startpos) startpos -= length;
        Debug.Log(tmp);
        Debug.Log("cam");
        Debug.Log(camera.transform.position.x);
        //Debug.Log(startpos);
    }
}
