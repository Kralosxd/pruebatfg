using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Text;
using System.IO;
using Google.XR.Cardboard;
using pb = global::Google.Protobuf;
using Cardboard;
using TMPro;

public class ModifyParams : MonoBehaviour
{
    GameObject scancube;
    string paramsbase64string;
    DeviceParams myDP = new DeviceParams();

    public TextMeshProUGUI textcurrentild;
    GameObject arrow1;
    GameObject arrow2;
    public int waitarrow = 0;
    float changeild;
    public TextMeshProUGUI textdebug;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scancube = GameObject.Find("ScanCube");
        arrow1 = GameObject.Find("arrow1");
        arrow2 = GameObject.Find("arrow2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Init()
    {
        
        paramsbase64string = scancube.GetComponent<UnshortenURL>().paramsstring;

        if (paramsbase64string.Length % 4 == 0) {   //la longitud de la cadena tiene que ser multiplo de 4 y si no lo es se rellena con =

        }
        else if (paramsbase64string.Length % 4 == 1) {
            paramsbase64string += "=";
        }
        else if (paramsbase64string.Length % 4 == 2) {
            paramsbase64string += "==";
        }

        paramsbase64string = paramsbase64string.Replace("-", "+"); //la cadena es base64url por lo que hay que sustituir los - con + y los _ con / para que funcione Convert
        paramsbase64string = paramsbase64string.Replace("_", "/");

        textdebug.SetText(paramsbase64string);

        byte[] myBase64EncodedBytes = Convert.FromBase64String(paramsbase64string); //no funciona con algunas cadenas
        
        myDP = DeviceParams.Parser.ParseFrom(myBase64EncodedBytes);
        textcurrentild.SetText("Current ILD: " + myDP.InterLensDistance);

    }

    public void ModifyILD()
    {
        waitarrow = 1;

        changeild = myDP.InterLensDistance;
        if (arrow1.GetComponent<ArrowController>().arrowclicked == 1) {
            //myDP.InterLensDistance += 0.001f;
            changeild += 0.001f;
        }
        else if (arrow2.GetComponent<ArrowController>().arrowclicked == 2) {
            //myDP.InterLensDistance -= 0.001f;
            changeild -= 0.001f;
        }
        //myDP.InterLensDistance = (float)System.Math.Round(myDP.InterLensDistance, 4);
        myDP.InterLensDistance = (float)System.Math.Round(changeild, 4);

        arrow1.GetComponent<ArrowController>().arrowclicked = 0;
        arrow2.GetComponent<ArrowController>().arrowclicked = 0;

        textcurrentild.SetText("Current ILD: " + myDP.InterLensDistance);
        

        Stream s1;
        //if (File.Exists("Assets/prueba"))
        //    s1 = new FileStream("Assets/prueba", FileMode.Truncate);
        //else {
        //    s1 = new FileStream("Assets/prueba", FileMode.Create);
        //    textcurrentild.SetText("Current ILD: " + myDP);
        //}

        if (File.Exists(Application.persistentDataPath + "/prueba")) { //en android hay que usar Application.persistentDataPath para poder guardar archivos
            s1 = new FileStream(Application.persistentDataPath + "/prueba", FileMode.Truncate);
        }
        else
        {
                s1 = new FileStream(Application.persistentDataPath + "/prueba", FileMode.Create);
            }

        pb::CodedOutputStream myOutput = new pb::CodedOutputStream(s1);
        myDP.WriteTo(myOutput);
        myOutput.Dispose();

        //string path = "Assets/prueba";
        string path = Application.persistentDataPath + "/prueba";
        byte[] myBytes = File.ReadAllBytes(path);
        string myBase64String = Convert.ToBase64String(myBytes);

        myBase64String = "http://google.com/cardboard/cfg?p=" + myBase64String;
        Api.SaveDeviceParams(myBase64String);

        waitarrow = 0;
    }

    
}
