﻿using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace i5.MixedRealityUIComponents.Button
{

    public class FocusableCheckButton : FocusableButton
    {
        private bool buttonChecked;
        private Renderer diodeRenderer;
        private Color onColor = new Color(0, 0.529f, 0, 1f);

        public bool ButtonChecked
        {
            get { return buttonChecked; }
            set
            {
                buttonChecked = value;

                if (diodeRenderer == null)
                {
                    diodeRenderer = transform.Find("LED").GetComponent<Renderer>();
                }

                if (buttonChecked)
                {
                    diodeRenderer.material.SetColor("_EmissiveColor", onColor);
                }
                else
                {
                    diodeRenderer.material.SetColor("_EmissiveColor", Color.black);
                }
            }
        }

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            if (ButtonEnabled)
            {
                ButtonChecked = !ButtonChecked;
            }
            base.OnInputClicked(eventData);
        }
    }

}