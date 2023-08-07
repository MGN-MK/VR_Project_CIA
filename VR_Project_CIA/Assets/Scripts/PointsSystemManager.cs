using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsSystemManager : MonoBehaviour
{
    [Header("Public Var")]
    public int EarlyPoints;
    public int PerfectPoints;
    public int LatePoints;

    [Header("TextMeshPro Var")]
    public TextMeshProUGUI ballsNumberText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI hitTypeText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI OffsetMs;

    //Private var for internal code
    private int ballsHitted = 0;
    private int totalPoints = 0;
    private int gettedPoints = 0;
    private int combo = 0;

    //Private var for timeOffset
    public float timeOffsetData
    {
        get { return timeOffset; }
    }

    private float distance;
    private float speed;
    private float timeOffset;
    private GameObject perfectPos;

    private void Start()
    {
        perfectPos = GameObject.FindGameObjectWithTag("PerfectArea");
    }

    //Adds 1 to the count of the balls hiutted. Displays it in the UI
    public void AddBall()
    {
        ballsHitted++;
        ballsNumberText.text = "Pelotas Golpeadas: " + ballsHitted;
    }

    //Adds the points obteined by hitting the balls according to the area type. Counts the combo and calculates de bonus points. Displays them in the UI
    public void AddPoints(AreaType type, GameObject ball)
    {
        distance = perfectPos.transform.position.z - ball.transform.position.z;
        speed = ball.GetComponent<Ball>().applyForce;
        timeOffset = distance / speed;

        switch (type)
        {
            case AreaType.Early:
                gettedPoints = EarlyPoints;
                combo = 0;
                break;

            case AreaType.Perfect:
                gettedPoints = PerfectPoints;
                combo++;

                if(combo >= 2)
                {
                    comboText.text = "¡¡¡COMBO DE " + combo + "!!!";
                    switch (combo)
                    {
                        case 2:
                            gettedPoints += PerfectPoints / 10;
                            break;

                        case 3:
                            gettedPoints += PerfectPoints / 5;
                            break;

                        case 4:
                            gettedPoints += PerfectPoints / 2;
                            break;

                        default:
                            gettedPoints += PerfectPoints;
                            break;
                    }
                }

                break;

            case AreaType.Late:
                gettedPoints = LatePoints;
                combo = 0;
                break;

            case AreaType.Miss:
                combo = 0;
                break;
        }

        totalPoints += gettedPoints;
        OffsetMs.text = (timeOffset * 1000).ToString("F2") + " ms";
        pointsText.text = "Puntos: " + totalPoints;
        hitTypeText.text = type.ToString();
    }
}
