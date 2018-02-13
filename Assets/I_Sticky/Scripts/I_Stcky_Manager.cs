using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;
using System.Linq;

public class I_Stcky_Manager : MonoBehaviour
 {
    private PlayAudio playAudio;

    public UnityEngine.UI.Image _imageViewer;
    public GameObject[] showHide;
    public GameObject unfocusImage;
    public GameObject save;
    public GameObject removeImage;
    public GameObject backBtn;
    public GameObject infoBtn;
    public GameObject screenShotBtn;
    public GameObject cameSwapBtn;
    public GameObject animateBtn;
    public static GameObject toScale;
    public float scaleFactor;
    public float scaleLimit;
    public static List<GameObject> ObjectsToHide;  

    public GameObject stickerBtn;

    [Space(10)]
    public GameObject[] objects;
    [Space(10)]
    public UserSettings userSettings;

    [HideInInspector]
    public static Plane raycastPlane;


    // private PlayAudio playAudio;
    private List<TrackableBehaviour> trackableObjects = new List<TrackableBehaviour>();

    private TrackableBehaviour[] lastTrackedObjects;
    private TrackableBehaviour currentTrackedTarget;
    [HideInInspector]
    public TrackableBehaviour currentTrackedPlayAudioTarget;
    private string currentTrackedTargetName;
    private bool scannedCurrentTrackable = true;
    public bool takingScreenShot = false;
    private int noOfActiveTrackables = 0;

    private float cardTrackingTimer = 0.0f;
    private Vector3 startScale;

    private bool takingScreenshot = false;

    bool toSave = false;
    bool showHideMenu = false;

   

    private CameraDevice.CameraDirection activeCameraDirection = CameraDevice.CameraDirection.CAMERA_BACK;

    void Start()
    {
        ObjectsToHide = new List<GameObject>();
        startScale = transform.localScale;
        foreach (GameObject i in showHide)
        {
            ObjectsToHide.Add(i);
            i.SetActive(false);
        }
        
        raycastPlane = new Plane(-Camera.main.transform.forward, Vector3.zero);
    }


    void Update()
    {

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            toScale.transform.localScale += startScale * deltaMagnitudeDiff * scaleFactor * -0.01f;
            toScale.transform.localScale = Vector3.Max(toScale.transform.localScale, startScale * scaleLimit / 10);
            toScale.transform.localScale = Vector3.Min(toScale.transform.localScale, startScale * scaleLimit);

        }
        lastTrackedObjects = new TrackableBehaviour[trackableObjects.Count];
        trackableObjects.CopyTo(lastTrackedObjects);
        trackableObjects.Clear();
        GetCurrentTrackableObjects();
        
        foreach (TrackableBehaviour trackable in lastTrackedObjects)
        {
            if (!trackableObjects.Contains(trackable))
            {
                if (trackable.GetComponentInChildren<TouchControls>())
                {
                    trackable.GetComponentInChildren<TouchControls>().objectVisible = false;
                }


                foreach (GameObject obj in objects)
                {
                    if (obj.name == currentTrackedTarget.TrackableName)
                    {
                      
                        obj.GetComponent<SnappedObject>().refObject.SetActive(true);

                        obj.GetComponent<SnappedObject>().InitTransform();

                    }

                }
                cardTrackingTimer = 0.0f;
              
                scannedCurrentTrackable = false;
            }

        }
    }

    void GetCurrentTrackableObjects()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();

        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();
        if (activeTrackables.Count() == 0 && !takingScreenshot) 
        {
            unfocusImage.SetActive(true);
            backBtn.SetActive(true);
            infoBtn.SetActive(false);
            /*infoBtn.SetActive(false);
            screenShotBtn.SetActive(false);
            cameSwapBtn.SetActive(false);
            animateBtn.SetActive(false);*/
            //stickerBtn.SetActive(false);
            //save.SetActive(false);
            //removeImage.SetActive(false);
        }
        else if(activeTrackables.Count()>=1)
        {
            takingScreenshot=true;
            infoBtn.SetActive(true);
        }
        
        
        else  
        {
            takingScreenShot = false;
            unfocusImage.SetActive(false);
            //infoBtn.SetActive(true);
            //backBtn.SetActive(false);
            /*infoBtn.SetActive(true);
            screenShotBtn.SetActive(true);
            cameSwapBtn.SetActive(true);
            animateBtn.SetActive(true);*/
            //stickerBtn.SetActive(true);
        }
        noOfActiveTrackables = activeTrackables.Count();
        foreach (TrackableBehaviour tb in activeTrackables)
        {

            //Debug.Log("Trackable: " + tb.TrackableName);



            currentTrackedTarget = tb;


            if (currentTrackedTarget.GetComponentInChildren<TouchControls>())
            {
                currentTrackedTarget.GetComponentInChildren<TouchControls>().objectVisible = true;
            }




            foreach (GameObject obj in objects)
            {
                GameObject ro = obj.GetComponent<SnappedObject>().refObject;

                if (ro != null)
                {
                    if (ro.activeSelf)
                    {
                        ro.SetActive(false);
                    }
                }
            }

            trackableObjects.Add(currentTrackedTarget);
            currentTrackedTargetName = currentTrackedTarget.TrackableName;

           
        }
    }

    public void ShowHideMenu ()
    {
       

        foreach (GameObject i in ObjectsToHide)
        {
            i.SetActive(true);
        }
        stickerBtn.SetActive(false);
    }
    public void ExitMenu()
    {


        foreach (GameObject i in ObjectsToHide)
        {
            i.SetActive(false);
        }
        stickerBtn.SetActive(true);
    }
    public void Return ()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void DownloadURL()
    {
        Application.OpenURL("https://www.google.com.eg/");
    }
    public void WebURL()
    {
        Application.OpenURL("https://play.google.com/store");
    }

    public void TakeScreenShot()
    {

        if(!takingScreenshot)
        {
            _imageViewer.gameObject.SetActive(false);
            stickerBtn.SetActive(false);
            save.SetActive(false);
            removeImage.SetActive(false);
            infoBtn.SetActive(false);
            screenShotBtn.SetActive(false);
            cameSwapBtn.SetActive(false);
            animateBtn.SetActive(false);
            backBtn.SetActive(false);
            unfocusImage.SetActive(false);

            
        }

        

       

        ScreenshotManager.SaveScreenshot("I-Sticky", "I_Sticky", "jpeg");
        takingScreenshot = true;
        //Hide UI

        
        
        
    }
    public void ShowScreenShot(Texture2D image,bool isSave)
    {
        if(image!=null && !isSave)
        {
            Sprite _saveImage = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
            // Show UI withoiut image viwer
            stickerBtn.SetActive(true);
            save.SetActive(true);
            removeImage.SetActive(true);
            infoBtn.SetActive(false);
            screenShotBtn.SetActive(false);
            cameSwapBtn.SetActive(false);
            animateBtn.SetActive(false);
            backBtn.SetActive(false);
            unfocusImage.SetActive(false);
    
            _imageViewer.sprite = _saveImage;
            //Show Image viewer 
            _imageViewer.gameObject.SetActive(true);
            
                
            
            
        }
       
       
    }

    public void RemoveTakenImage()
    {
        
        SceneManager.LoadScene("I-Sticky");
        
    }
    public void SaveImage()
    {

        /*  stickerBtn.SetActive(false);
          save.SetActive(false);
          removeImage.SetActive(false);
          foreach (GameObject i in showHide)
          {
              i.SetActive(true);
          }

      GameObject canvas = GameObject.Find("Canvas");
      foreach (var item in canvas.GetComponentsInChildren<Transform>())
      {
          item.gameObject.SetActive(false);
      }*/
        stickerBtn.SetActive(false);
        save.SetActive(false);
        removeImage.SetActive(false);
        infoBtn.SetActive(false);
        foreach (var item in ObjectsToHide)
        {
            item.SetActive(false);
        }
        //ShowScreenShot(gameObject.GetComponent<Texture2D>());

        // ScreenshotManager.SaveScreen();
        ScreenshotManager.SaveScreenshot("I-Sticky", "I_Sticky", "jpeg",true);
    
       // Invoke("RemoveTakenImage", 0.1f);
        
    }
   

        public void SwitchCameraMode()
    {
        if (activeCameraDirection == CameraDevice.CameraDirection.CAMERA_BACK)
            activeCameraDirection = CameraDevice.CameraDirection.CAMERA_FRONT;

        else if (activeCameraDirection == CameraDevice.CameraDirection.CAMERA_FRONT)
            activeCameraDirection = CameraDevice.CameraDirection.CAMERA_BACK;

        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        CameraDevice.Instance.Init(activeCameraDirection);
        CameraDevice.Instance.Start();
    }
    public void PlayInfoClips()
    {
        if (trackableObjects.Count == 1)
        {
            playAudio = currentTrackedTarget.gameObject.GetComponentInChildren<PlayAudio>();
            playAudio.PlayInfoClipOnClick();

            if (userSettings != null)
            {
                userSettings.FadeBGMusicVolume(playAudio.GetAudioSource().clip.length + 0.5f);
            }
        }
        

        
        
    }
}
