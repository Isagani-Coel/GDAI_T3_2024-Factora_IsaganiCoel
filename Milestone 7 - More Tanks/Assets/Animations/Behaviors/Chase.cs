using UnityEngine;

public class Chase : NPCBaseFSM {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // rotate
        var direction = opponent.transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

        // chase
        // Debug.LogWarning("Enemy is chasing the player");
        NPC.transform.Translate(0f, 0f, Time.deltaTime * movSpeed);
    }
}
