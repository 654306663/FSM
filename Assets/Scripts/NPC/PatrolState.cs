using UnityEngine;
using System.Collections;

public class PatrolState : FSMState {

    private int wayPointIndex;
    private Transform[] wayPoints;
    private GameObject thisGo;
    private Rigidbody thisRigidBody;
    private GameObject player;

    public PatrolState(Transform[] points, GameObject thisGo, GameObject player)
    {
        stateId = FSMStateId.Patrol;
        wayPoints = points;
        this.thisGo = thisGo;
        thisRigidBody = thisGo.GetComponent<Rigidbody>();
        this.player = player;
    }

    public override void DoBeforeEnter()
    {
        Debug.Log("DoBeforeEnter");
    }

    public override void DoUpdate()
    {
        PatrolMove();
        CheckTransition();
    }

    private void PatrolMove()
    {
        thisRigidBody.velocity = thisGo.transform.forward * 3;
        Vector3 targetVec = wayPoints[wayPointIndex].position;
        targetVec.y = this.thisGo.transform.position.y;
        thisGo.transform.LookAt(targetVec);
        if (Vector3.Distance(targetVec, thisGo.transform.position) < 1)
        {
            wayPointIndex++;
            wayPointIndex %= wayPoints.Length;
        }
    }

    private void CheckTransition()
    {
        if (Vector3.Distance(thisGo.transform.position, player.transform.position) < 3)
        {
            system.PerformTransition(FSMTransition.SawPlayer);
        }
    }
}
