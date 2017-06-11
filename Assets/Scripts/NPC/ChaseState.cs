using UnityEngine;
using System.Collections;

public class ChaseState : FSMState {

    private GameObject thisGo;
    private Rigidbody thisRigidBody;
    private GameObject player;

    public ChaseState(GameObject thisGo, GameObject player)
    {
        stateId = FSMStateId.Chase;
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
        ChaseMove();
        CheckTransition();
    }

    private void ChaseMove()
    {
        thisRigidBody.velocity = thisGo.transform.forward * 5;
        Transform targetTrans = player.transform;
        Vector3 targeVec = targetTrans.position;
        targeVec.y = thisGo.transform.position.y;
        thisGo.transform.LookAt(targeVec);
    }

    private void CheckTransition()
    {
        if (Vector3.Distance(thisGo.transform.position, player.transform.position) > 8)
        {
            system.PerformTransition(FSMTransition.LostPlayer);
        }
    }
}
