using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {

    private FSMSystem system;

    public Transform[] wayPoints;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

	    InitFSM();
	}

    void InitFSM()
    {
        system = new FSMSystem();

        PatrolState patrolState = new PatrolState(wayPoints, gameObject, player);
        ChaseState chaseState = new ChaseState(gameObject, player);

        // 注册切换条件 对应的 状态
        patrolState.AddTransition(FSMTransition.SawPlayer, FSMStateId.Chase);
        chaseState.AddTransition(FSMTransition.LostPlayer, FSMStateId.Patrol);

        system.AddState(patrolState);
        system.AddState(chaseState);

        system.Start(FSMStateId.Patrol);
    }

    void Update()
    {
        system.CurrentState.DoUpdate();
    }
}
