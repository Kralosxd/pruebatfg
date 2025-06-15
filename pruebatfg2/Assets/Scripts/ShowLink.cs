using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowLink : MonoBehaviour
{

     GameObject scancube;
    string qrurl;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    string longurl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scancube = GameObject.Find("ScanCube");
    }

    // Update is called once per frame
    void Update()
    {
        qrurl = scancube.GetComponent<NewScanQR>().QrCode;
        text1.SetText("QR Url: " + qrurl);
        text2.SetText("Long Url: " + longurl);
    }
}
