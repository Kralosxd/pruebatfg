using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

using Google.XR.Cardboard;

public class NewScanQR : MonoBehaviour
{

    WebCamTexture webcamTexture;
    public string QrCode = string.Empty;
    

    GameObject imagen;
    

    /// <summary>
    /// The material to use when this object is inactive (not being gazed at).
    /// </summary>
    public Material InactiveMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material GazedAtMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material ClickedMaterial;


    

    // Start is called before the first frame update
    void Start()
    {
        //imagen = GameObject.FindWithTag("ImagenCamara"); //el objeto rawimage con el tag imagencamara
        ////var renderer = GetComponent<RawImage>();
        //var renderer = imagen.GetComponent<RawImage>(); //coge el componente rawimage del objeto con el tag de imagencamara
        //webcamTexture = new WebCamTexture(512, 512);
        //renderer.texture = webcamTexture;
        ////renderer.material.mainTexture = webcamTexture;
        
    }




    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        GetComponent<Renderer>().material = GazedAtMaterial;
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        GetComponent<Renderer>().material = InactiveMaterial;
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        GetComponent<Renderer>().material = ClickedMaterial;

        QrCode = string.Empty; //reinicia la cadena para que se vuelva a iniciar la camara si quieres escanear otro codigo

        //if (GetComponent<UnshortenURL>().enabled == true)  // reinicia la cadena con la url larga y desactiva el script para que se vuelva a iniciar despues
        //{
            //GetComponent<UnshortenURL>().myLongURL = string.Empty;
            GetComponent<UnshortenURL>().goodurl = string.Empty;
            //GetComponent<UnshortenURL>().enabled = false;
        //}

        //Este fragmento de codigo va aqui en lugar de en start para que la primera vez que inicia la aplicacion funcione la camara porque si no
        // se ejecuta el start antes de aceptar el permiso de la camara y hay que reiniciar la aplicacion la primera vez que se ejecuta despues de instalarla
        imagen = GameObject.FindWithTag("ImagenCamara"); //el objeto rawimage con el tag imagencamara
        //var renderer = GetComponent<RawImage>();
        var renderer = imagen.GetComponent<RawImage>(); //coge el componente rawimage del objeto con el tag de imagencamara
        webcamTexture = new WebCamTexture(512, 512);
        renderer.enabled = true; //muestra el componente con la camara
        renderer.texture = webcamTexture;
        //renderer.material.mainTexture = webcamTexture;


        StartCoroutine(GetQRCode());
        
        //Api.SaveDeviceParams(uri);
        //Api.UpdateScreenParams();

    }

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text; //devuelve el enlace con https
                    QrCode = QrCode.Remove(4, 1); // borra la s de https para que al expandir el enlace devuelva la url correcta. Hay casos en los que no devuelve la correcta con http tampoco.
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
        webcamTexture.Stop();

        imagen.GetComponent<RawImage>().enabled = false;
        //GetComponent<UnshortenURL>().enabled = true; //activa el script que llama a la API para obtener la url larga
        GetComponent<UnshortenURL>().Init(); // llama a Init() para iniciar el script
    }
}
