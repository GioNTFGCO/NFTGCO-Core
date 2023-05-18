using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace NFTGCO
{
    public class BlurScreenController : MonoBehaviour
    {
        public System.Action<bool> OnBlurBackground;

        [SerializeField] private Volume _volume;

        [Space]
        [SerializeField] private NFTGCO.Helpers.InspectorButton EnableBlurButton = new NFTGCO.Helpers.InspectorButton("EnableBlur");
        [SerializeField] private NFTGCO.Helpers.InspectorButton DisableBLurButton = new NFTGCO.Helpers.InspectorButton("DisableBLur");

        private void OnEnable()
        {
            OnBlurBackground += BlurBackground;
        }
        private void OnDisable()
        {
            OnBlurBackground -= BlurBackground;
        }

        private void BlurBackground(bool state)
        {
            if (_volume == null)
                return;

            DepthOfField blurDOF;
            if (_volume.profile.TryGet<DepthOfField>(out blurDOF))
            {
                blurDOF.active = state;
            }
        }

        private void EnableBlur()
        {
            BlurBackground(true);
        }
        private void DisableBLur()
        {
            BlurBackground(false);
        }
    }
}