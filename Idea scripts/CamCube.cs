using UnityEngine;
using System.Collections;

public class CamCube : MonoBehaviour
{

    //Objects
    public GameObject sphere;
    public GameObject Camera;
    private Rigidbody rb;
    private float ScrollVelocity = 2;
    private float velocity = 2;
    //

    //Mouse and Arrow axes.
    private Vector3 moveSphere;
    private Vector2 rotateCamCubeXAng;
    private Vector2 rotateCamYAng;
    float jump;
    //

    //Variables
    float yStatus = 0;
    bool isGrounded = false;
    //

    //Constants
    private Vector3 cameraFollowDist = new Vector3(0, 1, -3);
    //


    // Use this for initialization
    void Start()
    {
        rb = sphere.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        translate();
    }

    public void OnCollisionStay(Collision col)
    {
        isGrounded = true;
    }

    public void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }

    private void getInput()
    {
        float xPos = Input.GetAxis("Horizontal");
        float zPos = Input.GetAxis("Vertical");
        float yPos = 0;
        if (Input.GetKeyUp(KeyCode.Space)) //&& isGrounded)
        {
            StartCoroutine(jumpNow());
        }
        moveSphere = new Vector3(xPos * velocity, 0, zPos * velocity);
        float mxPos = Input.GetAxis("Mouse X");
        rotateCamCubeXAng = new Vector2(mxPos, 0);
        float myPos = Input.GetAxis("Mouse Y");
        rotateCamYAng = new Vector2(0, myPos);
    }

    private void translate()
    {
        //Camera
        if (!yOutOfBounds(rotateCamYAng.y))
        {
            Camera.transform.Rotate(rotateCamYAng.y * ScrollVelocity, 0, 0);
            yStatus += rotateCamYAng.y;
        }
        //CamCube
        transform.Rotate(0, rotateCamCubeXAng.x * ScrollVelocity, 0);
        //Sphere
        rb.AddForce(moveSphere);
        transform.position = sphere.transform.position + cameraFollowDist;

    }

    private bool yOutOfBounds(float myPos)
    {
        if (yStatus + myPos > 10 || yStatus + myPos < -30) return true;
        return false;
    }

    private IEnumerator jumpNow()
    {
        float vari = Mathf.LerpAngle(sphere.transform.position.y, sphere.transform.position.y + 12, .17f);
        sphere.transform.position = new Vector3(sphere.transform.position.x, vari, sphere.transform.position.z);
        yield return null;
    }
}
