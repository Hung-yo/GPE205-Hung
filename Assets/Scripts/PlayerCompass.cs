using UnityEngine;

public class PlayerCompass : MonoBehaviour
{
    public GameObject compass;
    private PawnTank pawnTank;
    public GameObject pawn;
    public Pawn otherPawn;
    public string playerID;
    public Vector3 compassOffset;
    public float elevationOffset = 0.6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pawnTank = GetComponent<PawnTank>();
        if (pawnTank != null)
            playerID = pawnTank.playerID;

        if (compass != null)
            compass.SetActive(GameManager.numOfPlayers > 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (string.IsNullOrEmpty(playerID) && pawn != null)
        {
            PawnTank pt = pawn.GetComponent<PawnTank>();
            if (pt != null)
                playerID = pt.playerID;
        }

        if (otherPawn == null && GameManager.instance != null && GameManager.instance.tanks != null)
        {
            foreach (Pawn p in GameManager.instance.tanks)
            {
                if (p == null) continue;
                PawnTank pt = p.GetComponent<PawnTank>();
                if (pt == null) continue;
                if (!string.IsNullOrEmpty(playerID) && pt.playerID == playerID) continue;
                otherPawn = p;
                break;
            }
        }

        if (pawn == null || compass == null) return;

        float downOffset = 0.5f;
        Collider col = pawn.GetComponent<Collider>();
        if (col != null)
            downOffset = col.bounds.extents.y + 0.05f;
        else
        {
            Renderer rend = pawn.GetComponentInChildren<Renderer>();
            if (rend != null)
                downOffset = rend.bounds.extents.y + 0.05f;
        }

        float finalDown = Mathf.Max(0.05f, downOffset - elevationOffset);
        compass.transform.position = pawn.transform.position - Vector3.up * finalDown + compassOffset;

        if (otherPawn != null)
        {
            Vector3 dir = otherPawn.transform.position - pawn.transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion target = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(90f, 0f, 0f);
                compass.transform.rotation = target;
            }
        }
    }
    
}
