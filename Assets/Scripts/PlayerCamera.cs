using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject pawn;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public float offsetZMaximum;
    public float offsetZMinimum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pawn.transform.position + pawn.transform.rotation * new Vector3(offsetX, offsetY, offsetZ);
    }

    public void ChangeOffset(float amount)
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
}
