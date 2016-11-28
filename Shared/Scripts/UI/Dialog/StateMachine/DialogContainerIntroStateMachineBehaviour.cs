using UnityEngine;

namespace TimiShared.UI {
    public class DialogContainerIntroStateMachineBehaviour : DialogContainerStateMachineBehaviour {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.DialogContainer.OnIntroComplete();
        }


    }
}