using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is used to make the camera follow the player and zoom out when in the desert.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-11-28
public class CameraFollow : MonoBehaviour
{
    #region Public Variables
    public Transform target; //the target we want to follow
    public Transform zoomOut; //the camera will zoom out past this point
    public Transform zoomIn; //the camera will zoom in past this point

    #endregion

    #region Private Variables
    private Camera cam; //main camera of the game
    private float xOffset; //the x offset of the camera
    private float yOffset; //the y offset of the camera
    private bool inDesert; //whether or not the player is in the desert

    #endregion

    #region Methods

    /// <summary>
    /// initialize the camera
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-28
    private void Start()
    {
        cam = GetComponent<Camera>(); //get the camera component
    }

    /// <summary>
    /// Update handles the camera movement and camera size.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-28
    void Update()
    {
        //follow the player
        //if the player is in the desert we ignore y axis of player.
        if(inDesert)
        {
            transform.position = new Vector3(target.position.x + xOffset, yOffset, transform.position.z);
        }else{
            transform.position = new Vector3(target.position.x + xOffset, target.position.y + yOffset, transform.position.z);
        }
        

        //zoom camera out.
        if(transform.position.x > zoomOut.position.x && transform.position.x < zoomIn.position.x)
        {
            inDesert = true;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10, Time.deltaTime);
            yOffset = Mathf.Lerp(transform.position.y, -5f, Time.deltaTime);
        }
        //zoom camrea in
        if(transform.position.x < zoomOut.position.x)
        {
            inDesert = false;
            yOffset = 0;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7, Time.deltaTime);
            
        }
        //zoom camera in
        if(transform.position.x > zoomIn.position.x)
        {
            inDesert = false;
            yOffset = 0;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7, Time.deltaTime);
        }
       
    }
    
    #endregion
}
