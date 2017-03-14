using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    /*public Transform player;   // Public variable to store a reference to the player game object

    public Vector3 offset;  // Public variable to store the offset distance between the player and camera

    private Vector3 currentOffset;

    public float smooth = 1;

    private RaycastHit hitInfo;

    private float maxDist;

    void Start()
    {
        maxDist = Vector3.Distance(Vector3.zero, offset);
    }

    void LateUpdate()
    {
        currentOffset = offset;
        //transform.position = player.position + offset;

        Vector3 heading =  (player.position + offset) - player.position;
        float dist = heading.magnitude;

        Vector3 dir = heading / dist;
        /*if(Physics.Raycast(player.position, dir, maxDist))
        {
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, player.transform.position.y, smooth),
                Mathf.Lerp(transform.position.y, player.transform.position.z, smooth));
        }

        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(transform.position.y, minDistance.y, player.position.y + offset.y),
            Mathf.Clamp(transform.position.z, minDistance.z, player.position.z + offset.z));

        transform.LookAt(player.position);

        if (Physics.Raycast(player.position, dir, out hitInfo, maxDist))
        {
            currentOffset.z = hitInfo.point.z - player.position.z;
        }
        else
            currentOffset.z = offset.z;

        //currentOffset.z = Mathf.Clamp(currentOffset.z, offset.z, offset.z/2);
        Debug.Log(currentOffset.z);
        Vector3 newPos = player.position + (Vector3.forward * currentOffset.z);
        newPos = newPos + (Vector3.up * currentOffset.y);
        transform.position = new Vector3(player.position.x, 
            Mathf.Lerp(transform.position.y , newPos.y, Time.deltaTime * smooth),
            Mathf.Lerp(transform.position.z, newPos.z, Time.deltaTime * smooth));

        transform.LookAt(player.position);
    }
}*/

    [Header("Camera Properties")]
    public float DistanceAway;                     //how far the camera is from the player.
    public float DistanceUp;                    //how high the camera is above the player
    public float smooth = 4.0f;                    //how smooth the camera moves into place
    private float currentSmooth;
    public float rotateAround = 180f;            //the angle at which you will rotate the camera (on an axis)
    [Header("Player to follow")]
    public Transform target;                    //the target the camera follows
    [Header("Layer(s) to include")]
    public LayerMask CamOcclusion;                //the layers that will be affected by collision
    [Header("Map coordinate script")]
    RaycastHit hit;
    float cameraHeight = 55f;
    float cameraPan = 0f;
    Vector3 camPosition;
    Vector3 camMask;
    Vector3 followMask;
    // Use this for initialization
    void Start()
    {
        //the statement below automatically positions the camera behind the target.
        rotateAround = target.eulerAngles.y - 45f;
    }
    void Update()
    {

    }
    // Update is called once per frame

    void LateUpdate()
    {
        //Offset of the targets transform (Since the pivot point is usually at the feet).
        Vector3 targetOffset = new Vector3(target.position.x, (target.position.y + 2f), target.position.z);
        Quaternion rotation = Quaternion.Euler(cameraHeight, rotateAround, cameraPan);
        Vector3 vectorMask = Vector3.one;
        Vector3 rotateVector = rotation * vectorMask;
        //this determines where both the camera and it's mask will be.
        //the camMask is for forcing the camera to push away from walls.
        camPosition = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;
        camMask = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;

        occludeRay(ref targetOffset);
        smoothCamMethod();

        transform.LookAt(target);
    }
    void smoothCamMethod()
    {
        transform.position = new Vector3(camPosition.x, Mathf.Lerp(transform.position.y, camPosition.y, Time.deltaTime * currentSmooth), 
            Mathf.Lerp(transform.position.z, camPosition.z, Time.deltaTime * currentSmooth));
    }
    void occludeRay(ref Vector3 targetFollow)
    {
        #region prevent wall clipping
        //declare a new raycast hit.
        RaycastHit wallHit = new RaycastHit();
        //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
        if (Physics.Linecast(targetFollow, camMask, out wallHit, CamOcclusion))
        {
            //the smooth is increased so you detect geometry collisions faster.
            currentSmooth = smooth;
            //the x and z coordinates are pushed away from the wall by hit.normal.
            //the y coordinate stays the same.
            camPosition = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
        else
            currentSmooth = 1000;
        #endregion
    }
}
