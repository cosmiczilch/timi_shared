using UnityEngine;

namespace TimiShared.UI {
    public class DialogContainerExitStateMachineBehaviour : DialogContainerStateMachineBehaviour {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.DialogContainer.OnExitComplete();
        }

    }
}