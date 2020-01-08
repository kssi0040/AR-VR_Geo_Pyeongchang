using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollRectSnap : MonoBehaviour
{
    public RectTransform panel;                             // to hold the scroll panel;
    public Button[] aButtons;

    public GameObject Sphere;
    public Texture[] Textures;
    public string[] Names;
    public new Text name;
    public new Camera camera;
    public int Temp;

    public RectTransform center;                           // center to compare the distance for each button

    private float[] aDistances;                               // all button's distance to the center..
    private bool bDragging = false;                          // will be true,  while we drag the panel
    private int iBtnDistance;                                   // will hold the distance between the buttons
    private int iMinButtonNum;                               // to hold the number of the button, with smallest distance to center

    public GameObject NoticePopup;

    // Start is called before the first frame update
    void Start()
    {
        int iBtnLength = aButtons.Length;
        aDistances = new float[iBtnLength];
        // get distance between buttons
        iBtnDistance = (int)Mathf.Abs(aButtons[1].GetComponent<RectTransform>().anchoredPosition.x - aButtons[0].GetComponent<RectTransform>().anchoredPosition.x);        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < aButtons.Length; ++i)
        {
            aDistances[i] = Mathf.Abs(center.transform.position.x - aButtons[i].transform.position.x);            
        }

        float minDistance = Mathf.Min(aDistances);                              // Get the min Distance        
        for (int a = 0; a < aButtons.Length; ++a)
        {
            if(minDistance == aDistances[a])
            {
                /*
                Temp = iMinButtonNum;
                iMinButtonNum = a;          
                if(Temp!=iMinButtonNum)
                {
                    if(camera!=null)
                    {
                        camera.transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                }
                */
                if (iMinButtonNum < Textures.Length)
                {
                    Sphere.GetComponent<Renderer>().material.mainTexture = Textures[iMinButtonNum];
                }
                if (iMinButtonNum < Names.Length)
                    name.text = Names[iMinButtonNum];
            }
        }

        if(!bDragging)
        {
            LerpToButton(iMinButtonNum * -iBtnDistance);
        }
    }


    void LerpToButton(int position)
    {
        float fPosX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(fPosX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
    }

    //================================================ EVENT TRIGGER ==================================================//
    // Event Trigger 에서 call...
    public void StartDrag()
    {
        bDragging = true;
    }

    public void EndDrag()
    {
        bDragging = false;
    }

    public void LeftButtonDown()
    {
        iMinButtonNum--;
        if (iMinButtonNum < 0)
        {
            iMinButtonNum = 0;
        }
    }

    public void RightButtonDown()
    {
        iMinButtonNum++;
        if (iMinButtonNum >= Names.Length - 1)
        {
            iMinButtonNum = Names.Length - 1;
        }
    }

    public void NoticeButtonEvent()
    {
        NoticePopup.SetActive(false);
    }

    // stage button....
    public void StageButtonEvent()
    {
        if (0 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                SceneManager.LoadScene("Stage1");
            }
        }
        else if (1 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                SceneManager.LoadScene("Stage2");
            }
        }
        else if (2 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                SceneManager.LoadScene("GPS_Scene");
            }
        }
        else if (3 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                SceneManager.LoadScene("Record");
            }
        }
    }

    public void HomeButtonEvent()
    {
        SceneManager.LoadScene("Start");
    }


    public void MapButtonEvent()
    {
        if (0 == iMinButtonNum)
        {
            //36.895005, 126.206617
            string strUrl = "https://www.google.com/maps/place/%EB%8C%80%EA%B4%80%EB%A0%B9+%EC%82%BC%EC%96%91%EB%AA%A9%EC%9E%A5/@37.7227063,128.7180857,17z/data=!4m12!1m6!3m5!1s0x3561f6a6b0f28d7b:0x8dc24d7e4f39c48b!2z64yA6rSA66C5IOyCvOyWkeuqqeyepQ!8m2!3d37.72305!4d128.7196307!3m4!1s0x3561f6a6b0f28d7b:0x8dc24d7e4f39c48b!8m2!3d37.72305!4d128.7196307";
            Application.OpenURL(strUrl);
        }
        else if (1 == iMinButtonNum)
        {
            string strUrl = "https://www.google.com/maps/place/%EC%9D%98%EC%95%BC%EC%A7%80%EC%B2%AD%EB%85%84%ED%9A%8C%EC%98%81%EB%86%8D%EC%A1%B0%ED%95%A9%EB%B2%95%EC%9D%B8/@37.6884764,128.7113911,17z/data=!3m1!4b1!4m5!3m4!1s0x3561f4c0dc562875:0x5920eeaf7ba4afc3!8m2!3d37.6884722!4d128.7135798";
            Application.OpenURL(strUrl);
        }
        else if (2 == iMinButtonNum)
        {
            //36.845262, 126.196726            
            //string strUrl = "https://www.google.com/maps/place/36.835972,126.195911";
            //Application.OpenURL(strUrl);
        }
    }






}
