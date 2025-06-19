using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.IO;
using Google.XR.Cardboard;
using pb = global::Google.Protobuf;
using Cardboard;

public class ModifyParams : MonoBehaviour
{
    GameObject scancube;
    string paramsbase64string;
    DeviceParams myDP = new DeviceParams();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scancube = GameObject.Find("ScanCube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Init()
    {
        
        paramsbase64string = scancube.GetComponent<UnshortenURL>().paramsstring;

        
        byte[] myBase64EncodedBytes = Convert.FromBase64String(paramsbase64string);
        myDP = DeviceParams.Parser.ParseFrom(myBase64EncodedBytes);


    }

    public void ModifyILD()
    {
        myDP.InterLensDistance = 0.05f;

        Stream s1;
        if (File.Exists("Assets/prueba"))
            s1 = new FileStream("Assets/prueba", FileMode.Truncate);
        else
            s1 = new FileStream("Assets/prueba", FileMode.Create);
        pb::CodedOutputStream myOutput = new pb::CodedOutputStream(s1);
        myDP.WriteTo(myOutput);
        myOutput.Dispose();

        string path = "Assets/prueba";
        byte[] myBytes = File.ReadAllBytes(path);
        string myBase64String = Convert.ToBase64String(myBytes);

        myBase64String = "http://google.com/cardboard/cfg?p=" + myBase64String;
        Api.SaveDeviceParams(myBase64String);
    }
}
