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
    string QrCode = string.Empty;

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
                    QrCode = Result.Text;
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
    }
}
