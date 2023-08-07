using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveData : MonoBehaviour
{
    public string filepath;

    //Data variables for save
    private string fileID;

    private string nameID;
    private string age;
    private string gender;

    private string ballID;
    private string ballHitType;
    private string offsetTime;
    
    private int earlyHit = 0;
    private int perfectHit = 0;
    private int lateHit = 0;
    private int missHit = 0;

    private StreamWriter writer;
    private StreamReader reader;

    private void WriteData()
    {
        fileID = nameID + "_" + DateTime.Today;
    }

    private void AddLine()
    {

    }


    private void UploadFile()
    {

    }
}
