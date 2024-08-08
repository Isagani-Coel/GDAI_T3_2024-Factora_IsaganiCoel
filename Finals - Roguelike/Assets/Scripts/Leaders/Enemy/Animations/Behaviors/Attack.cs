using UnityEngine;

public class Attack : NPCBaseFSM {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        NPC.GetComponent<Enemy>().StartAttacking();
        // Debug.Log("Started attacking");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        NPC.transform.LookAt(target.transform.position);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        NPC.GetComponent<Enemy>().StopAttacking();
        // Debug.Log("Stopped attacking");
    }
}
