using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace i5.MixedRealityUIComponents.BoundingBox
{

    [CustomEditor(typeof(BoundingBoxScaler))]
    public class BoundingBoxEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BoundingBoxScaler scaler = (BoundingBoxScaler)target;
            if (GUILayout.Button("Adapt to Content"))
            {
                scaler.EncapsulateContent();
            }
        }
    }

}