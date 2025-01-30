using UnityEngine;
using System.Collections;
using System;

public class CamCube : MonoBehaviour
{

    //Objects
    private CamScript camScr; //Camera turn and rotation settings script
    public GameObject stateManager; //State manager script for managing states
    private stateManagerObj stMgr; 
    public GameObject inventoryObj;
    private Inventory inventory; //Inventory script
    public GameObject sphere;
    public GameObject Camera;
    public GameObject spawnPoint; //Where the sphere is spawned.
    private Rigidbody rb; //Sphere rigidbody
    private float ScrollVelocity = 2; //how fast you can turn the sphere
    private float velocity = 5;  //how was the sphere may move
    private float maxRotateVelocity = 15; //The maximum velocity the sphere may achieve without boosters
    public GameObject xPos2D; //The locked x-position of the sphere in 2D-mode
    //

    //Mouse and Arrow axes.
    private Vector3 moveSphere; //Input turned into sphere move data
    private Vector2 rotateCamCubeXAng; //CamCube rotation on the X-axis
    private Vector3 camForward; //camera view vector used for defining how to move the sphere based on the camera z-axis
    //

    //Constants
    private const float jumpPower = 2f; //The power to be added to a jump
    //

    //Game Initialization
    void Start()
    {
        //Game setup
        stMgr = stateManager.GetComponent<stateManagerObj>();
        stMgr.setState(States.Normal); //Set to normal state
        inventory = inventoryObj.GetComponent<Inventory>();
        //Camera setup
        camScr = new CamScript(transform.position, sphere.transform.position, transform.rotation, "back");
        //Sphere setup
        rb = sphere.GetComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().maxAngularVelocity = maxRotateVelocity;
        sphere.transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        getItemUse(); //Check if player uses item
        getInput(); //Get player input       
    }

    //Update based on "frame-speed" per frame. 
    void FixedUpdate()
    {
            switch (stMgr.getCurrentState()) //Check for the current State and change transform.position
            {
                case States.Gravity:
                    translateGravity();
                    break;
                case States.twoD:
                    translate2D();
                    break;
                case States.FlipGravity:
                    translateFlipGravity();
                break;
                case States.Normal:
                    translateNormal();
                break;
        }
    }

    //Update at the end
    void LateUpdate()
    {
        if (stMgr.stateChanged) //If state was changed, reset settings
        {
            resetStateSettings();
            stMgr.stateChanged = false;
        }
        switch (stMgr.getCurrentState()) //Change camera or physics based on state
        {
            case States.Zero:
                {
                    Physics.gravity = new Vector3(0, transform.position.y, 0);
                }
                break;
            case States.Gravity:
                {
                    transform.position = sphere.transform.position + camScr.getCamOffset("back");
                }
                break;
            case States.twoD:
                {
                    if (transform.rotation != camScr.getCamRotation("right"))
                    {
                        StartCoroutine(smoothCamRotation("right", 500f));
                        StartCoroutine(rotateCamOffset("right", 50f));

                    }
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    sphere.transform.localScale = new Vector3(.1f, 1, 1);
                    transform.position = sphere.transform.position + camScr.getCamOffset("right");
                }
                break;
            case States.FlipGravity:
                {
                    Physics.gravity = new Vector3(0, 9.8f, 0);
                    transform.position = sphere.transform.position + camScr.cameraFollowOffset;
                    cameraFollowNormal(Vector3.down);
                }
                    break;
            case States.Normal:
                {
                    transform.position = sphere.transform.position + camScr.cameraFollowOffset;
                    cameraFollowNormal(Vector3.up);
                }
                break;
            default: //Pause
                break;

        }
    }


