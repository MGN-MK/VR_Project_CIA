using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsSystemManager : MonoBehaviour
{
    public int EarlyPoints;
    public int PerfectPoints;
    public int LatePoints;

    public TextMeshProUGUI ballsNumberText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI hitTypeText;
    public TextMeshProUGUI comboText;

    private int ballsHitted = 0;
    private int totalPoints = 0;
    private int gettedPoints = 0;
    private int combo = 0;

    public void AddBall()
    {
        ballsHitted++;
        Debug.Log(ballsHitted);
        ballsNumberText.text = "Pelotas Golpeadas: " + ballsHitted;
    }

    public void AddPoints(AreaType type)
    {
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
        }


        totalPoints += gettedPoints;
        pointsText.text = "Puntos: " + totalPoints;
        hitTypeText.text = type.ToString();
    }
}
