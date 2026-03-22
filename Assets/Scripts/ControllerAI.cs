using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum AIState
{
    ChooseRoamDirection,
    Unstuck,
    Roam,
    Attack,
    TurnAndShoot,
    Flee,
    Flank,
    Chase,
    Idle,
    Patrol
}

public abstract class ControllerAI : Controller
{
    protected Quaternion roamDirection = Quaternion.identity;
    protected float transitionChangeTime;
    public Transform target;
    public PawnTank pawnTank;
    public float fleeDistance = 10f;
    public float visionDistance = 15f;
    public float hearingDistance = 15f;
    public float FOVAngle = 60f;
    protected AIState currentState = AIState.Roam;
    public Transform[] waypoints;
    protected int currentWaypoint = 0;
    public float waypointTolerance = 1f;
    public bool loopWaypoints = true;
    protected Health health;

    public override void Start()
    {
        pawnTank = GetComponent<PawnTank>();
        health = GetComponent<Health>();
        health.OnDamaged += OnDamaged;
        if (GameManager.instance != null)
        {
            if (GameManager.instance.playerPawn != null)
                target = GameManager.instance.playerPawn.transform;
            else if (GameManager.instance.tanks.Count > 0)
                target = GameManager.instance.tanks[0].transform;
        }

        transitionChangeTime = Time.deltaTime;
        ChangeState(AIState.Idle);

    }
    public override void Update()
    {
        MakeDecisions();
    }

    public void ChangeState(AIState newState)
    {
        currentState = newState;
        transitionChangeTime = Time.time;
        if (newState == AIState.ChooseRoamDirection || newState == AIState.Unstuck)
            roamDirection = Quaternion.identity;
    }

    public override void MakeDecisions()
    {

    }

    public void DoIdle()
    {
        
    }

    // waitToAlign: when true (default) the pawn will rotate in place until aligned
    // with the chosen roam direction before entering the Roam state. When false,
    // it will immediately enter Roam and movement will occur while turning.
    public virtual void ChooseRoamDirection(bool waitUntilAligned = true)
    {
        if (roamDirection == Quaternion.identity)
        {
            float angle = Random.Range(0f, 360f);
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            roamDirection = Quaternion.LookRotation(dir);
        }

        Vector3 desiredDir = roamDirection * Vector3.forward;
        Vector3 lookTarget = pawn.transform.position + desiredDir;
        pawn.RotateTowards(lookTarget, pawn.turnSpeed);

        if (!waitUntilAligned)
        {
            ChangeState(AIState.Roam);
            return;
        }

        float angleToDir = Vector3.Angle(pawn.transform.forward, desiredDir);
        const float alignThreshold = 5f; // leeway in degrees
        if (angleToDir <= alignThreshold)
        {
            ChangeState(AIState.Roam);
        }
    }

    public virtual void DoRoam()
    {
        pawn.Move(Vector3.forward);
    }

    public void DoChase()
    {
        pawn.RotateTowards(target.position, pawn.turnSpeed);
        pawn.Move(Vector3.forward);
    }

    public void Seek(Vector3 position)
    {
        pawn.RotateTowards(position, pawn.turnSpeed);
        pawn.Move(Vector3.forward);
    }

    public void DoPatrol()
    {
        Transform[] pts = GetWaypoints();
        if (pts == null || pts.Length == 0)
            return;

        Transform wp = pts[currentWaypoint];

        // Rotate towards waypoint and move forward
        pawn.RotateTowards(wp.position, pawn.turnSpeed);
        pawn.Move(Vector3.forward);

        if (CanSee(target.gameObject) || CanHear(target.gameObject))
        {
            ChangeState(AIState.TurnAndShoot);
        }

        // Advance when close enough
        if (Vector3.Distance(pawn.transform.position, wp.position) <= waypointTolerance)
        {
            currentWaypoint++;
            if (currentWaypoint >= pts.Length)
            {
                if (loopWaypoints)
                    currentWaypoint = 0;
                else
                    currentWaypoint = pts.Length - 1;
            }
        }
    }

    protected Transform[] GetWaypoints()
    {
        if (waypoints != null && waypoints.Length > 0)
            return waypoints;
        return null;
    }

    public virtual void DoFlee()
    {
        Vector3 vectorToTarget = pawn.transform.position - target.position;
        float distanceToPlayer = vectorToTarget.magnitude;
        vectorToTarget.Normalize();

        float percentOfFleeDistance = distanceToPlayer / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;
        float newFleeDistance = flippedPercentOfFleeDistance * fleeDistance;

        Vector3 targetPosition = pawn.transform.position + (vectorToTarget * newFleeDistance);
        Seek(targetPosition);
    }

    public bool CanSee(GameObject target)
    {
        RaycastHit hit;
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // FOV check
        float angleToTarget = Vector3.Angle(vectorToTarget, pawn.transform.forward);
        if (angleToTarget > FOVAngle)
            return false;

        // LOS check
        if (Physics.Raycast(pawn.transform.position, vectorToTarget, out hit, visionDistance))
        {
            if (hit.collider.gameObject == target)
                return true;
        }
        return false;
        
    }

    public bool CanHear(GameObject target)
    {
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        if (targetNoiseMaker == null)
            return false;
        if (targetNoiseMaker.noiseVolume > 0)
        {
            float totalDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
            if (totalDistance <= targetNoiseMaker.noiseVolume + hearingDistance)
                return true;
        }
        return false;
    }

    public bool CanMoveForward(float distance)
    {
        // Raycast forward the distance to move in a frame draw
        // If that hits something, return false
        // Otherwise return true
        RaycastHit hit;
        Vector3 origin = pawn.transform.position + Vector3.up * 0.5f;
        Vector3 direction = pawn.transform.forward;
        float sphereRadius = 0.5f;

        // SphereCast forward to detect obstacles slightly off-center.
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, distance))
        {
            if (hit.collider != null && hit.collider.gameObject != pawn.gameObject)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsObjectInRange(Transform objectToCheck, float range)
    {
        // Find the distance between the pawn and the object to check
        // Compare to allowed 'range'
        if (Vector3.Distance(objectToCheck.position, pawn.transform.position) < range)
        {
            return true;
        }
        
        return false;
    }

    public bool IsRoamDirection()
    {
        if (roamDirection != Quaternion.identity)
        {
            return true;
        }

        return false;
    }

    public bool HasTimeElapsed(float seconds)
    {
        if (Time.time - transitionChangeTime >= seconds)
            return true;
        return false;
    }

    public virtual void OnDamaged(float amount, GameObject damageSource)
    {
        target = damageSource.transform;
        ChangeState(AIState.TurnAndShoot);
    }
}
