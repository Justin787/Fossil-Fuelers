using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour {

    public GameObject player1Car;
    public GameObject player2Car;
    public GameObject finishLine;

    public Text countDown;
    public Text mainTime;
    public Text plr1Gear;
    public Text plr1RPM;
    public Text plr2Gear;
    public Text plr2RPM;
    public Text winText1;

    bool player2Active = true;//////////////////////////////////////////////////////////////////////////////////////////////////////////

    bool plr1drive = false;
    bool plr2drive = false;
    bool gameRunning = false;

    int winner = 0; //1 = plr 1   2 = plr 2

    int p1Forward = 0; //0 = not moving, 1 = forward, -1 = backwards
    int p2Forward = 0;

    //Gear ratios
    float[] gearRatio = new float[6];
    int curGear1 = 0;
    int curGear2 = 0;
    bool canShift1 = false;
    bool canShift2 = false;

    Vector3 currentVelocity;
    float speed = .1f;
    float acceleration = .75f;
    float accelFactor1 = 0;
    float accelFactor2 = 0;
    float topSpeed = 90;

	// Use this for initialization
	void Start () {
        //Gear Ratios
		gearRatio[0] = 1;//First Gear
        gearRatio[1] = 1.5f;//Second Gear
        gearRatio[2] = 2;//Third Gear
        gearRatio[3] = 2.5f;//Fourth Gear
        gearRatio[4] = 3;//Fifth Gear
        gearRatio[5] = 3.5f;//Sixth Gear

        //Text Objects
        countDown.text = "";
        mainTime.text = "";
        plr1Gear.text = "";
        plr1RPM.text = "";
        plr2Gear.text = "";
        plr2RPM.text = "";
        winText1.text = "";

        StartCoroutine(countdown());
    }

    // Update is called once per frame
    void Update() {
        if(winner != 0)
        {
            gameRunning = false;
        }

        //reset
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("TitleScreen");
        }

        //winner
        if(player1Car.transform.position.x >= finishLine.transform.position.x && winner == 0)
        {
            winner = 1;
        }
        else if(player2Car.transform.position.x >= finishLine.transform.position.x && winner == 0)
        {
            winner = 2;
        }



        //update text
        plr1Gear.text = "Gear: " + (curGear1 + 1);
        plr1RPM.text = "RPM: "  +  Mathf.RoundToInt(accelFactor1 * 100);

        plr2Gear.text = "Gear: " + (curGear2 + 1);
        plr2RPM.text = "RPM: " + Mathf.RoundToInt(accelFactor2 * 100);


        //Player1///////////////////////////////////////////
        if (Input.GetKey(KeyCode.UpArrow) && gameRunning)
        {
            player1Car.transform.position = player1Car.transform.position + player1Car.transform.right * speed * (accelFactor1/14) * gearRatio[curGear1];

            if (accelFactor1 < topSpeed)
                accelFactor1 += acceleration/.75f;
            else
                canShift1 = true;//////////

            //extra variables
            plr1drive = true;
            p1Forward = 1;

        }
        /*Reverse
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player1Car.transform.position -= player1Car.transform.right * speed;
            plr1drive = true;
            p1Forward = -1;
        }
        */
        else
        {
            if (p1Forward == 1 && accelFactor1 > 0 && plr1drive)
            {
                player1Car.transform.position = player1Car.transform.position + player1Car.transform.right * speed * (accelFactor1 / 16) * gearRatio[curGear1];

                accelFactor1 -= acceleration/2;
            }
            else
            {
                accelFactor1 = 0;
                plr1drive = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) && curGear1 < 5 && canShift1 && Input.GetKey(KeyCode.UpArrow) == false && gameRunning)
        {
            curGear1++;
            canShift1 = false;
            accelFactor1 = topSpeed/2;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && curGear1 > 0 && gameRunning)
        {
            curGear1--;
        }
            /*Turning
            if (Input.GetKey(KeyCode.RightArrow) && plr1drive == true)
            {
                player1Car.transform.Rotate(0, 0, -5);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && plr1drive == true)
            {
                player1Car.transform.Rotate(0,0,5);
            }
            */

            //Player2///////////////////////////////////////////
            if (player2Active == false)
            {
                player2Car.SetActive(false);
            }
            else
            {
                //Player2///////////////////////////////////////////
                if (Input.GetKey(KeyCode.W) && gameRunning)
                {
                    player2Car.transform.position = player2Car.transform.position + player2Car.transform.right * speed * (accelFactor2 / 14) * gearRatio[curGear2];

                    if (accelFactor2 < topSpeed)
                        accelFactor2 += acceleration / .75f;
                    else
                        canShift2 = true;//////////

                    //extra variables
                    plr2drive = true;
                    p2Forward = 1;

                }
                else
                {
                    if (p2Forward == 1 && accelFactor2 > 0 && plr2drive)
                    {
                        player2Car.transform.position = player2Car.transform.position + player2Car.transform.right * speed * (accelFactor2 / 16) * gearRatio[curGear2];

                        accelFactor2 -= acceleration / 2;
                    }
                    else
                    {
                        accelFactor2 = 0;
                        plr2drive = false;
                    }
                }

                if (Input.GetKeyDown(KeyCode.D) && curGear2 < 5 && canShift2 && Input.GetKey(KeyCode.W) == false && gameRunning)
                {
                    curGear2++;
                    canShift2 = false;
                    accelFactor2 = topSpeed / 2;
                }
                else if (Input.GetKeyDown(KeyCode.A) && curGear2 > 0 && gameRunning)
                {
                    curGear2--;
                }
            }
        
	}

    IEnumerator raceTimer()
    {
        float time = 0;

        while(true)
        {


            yield return new WaitForSeconds(1);

            if (gameRunning)    
            {
                time += 1;
                mainTime.text = time.ToString();
                
            }
            else if(winner != 0)
            {

                if (winner == 1)
                {
                    winText1.text = "Player One Wins!";
                }
                else if(winner == 2)
                {
                    winText1.text = "Player Two Wins!";
                }
            }
            else
            {
                mainTime.text = "0";
            }
        }

    }

    IEnumerator countdown()
    {
        while (gameRunning == false)
        {
            countDown.text = "4";
            yield return new WaitForSeconds(1);
            countDown.text = "3";
            yield return new WaitForSeconds(1);
            countDown.text = "2";
            yield return new WaitForSeconds(1);
            countDown.text = "1";
            yield return new WaitForSeconds(1);
            countDown.text = "GO!!";

            gameRunning = true;
            StartCoroutine(raceTimer());

            yield return new WaitForSeconds(1);
            countDown.text = "";
        }
    }
}
