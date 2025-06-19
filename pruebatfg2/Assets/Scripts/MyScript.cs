using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.IO;
using pb = global::Google.Protobuf;
using Cardboard;

public class MyScript : MonoBehaviour
{
    public string myJson = "";
    public string myJson2 = "";
    public string myJson3 = "";
    public string myJson4 = "";

    // Start is called before the first frame update
    void Start()
    {
        /* DeviceParams 1: Crear nuevo DP y guardar en fichero */

        DeviceParams myDP = new DeviceParams();

        myDP.Vendor = "Homido";
        myDP.Model = "Mini";
        myDP.ScreenToLensDistance = 0.05f;
        myDP.InterLensDistance = 0.064f;
        //myDP.LeftEyeFieldOfViewAngles = ;
        myDP.VerticalAlignment = 0;
        myDP.TrayToLensDistance = 0.032f;
        //myDP.DistortionCoefficients = ;
        //myDP.HasMagnet = false;  // No está definido en esta versión de DeviceParams
        myDP.PrimaryButton = 0;

        myJson = myDP.ToString(); // A formato JSON

        Stream s;
        if (File.Exists("Assets/prueba"))
            s = new FileStream("Assets/prueba", FileMode.Truncate);
        else
            s = new FileStream("Assets/prueba", FileMode.Create);
        pb::CodedOutputStream myOutput = new pb::CodedOutputStream(s);
        myDP.WriteTo(myOutput);
        //myOutput.Flush();
        myOutput.Dispose();

        Debug.Log("DeviceParams 1:" + myDP); // En formato JSON

        /* DeviceParams 2: Leer DP de fichero y modificar en memoria */

        DeviceParams myDP2 = new DeviceParams();

        Stream s2 = new FileStream("Assets/prueba", FileMode.Open);
        pb::CodedInputStream myInput = new pb::CodedInputStream(s2);
        myDP2 = DeviceParams.Parser.ParseFrom(myInput);
        myInput.Dispose();

        myDP2.InterLensDistance = 0.07f;
        myJson2 = myDP2.ToString(); // A formato JSON

        Debug.Log("DeviceParams 2:" + myDP2); // En formato JSON

        /* DeviceParams 3: Leer DP de fichero, convertir a string base 64, y voler a decodificar */

        DeviceParams myDP3 = new DeviceParams();

        //string path = "Assets/current_device_params"; // Da error con el parser
        string path = "Assets/prueba";
        byte[] myBytes = File.ReadAllBytes(path);
        string myBase64String = Convert.ToBase64String(myBytes);

        Debug.Log("myBase64String:" + myBase64String);

        byte[] myBase64EncodedBytes = Convert.FromBase64String(myBase64String);
        myDP3 = DeviceParams.Parser.ParseFrom(myBase64EncodedBytes);
        myJson3 = myDP3.ToString(); // A formato JSON

        Debug.Log("DeviceParams 3:" + myDP3); // En formato JSON

        /* DeviceParams 4: Decodificar string base 64, modificar, guardar en fichero, volver a leer y convertirlo de nuevo en string base 64 */

        DeviceParams myDP4 = new DeviceParams();

        //byte[] myBase64EncodedBytes4 = Convert.FromBase64String("CgZIb21pZG8SBE1pbmkdzcxMPSVvEoM9NW8SAz1QAFgAYAA=");
        byte[] myBase64EncodedBytes4 = Convert.FromBase64String("CgZIb21pZG8SDUhvbWlkbyAibWluaSIdhxZZPSW28309KhAAAEhCAABIQgAASEIAAEhCWAE1KVwPPToIexQuPs3MTD1QAGAC");
        //byte[] myBase64EncodedBytes4 = Convert.FromBase64String("CgtWUlN0cmVhbS5mchILSG9taWRvIG1pbmkdrkdhPSX8qXE9KhAAAEhCAABIQgAASEIAAEhCWAI1KVwPPToIMzOzPs3MzL1QAGAC");
        myDP4 = DeviceParams.Parser.ParseFrom(myBase64EncodedBytes4);
        myJson4 = myDP4.ToString(); // A formato JSON

        Debug.Log("DeviceParams 4:" + myDP4); // En formato JSON

        myDP4.InterLensDistance = 0.05f;

        Stream s4;
        if (File.Exists("Assets/prueba"))
            s4 = new FileStream("Assets/prueba", FileMode.Truncate);
        else
            s4 = new FileStream("Assets/prueba", FileMode.Create);
        pb::CodedOutputStream myOutput4 = new pb::CodedOutputStream(s4);
        myDP4.WriteTo(myOutput4);
        myOutput4.Dispose();

        Debug.Log("DeviceParams 4:" + myDP4); // En formato JSON

        string path4 = "Assets/prueba";
        byte[] myBytes4 = File.ReadAllBytes(path4);
        string myBase64String4 = Convert.ToBase64String(myBytes4);
        myJson4 = myDP4.ToString(); // A formato JSON

        Debug.Log("myBase64String4:" + myBase64String4);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
