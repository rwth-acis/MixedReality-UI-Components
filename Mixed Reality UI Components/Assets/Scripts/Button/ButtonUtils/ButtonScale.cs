using i5.Utilities.Debugging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace i5.MixedRealityUIComponents.Button
{

    public class ButtonScale : MonoBehaviour
    {
        [SerializeField] private Transform buttonBackground;
        [SerializeField] private Transform frame;
        [SerializeField] private Vector3 buttonSize = new Vector3(0.1f, 0.1f, 0.007f);
        [SerializeField] private float frameThickness = 1f;

        private SpriteRenderer frameRenderer;

        public Vector3 ButtonSize
        {
            get { return buttonSize; }
            set
            {
                buttonSize = value;
                UpdateSize();
            }
        }

        public float FrameThickness
        {
            get { return frameThickness; }
            set
            {
                frameThickness = value;
                UpdateSize();
            }
        }

        private void Awake()
        {
            CheckSetup();
            frameRenderer = frame.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Called in the editor if a variable in the inspector is changed
        /// </summary>
        private void OnValidate()
        {
            CheckSetup();
            if (frameRenderer == null)
            {
                frameRenderer = frame.GetComponent<SpriteRenderer>();
            }
            UpdateSize();
        }

        private void UpdateSize()
        {
            buttonBackground.localScale = buttonSize;
            if (frameThickness == 0)
            {
                frameThickness = 1;
            }
            frame.localScale = new Vector3(0.01f * frameThickness, 0.01f * frameThickness, 0.01f * frameThickness);
            frameRenderer.size = new Vector2(buttonSize.x * 100 / frameThickness, buttonSize.y * 100 / frameThickness);
        }

        private bool CheckSetup()
        {
            bool setUpCorrectly = true;
            if (buttonBackground == null)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(buttonBackground));
                setUpCorrectly = false;
            }
            if (frame == null)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(frame));
                setUpCorrectly = false;
            }
            else
            {
                if (frame.GetComponent<SpriteRenderer>() == null)
                {
                    DebugMessages.LogComponentNotFoundError(this, nameof(SpriteRenderer), frame.gameObject);
                    setUpCorrectly = false;
                }
            }

            return setUpCorrectly;

        }
    }

}