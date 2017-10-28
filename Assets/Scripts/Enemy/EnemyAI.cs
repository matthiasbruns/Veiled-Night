﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : AiComponent, HasMovementAi {

	public EnemyAiConfig aiConfig;

	public AILerp movementAi;

    public Transform Target { 
		get {
			return movementAi.target; 
		} 
		set {
			if(movementAi.target != value){
				movementAi.target = value;
			}
			movementAi.SearchPath();
		} 
	}

    void Awake(){
		if(movementAi == null){
			movementAi = GetComponent<AILerp>();
			if(movementAi == null){
				movementAi = GetComponentInParent<AILerp>();
			}
		}

		if(aiConfig.idleState == null){
			 Debug.LogError("idleState is not set");
		}
		if(aiConfig.chaseState == null){
			 Debug.LogError("wanderState is not set");
		}
		if(aiConfig.attackState == null){
			 Debug.LogError("attackState is not set");
		}

		initialState = aiConfig.idleState;
		aiConfig.idleState.NextState = aiConfig.chaseState;
		aiConfig.chaseState.NextState = aiConfig.attackState;
		aiConfig.attackState.NextState = initialState;
	}
}