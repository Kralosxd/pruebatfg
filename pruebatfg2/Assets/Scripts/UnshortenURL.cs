using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Google.XR.Cardboard;

public class UnshortenURL : MonoBehaviour
{
    GameObject scancube;
    string myShortURL;
    public string myLongURL = string.Empty;
    public string goodurl = string.Empty;

    void Start()
    {
        //scancube = GameObject.Find("ScanCube");
        //myShortURL = scancube.GetComponent<NewScanQR>().QrCode;
        ////StartCoroutine(GetRequest("https://unshorten.me/s/" + myShortURL));
        //StartCoroutine(GetRequest("https://api.redirect-checker.net/?url=" + myShortURL));
    }

    public void Init()
    {
        scancube = GameObject.Find("ScanCube");
        myShortURL = scancube.GetComponent<NewScanQR>().QrCode;
        StartCoroutine(GetRequest("https://api.redirect-checker.net/?url=" + myShortURL));
    }

    IEnumerator GetRequest(string uri)
    {
        

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    myLongURL = webRequest.downloadHandler.text;

                    goodurl = myLongURL;
                    
                    if (!string.IsNullOrEmpty(goodurl)) {
                    goodurl = getBetween(goodurl, "http:\\/\\/google.com\\/cardboard\\/cfg?p=", "\"");
                    }

                    goodurl = "http://google.com/cardboard/cfg?p=" + goodurl;

                    //string[] urlsplit = myLongURL.Split('=');
                    //goodurl = "http://google.com/cardboard/cfg?p=" + urlsplit[1];
                    Api.SaveDeviceParams(goodurl);
                    break;
            }
        }
    }


    public static string getBetween(string strSource, string strStart, string strEnd) //obtiene un string con el link largo a partir de todo el texto que devuelve la peticion get
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length; //busca a partir de la posicion 0 la posicion de la cadena strStart y le suma su longitud
            End = strSource.IndexOf(strEnd, Start); //busca la posicion de strEnd a partir de Start
            return strSource.Substring(Start, End - Start); // empieza en start y longitud end-start
        }
        else { return "llega aqui"; }
    }

}