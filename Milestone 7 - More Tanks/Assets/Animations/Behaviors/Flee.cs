using UnityEngine;

public class Flee : NPCBaseFSM {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // reused the chase logic and made an inverse to it flees instead

        // rotate
        var direction = -(opponent.transform.position - NPC.transform.position);
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

        // flee
        // Debug.LogWarning("Enemy is fleeing. Low HP");
        NPC.transform.Translate(0f, 0f, Time.deltaTime * movSpeed);
    }
}
