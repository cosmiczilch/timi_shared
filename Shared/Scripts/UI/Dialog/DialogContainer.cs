using UI.Dialog;
using UnityEngine;

namespace TimiShared.UI {
    public class DialogContainer : MonoBehaviour {

        [SerializeField] protected Animator _animator;
        [SerializeField] private Transform _holder;

        public enum TransitionType {
            Right,
            Left
        }
        private TransitionType _introTransitionType;
        private TransitionType _exitTransitionType;
        private IDialogTransitionsDelegate _transitionsDelegate;

        private const string kTriggerNameIntroLeft = "IntroLeft";
        private const string kTriggerNameIntroRight = "IntroRight";
        private const string kTriggerNameExitLeft = "ExitLeft";
        private const string kTriggerNameExitRight = "ExitRight";

        #region Public API
        public void Init(Transform childView, TransitionType introTransitionType, TransitionType exitTransitionType, IDialogTransitionsDelegate transitionsDelegate) {
            // Reparent the child view to ourselves
            childView.SetParent(this._holder, worldPositionStays: false);

            this._introTransitionType = introTransitionType;
            this._exitTransitionType  = exitTransitionType;
            this._transitionsDelegate = transitionsDelegate;
            DialogContainerStateMachineBehaviour[] stateMachineBehaviours = this._animator.GetBehaviours<DialogContainerStateMachineBehaviour>();
            if (stateMachineBehaviours != null) {
                for (int i = 0; i < stateMachineBehaviours.Length; ++i) {
                    stateMachineBehaviours[i].DialogContainer = this;
                }
            }
        }

        public void PlayIntroTransition() {
            switch (this._introTransitionType) {
                case TransitionType.Left:  this.SetTrigger(kTriggerNameIntroLeft);  break;
                case TransitionType.Right: this.SetTrigger(kTriggerNameIntroRight); break;
            }
        }

        public void PlayExitTransition() {
            switch (this._exitTransitionType) {
                case TransitionType.Left:  this.SetTrigger(kTriggerNameExitLeft);  break;
                case TransitionType.Right: this.SetTrigger(kTriggerNameExitRight); break;
            }
        }
        #endregion

        #region State machine behaviour functions
        public void OnIntroComplete() {
            this._transitionsDelegate.OnDialogShowComplete();
        }

        public void OnExitComplete() {
            this._transitionsDelegate.OnDialogHideComplete();
        }
        #endregion

        private void SetTrigger(string triggerName) {
            if (this._animator != null) {
                this._animator.SetTrigger(triggerName);
            }
        }

        private void ResetTriggers() {
            if (this._animator != null) {
                this._animator.ResetTrigger(kTriggerNameIntroLeft);
                this._animator.ResetTrigger(kTriggerNameIntroRight);
                this._animator.ResetTrigger(kTriggerNameExitLeft);
                this._animator.ResetTrigger(kTriggerNameExitRight);
            }
        }
    }
}