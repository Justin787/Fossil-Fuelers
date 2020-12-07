using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerOnePlayer : MonoBehaviour {

    //Variables
    public GameObject player1Car;
    public GameObject AICar;
    public GameObject finishLine;

    public Text countDown;
    public Text mainTime;
    public Text plr1Gear;
    public Text plr1RPM;
    public Text winText1;

    bool plr1drive = false;
    bool gameRunning = false;

    int winner = 0; //1 = plr 1   2 = plr 2
    int p1Forward = 0; //0 = not moving, 1 = forward, -1 = backwards

    //Gear ratios
    float[] gearRatio = new float[6];
    int curGear1 = 0;
    int AIGear = 0;
    bool canShift1 = false;

    Vector3 currentVelocity;
    float speed = .1f;
    float acceleration = .75f;
    float accelFactor1 = 0;
    float topSpeed = 90;
    float rand;

    // Use this for initialization
    void Start () {
        //Gear Ratios
        gearRatio[0] = 1;//First Gear
        gearRatio[1] = 1.5f;//Second Gear
        gearRatio[2] = 2;//Third Gear
        gearRatio[3] = 2.5f;//Fourth Gear
        gearRatio[4] = 3;//Fifth Gear
        gearRatio[5] = 3.5f;//Sixth Gear

        rand = Random.Range(82, 93);

        //Text Objects
        countDown.text = "";
        mainTime.text = "";
        plr1Gear.text = "";
        plr1RPM.text = "";
        winText1.text = "";

        StartCoroutine(countdown());
    }

    // Update is called once per frame
    void Update() {
        if (winner != 0)
        {
            gameRunning = false;
        }

        //reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("TitleScreen");
        }

        //winner
        if (player1Car.transform.position.x >= finishLine.transform.position.x && winner == 0)
        {
            winner = 1;
        }
        else if (AICar.transform.position.x >= finishLine.transform.position.x && winner == 0)
        {
            winner = 2;
        }

        if (curGear1 < 5)
        {
            AIGear = curGear1;
        }

        //update text
        plr1Gear.text = "Gear: " + (curGear1 + 1);
        plr1RPM.text = "RPM: " + Mathf.RoundToInt(accelFactor1 * 100);

        //AI
        if(gameRunning)
        {


            AICar.transform.position = AICar.transform.position + player1Car.transform.right * speed * (accelFactor1 / 14 + 1) * gearRatio[AIGear] * (rand/100) ;
        }
        else if(winner != 0)
        {
            AICar.transform.position = AICar.transform.position + player1Car.transform.right * speed * (accelFactor1 / 14) * gearRatio[AIGear] * (rand / 100);
        }

        //Player1///////////////////////////////////////////
        if (Input.GetKey(KeyCode.UpArrow) && gameRunning)
        {
            player1Car.transform.position = player1Car.transform.position + player1Car.transform.right * speed * (accelFactor1 / 14) * gearRatio[curGear1];

            if (accelFactor1 < topSpeed)
                accelFactor1 += acceleration / .75f;
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

                accelFactor1 -= acceleration / 2;
            }
            else
            {
                accelFactor1 = 0;
                plr1drive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && curGear1 < 5 && canShift1 && Input.GetKey(KeyCode.UpArrow) == false && gameRunning)
        {
            curGear1++;
            canShift1 = false;
            accelFactor1 = topSpeed / 2;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && curGear1 > 0 && gameRunning)
        {
            curGear1--;
        }

    }

    IEnumerator raceTimer()
    {
        float time = 0;

        while (true)
        {



            yield return new WaitForSeconds(1);

            if (gameRunning)
            {
                time += 1;
                mainTime.text = time.ToString();

            }
            else if (winner != 0)
            {

                if (winner == 1)
                {
                    winText1.text = "Player One Wins!";
                }
                else if (winner == 2)
                {
                    winText1.text = "AI Wins!";
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
