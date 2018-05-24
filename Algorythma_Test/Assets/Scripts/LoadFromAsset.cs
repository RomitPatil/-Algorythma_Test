using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFromAsset : MonoBehaviour {
    public RawImage SketchImage;
    public GameObject FILE;
    public void LoadAsset()
    {
        string url = "https://drive.google.com/uc?export=download&id=1U0MauTi4XYUrQ-ZHJ1jSJ6dLcol3FYzw";
        Debug.Log(url);
        WWW www = new WWW(url);


        StartCoroutine(WaitForReq(www));
    }
    IEnumerator WaitForReq(WWW www) {

        yield return www;
        AssetBundle bundle = www.assetBundle;
        if (www.error == null)
        {

            GameObject wheel = (GameObject)bundle.LoadAsset("sketchfile");
            FILE = wheel;
            SketchImage.texture = wheel.GetComponent<RawImage>().texture;
            SketchImage.transform.gameObject.SetActive(true);
        }
        else {
            Debug.Log(www.error);
        }
    }
}
