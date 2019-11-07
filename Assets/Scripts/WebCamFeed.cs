using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamFeed : MonoBehaviour {

    public Material webCamMaterial;
    Texture2D tex;

    void Start()
    {


        // Grabbing all web cam devices
        WebCamDevice[] devices = WebCamTexture.devices;

        // I just use the first one, use which ever one you need 
        string camName = devices[1].name;

        // set the Texture from the cam feed
        WebCamTexture camFeed = new WebCamTexture(camName);
        


        // Then start the texture
        

        webCamMaterial.mainTexture = camFeed;

        camFeed.Play();
        //gameObject.GetComponent<Renderer>().material.mainTexture = camFeed;

    }
}
