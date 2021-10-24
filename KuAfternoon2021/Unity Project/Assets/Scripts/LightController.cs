using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light light1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        print("Mouse Down");


        light1.enabled = !light1.enabled;
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
