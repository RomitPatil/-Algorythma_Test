using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorChanging : MonoBehaviour {


	public void OnClick () {
        StartCoroutine(ChangeColor());
	}
	
	
	IEnumerator ChangeColor () {
        for (int i = 0; i < gameObject.transform.childCount; i++) {
          

            byte byte1, byte2, byte3;

            byte1 = byte.Parse( Random.RandomRange(0, 255).ToString());
            byte2 = byte.Parse(Random.RandomRange(0, 255).ToString());
            byte3 = byte.Parse(Random.RandomRange(0, 255).ToString());

            gameObject.transform.GetChild(i).GetComponent<Image>().color = new Color32(byte1,byte2 , byte3,255);

            yield return new WaitForSeconds(1f);
           
        }

        SceneManager.LoadScene("Scene2");
	}
}
