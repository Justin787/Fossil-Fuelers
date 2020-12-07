using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour {

    float speed = 65;

	// Use this for initialization
	void Start () {
        StartCoroutine(timer());
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(24);
        SceneManager.LoadScene("TitleScreen");
    }
}
