using UnityEngine;

public class Chase : NPCBaseFSM {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // rotate to the player
        Vector3 direction = target.transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, 
                                                  Quaternion.LookRotation(direction), 
                                                  rotSpeed * Time.deltaTime);

        NPC.transform.Translate(0f, 0f, movSpeed * Time.deltaTime);
        // Debug.Log("Chasing " + target.name);
    }
}
