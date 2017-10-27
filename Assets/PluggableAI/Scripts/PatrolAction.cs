using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PluggableAI/Actions/Patrol")]
public class PatrolAction : Action 
{

	public override void Act (StateController controller)
	{
		Patrol (controller);
	}

	private void Patrol(StateController controller)
	{
		controller.navMeshAgent.destination = controller.wayPointList [controller.nextWaypoint].position;
		controller.navMeshAgent.Resume ();

		//check for if we have arrived at our destination
		if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
		{
			controller.nextWaypoint = (controller.nextWaypoint + 1) % controller.wayPointList.Count;
		}

	}
}