    //Rotates camera smoothly from one position to another based on input speed.
    //@Param string side - Side to rotate to based on camScript
    //@Param float speed - The speed it should rotate at
    private IEnumerator smoothCamRotation(string side, float speed) 
    {
        Quaternion newRotation = camScr.getCamRotation(side);
        while (transform.rotation != newRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, speed * Time.deltaTime);
            yield return null;
        }
    }

    //Rotates camera smoothly from one side of an object to another based on input speed.
    //@Param string side - Side to rotate to based on camScript
    //@Param float speed - The speed it should rotate at
    private IEnumerator rotateCamOffset(string side, float speed)
    {
        while (camScr.cameraFollowOffset != camScr.getCamOffset(side))
        {
            camScr.cameraFollowOffset = Vector3.MoveTowards(camScr.cameraFollowOffset, camScr.getCamOffset(side), speed * Time.deltaTime);
            yield return null;
        }
    }

    //resetStateSettings()
    //Resets transform settins to avoid camera problems
    private void resetStateSettings()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        sphere.transform.localScale = new Vector3(1, 1, 1);
        if (stMgr.getState(States.twoD))
        {
            sphere.transform.rotation = camScr.getCamRotation("back"); //Make 2D Transition look better
            sphere.transform.position = new Vector3(xPos2D.transform.position.x, sphere.transform.position.y, sphere.transform.position.z);
        }
        else if (stMgr.getState(States.FlipGravity)) {
            transform.rotation = camScr.getCamRotation("back");
            camScr.cameraFollowOffset = camScr.initPosVector;
            transform.rotation = camScr.getRotationUpsideDown();
            camScr.cameraFollowOffset = camScr.cameraFollowOffset + new Vector3(0, -2 * camScr.cameraFollowOffset.y, 0);
        }
        else
        { // Because "gravity" seems to mess with rotation
            transform.rotation = camScr.getCamRotation("back");
            camScr.cameraFollowOffset = camScr.initPosVector;
        }
        rb.constraints = RigidbodyConstraints.None;
    }


    //Calculates camera offset with X-axis camera-rotation enabled.
    //@param Vector3 yPerspective - The vector the camera should rotate around
    //Vector3.down for upside-down or Vector3.up for normal
    private void cameraFollowNormal(Vector3 yPerspective)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(sphere.transform.position, yPerspective, -200 * Time.deltaTime);
            camScr.cameraFollowOffset = transform.position - sphere.transform.position;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(sphere.transform.position, yPerspective, 200 * Time.deltaTime);
            camScr.cameraFollowOffset = transform.position - sphere.transform.position;
        }
    }

    //getItemUse()
    //Use items based on numeric key or space input
    private void getItemUse()
    {
        Items items = Items.Zero;
        States state = States.Zero;
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            items = Items.FlipGravity;
            state = States.FlipGravity;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            items = Items.TwoD;
            state = States.twoD;
        }
        ////
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //A player can only jump if a charged Jump item exists.
            items = Items.Jump;
            if (inventory.useItemType(Items.Jump))
            {
                saltar();
            }
        }
        else if (items != Items.Zero) {
            if (inventory.tryPauseItem(items)) stMgr.setState(States.Normal);
            else if (inventory.useItemType(items)) stMgr.setState(state);
        }
    }

    //Saltar = jump in spanish. Unity reserved "jump"
    //This method adds jump power to an object this script is connected to.
    private void saltar()
    {
        if (stMgr.getState(States.FlipGravity))
        {
            rb.AddForce(Vector3.down * jumpPower, ForceMode.Impulse);
        }
        else {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        
    }

    //Record normal input for controlling the object this script is connected to
    private void getInput()
    {
        float xPos = Input.GetAxis("Horizontal");
        float zPos = Input.GetAxis("Vertical");
        moveSphere = new Vector3(xPos, 0, zPos);
        float mxPos = Input.GetAxis("Mouse X");
        rotateCamCubeXAng = new Vector2(mxPos, 0);
    }

    //Change transform settings for the objects while in Normal state (States.Normal)
    //Calculates the move based on camera Z-axis
    private void translateNormal()
    {
        // calculate camera relative direction to move:
        Transform cam = transform;
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = (moveSphere.z * camForward + moveSphere.x * cam.right).normalized;
        rb.AddTorque(new Vector3(move.z, 0, -move.x) * velocity);
    }

    //Change transform settings for the objects while in FlipGravity state (States.FlipGravity)
    //Calculates the move based on camera Z-axis
    private void translateFlipGravity()
    {
        // calculate camera relative direction to move:
        Transform cam = transform;
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = (-moveSphere.z * camForward - moveSphere.x * cam.right).normalized;
        rb.AddTorque(new Vector3(move.z, 0, -move.x) * velocity);
    }

    //Change transform settings for the objects while in 2D state (States.TwoD)
    private void translate2D()
    {
        //Sphere
        float move = moveSphere.x;
        rb.AddForce(0, 0, moveSphere.x * velocity * 100 * Time.deltaTime);
    }

    //Change transform settings for the objects while in Gravity/Secret state (States.Gravity)
    private void translateGravity()
    {
        //Sphere
        Vector3 change = Physics.gravity + new Vector3(moveSphere.x, 0,0).normalized;
        if (change.x < 9.8f && change.x > -9.8)
        {
            Physics.gravity = change;
            rb.AddForce(Physics.gravity.x + moveSphere.x / 2, 0, velocity/2);
            transform.rotation = Quaternion.Euler(0, 0, change.x * ScrollVelocity);
        }
    }
}
