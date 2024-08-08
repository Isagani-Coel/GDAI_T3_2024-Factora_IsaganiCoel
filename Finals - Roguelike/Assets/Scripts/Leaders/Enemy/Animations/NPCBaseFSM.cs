using UnityEngine;

/* -NOTES- 
    (07.24.24)
    - has exit time -> finishes the animation before moving to the next one
    - you can't manually assign a reference from the inspector
    - you have to assign it on run-time
*/

public class NPCBaseFSM : StateMachineBehaviour {

    public GameObject NPC, target;
    public float movSpeed, rotSpeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        NPC = animator.gameObject;
        target = NPC.GetComponent<Enemy>().GetTarget();

        movSpeed = NPC.GetComponent<Enemy>().GetMovSpeed();
        rotSpeed = NPC.GetComponent<Enemy>().GetRotSpeed();
    }
}
