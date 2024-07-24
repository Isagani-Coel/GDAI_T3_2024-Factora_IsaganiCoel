using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { IDLE, RUNNING, CASTING }

public class AIControl : MonoBehaviour {

    public Transform player;
    Animator animator;

    float rotationSpeed = 2.0f;
    float speed = 2.0f;
    float visionDist = 20.0f;
    float visionAngle = 30.0f;
    float castRange = 5.0f;

    State state;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void LateUpdate() {
        Vector3 dir = player.position - transform.position;
        float angle = Vector3.Angle(dir, transform.forward);

        if (dir.magnitude < visionDist && angle < visionAngle) {
            dir.y = 0;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

            if (dir.magnitude > castRange) {
                if (state != State.RUNNING) {
                    state = State.RUNNING;
                    animator.SetTrigger("IsRunning");
                }
            }
            else  {
                if (state != State.CASTING) {
                    state = State.CASTING;
                    animator.SetTrigger("IsCasting");
                }
            }
        }
        else {
            if (state != State.IDLE) {
                state = State.IDLE;
                animator.SetTrigger("IsIdle");
            }
        }

        if (state == State.RUNNING) 
            transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
