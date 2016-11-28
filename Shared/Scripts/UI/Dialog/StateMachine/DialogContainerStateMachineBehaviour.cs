using UnityEngine;

namespace TimiShared.UI {
    public class DialogContainerStateMachineBehaviour : StateMachineBehaviour {

        public DialogContainer DialogContainer {
            get; set;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        }


    }
}