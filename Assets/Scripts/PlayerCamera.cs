using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;
    public GameObject pawn;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public float offsetXMaximum;
    public float offsetXMinimum;
    public float offsetYMaximum;
    public float offsetYMinimum;
    public float offsetZMaximum;
    public float offsetZMinimum;
    
    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pawn.transform.position + pawn.transform.rotation * new Vector3(offsetX, offsetY, offsetZ);
    }

    public void SetRect(string playerID)
    {
        if (playerID == "p1" && GameManager.numOfPlayers == 2)
        {
            cam.rect = new Rect(0f, 0f, .5f, 1f);
        }
        else if (playerID == "p2")
        {
            cam.rect = new Rect(.5f, 0f, .5f, 1f);
        }
    }

    public void ChangeOffsetX(float amount)
    {
        offsetX = offsetX + amount;
        if (offsetX > offsetXMaximum)
        {
            offsetX = offsetXMaximum;
        } else if (offsetX < offsetXMinimum)
        {
            offsetX = offsetXMinimum;
        }
    }
    public void ChangeOffsetY(float amount)
    {
        offsetY = offsetY + amount;
        if (offsetY > offsetYMaximum)
        {
            offsetY = offsetYMaximum;
        } else if (offsetY < offsetYMinimum)
        {
            offsetY = offsetYMinimum;
        }
    }

    public void ChangeOffsetZ(float amount)
    {
        offsetZ = offsetZ + amount;
        if (offsetZ > offsetZMaximum)
        {
            offsetZ = offsetZMaximum;
        } else if (offsetZ < offsetZMinimum)
        {
            offsetZ = offsetZMinimum;
        }
    }

    public void ResetOffset()
    {
        offsetX = 0;
        offsetY = 50;
        offsetZ = 0;
    }
}
