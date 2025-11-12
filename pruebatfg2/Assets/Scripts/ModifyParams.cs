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
    GameObject cube1;
    GameObject cube2;
    GameObject cube3;
    GameObject cube4;
    GameObject cube5;
    GameObject cube6;
    GameObject cube7;
    GameObject cube8;
    GameObject cube9;
    GameObject cube0;
    GameObject cubecoma;
    GameObject cubedel;
    public int numberclicked;
    public int waitarrow = 0;
    float changeild;
    public TextMeshProUGUI textdebug;

    public TextMeshProUGUI textildmanual;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scancube = GameObject.Find("ScanCube");
        arrow1 = GameObject.Find("arrow1");
        arrow2 = GameObject.Find("arrow2");
        cube1 = GameObject.Find("Cube1");
        cube2 = GameObject.Find("Cube2");
        cube3 = GameObject.Find("Cube3");
        cube4 = GameObject.Find("Cube4");
        cube5 = GameObject.Find("Cube5");
        cube6 = GameObject.Find("Cube6");
        cube7 = GameObject.Find("Cube7");
        cube8 = GameObject.Find("Cube8");
        cube9 = GameObject.Find("Cube9");
        cube0 = GameObject.Find("Cube0");
        cubecoma = GameObject.Find("Cubecoma");
        cubedel = GameObject.Find("Cubedel");
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

        //textdebug.SetText(paramsbase64string);

        byte[] myBase64EncodedBytes = Convert.FromBase64String(paramsbase64string); //no funciona con algunas cadenas
        
        myDP = DeviceParams.Parser.ParseFrom(myBase64EncodedBytes);
        myDP.InterLensDistance = (float)System.Math.Round(myDP.InterLensDistance, 4);
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


        MySaveParams();

        waitarrow = 0;
    }

    public void MySaveParams()
    {

        Stream s1;
        //if (File.Exists("Assets/prueba"))
        //    s1 = new FileStream("Assets/prueba", FileMode.Truncate);
        //else {
        //    s1 = new FileStream("Assets/prueba", FileMode.Create);
        //    textcurrentild.SetText("Current ILD: " + myDP);
        //}

        if (File.Exists(Application.persistentDataPath + "/prueba"))
        { //en android hay que usar Application.persistentDataPath para poder guardar archivos
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

        myBase64String = myBase64String.Replace("+", "-"); //se sustituyen de vuelta + con - y / con _ y se quitan los = para que funcione savedeviceparams
        myBase64String = myBase64String.Replace("/", "_");
        myBase64String = myBase64String.Replace("=", string.Empty);


        myBase64String = "http://google.com/cardboard/cfg?p=" + myBase64String;
        textdebug.SetText(myBase64String);
        Api.SaveDeviceParams(myBase64String);

    }

    public void ModILDNumber() //se llama desde numbercontroller
    {

        //int numero = cube1.GetComponent<NumberController>().numberclicked; //hay que usar el numberclicked de cada cube

        //changeild = myDP.InterLensDistance;
        //if (numero == 1) //quiza hacerlo tambien con un switch
        //{
        //    textildmanual.SetText(textildmanual.text + "1");

        //}

        if (textildmanual.text.Length < 2) { //para no poder escribir mas de dos numeros
        switch (numberclicked) //uso directamente numberclicked de este script en vez de cogerlo de numbercontroller
        {
            case 1:
                textildmanual.SetText(textildmanual.text + "1");
                break;
            case 2:
                textildmanual.SetText(textildmanual.text + "2");
                break;
            case 3:
                textildmanual.SetText(textildmanual.text + "3");
                break;
            case 4:
                textildmanual.SetText(textildmanual.text + "4");
                break;
            case 5:
                textildmanual.SetText(textildmanual.text + "5");
                break;
            case 6:
                textildmanual.SetText(textildmanual.text + "6");
                break;
            case 7:
                textildmanual.SetText(textildmanual.text + "7");
                break;
            case 8:
                textildmanual.SetText(textildmanual.text + "8");
                break;
            case 9:
                textildmanual.SetText(textildmanual.text + "9");
                break;
            case 0:
                textildmanual.SetText(textildmanual.text + "0");
                break;
            case 10:
                textildmanual.SetText(textildmanual.text + ",");
                break;
            //case 11:
            //    string s = textildmanual.text;
            //    s = s.Remove(s.Length - 1);
            //    textildmanual.text = s;
            //    break;
            //case 12:
            //    ModILDmanual();
            //    break;
            default:
                break;
        }
        }
        switch (numberclicked)
        {
            case 11:
                string s = textildmanual.text;
                s = s.Remove(s.Length - 1);
                textildmanual.text = s;
                break;
            case 12:
                ModILDmanual();
                break;
            default:
                break;
        }

        //for (int i = 0; i < 10; i++)
        //{
        //    if (numero == i)
        //    {
        //        textildmanual.SetText(textildmanual.text + i.ToString);
        //    }
        //}


    }


    public void ModILDmanual()
    {
        //myDP.InterLensDistance = Convert.ToSingle(textildmanual.text);

        string nuevoild = "0,0" + textildmanual.text;
        myDP.InterLensDistance = float.Parse(nuevoild);

        MySaveParams();
    }



}
