using UnityEngine;

/* -NOTES- (07.24.24)
     - has exit time -> finishes the animation before moving to the next one
 
*/
public class NPCBaseFSM : StateMachineBehaviour { // can't manually assign a reference from the inspector

    public GameObject NPC, opponent;
    public float movSpeed = 2f, rotSpeed = 1f, accuracy = 3f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        NPC = animator.gameObject;
        opponent = NPC.GetComponent<TankAI>().GetPlayer();
    }
}
