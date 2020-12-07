using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

    //GameObjects
    public GameObject plr1Arrow;
    public GameObject plr2Arrow;
    public GameObject ctrlArrow;
    public GameObject credArrow;
    public GameObject dino;
    public GameObject plr1WP;
    public GameObject plr2WP;
    public GameObject ctrlWP;
    public GameObject credWP;


    //Waypoint Variables
    int cur = 0;
    public float speed = .1f;
    Rigidbody2D rb;


    //int
    int row = 1;//Side to side
    int col = 1;//Up and down

	// Use this for initialization
	void Start () {
        //Set Variables
        plr1Arrow.SetActive(false);
        plr2Arrow.SetActive(false);
        dino.transform.Rotate(0, 180, 0);
        rb = dino.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        /*Temperary continue button
		if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("FossilFuelers2D");
        }
        */

        print("row : " + row);
        print("col : " + col);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (row == 1 && col == 1)
            {
                //Run Single Player
                SceneManager.LoadScene("FossilFuelers2D1P");
            }
            else if (row == 1 && col == 2)
            {
                //Run Two Player
                SceneManager.LoadScene("FossilFuelers2D");
            }
            else if (row == 2 && col == 1)
            {
                //Run Credits
                SceneManager.LoadScene("Credits");
            }
            else if (row == 2 && col == 2)
            {
                //Run control screen
                SceneManager.LoadScene("Controls");
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (col != 2)
            {
                dino.transform.Rotate(0, 180, 0);
                col++;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (col != 1)
            {
                dino.transform.Rotate(0, 180, 0);
                col--;
            }

        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (row != 1)
            {
                row--;
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (row != 2)
            {
                row++;
            }
        }

        //Dinosaur moves to hovering arrow
        else
        {
            if (dino.transform.position != plr1WP.transform.position && col == 1 && row == 1)
            {
                Vector3 p = Vector3.MoveTowards(dino.transform.position,
                                plr1WP.transform.position,
                                speed);
                dino.GetComponent<Rigidbody2D>().MovePosition(p);
            }
            else if(dino.transform.position != plr2WP.transform.position && col == 2 && row == 1)
            {


                Vector3 p = Vector3.MoveTowards(dino.transform.position,
                plr2WP.transform.position,
                speed);
                dino.GetComponent<Rigidbody2D>().MovePosition(p);
            }
            else if (dino.transform.position != credWP.transform.position && col == 1 && row == 2)
            {


                Vector3 p = Vector3.MoveTowards(dino.transform.position,
                credWP.transform.position,
                speed);
                dino.GetComponent<Rigidbody2D>().MovePosition(p);
            }
            else if (dino.transform.position != ctrlWP.transform.position && col == 2 && row == 2)
            {


                Vector3 p = Vector3.MoveTowards(dino.transform.position,
                ctrlWP.transform.position,
                speed);
                dino.GetComponent<Rigidbody2D>().MovePosition(p);
            }

        }

        //Arrow Controller
        if (row == 1 && col == 1)
        {
            plr1Arrow.SetActive(true);
            plr2Arrow.SetActive(false);
            credArrow.SetActive(false);
            ctrlArrow.SetActive(false);
        }
        else if (row == 1 && col == 2)
        {
            plr1Arrow.SetActive(false);
            plr2Arrow.SetActive(true);
            credArrow.SetActive(false);
            ctrlArrow.SetActive(false);
        }
        else if (row == 2 && col == 1)
        {
            plr1Arrow.SetActive(false);
            plr2Arrow.SetActive(false);
            credArrow.SetActive(true);
            ctrlArrow.SetActive(false);
        }
        else if (row == 2 && col == 2)
        {
            plr1Arrow.SetActive(false);
            plr2Arrow.SetActive(false);
            credArrow.SetActive(false);
            ctrlArrow.SetActive(true);
        }
    }
}
