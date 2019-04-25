using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using i5.Utilities.Debugging;

namespace i5.MixedRealityUIComponents.BoundingBox
{

    public class BoundingBoxScaler : MonoBehaviour
    {
        [SerializeField] private Transform[] xAxisWires;
        [SerializeField] private Transform[] yAxisWires;
        [SerializeField] private Transform[] zAxisWires;
        [SerializeField] private float wireThickness = 0.01f;

        [SerializeField] private Vector3 boundingBoxSize = Vector3.one;

        public float WireThickness
        {
            get
            {
                return wireThickness;
            }
            set
            {
                wireThickness = value;
                UpdateBoundingBox();
            }
        }

        public Vector3 BoundingBoxSize
        {
            get { return boundingBoxSize; }
            set
            {
                boundingBoxSize = value;
                UpdateBoundingBox();
            }
        }

        private void Awake()
        {
            CheckSetup();
        }

        private void UpdateBoundingBox()
        {
            if (boundingBoxSize.x == 0)
            {
                boundingBoxSize.x = 0.001f;
            }
            if (boundingBoxSize.y == 0)
            {
                boundingBoxSize.y = 0.001f;
            }
            if (boundingBoxSize.y == 0)
            {
                boundingBoxSize.z = 0.001f;
            }

            foreach (Transform xWire in xAxisWires)
            {
                UpdateWire(xWire, boundingBoxSize.x);
            }
            foreach (Transform yWire in yAxisWires)
            {
                UpdateWire(yWire, boundingBoxSize.y);
            }
            foreach (Transform zWire in zAxisWires)
            {
                UpdateWire(zWire, boundingBoxSize.z);
            }
        }

        private void UpdateWire(Transform wire, float length)
        {
            wire.localScale = new Vector3(
                    wireThickness,
                    length + wireThickness,
                    wireThickness
                    );
            Vector3 uniformPos = ToUniSpaceVector(wire.localPosition);
            wire.localPosition = 0.5f * Vector3.Scale(uniformPos, boundingBoxSize);
        }

        private void OnValidate()
        {
            if (CheckSetup())
            {
                UpdateBoundingBox();
            }
        }

        private static Vector3 ToUniSpaceVector(Vector3 vector)
        {
            return new Vector3(
                vector.x == 0 ? 0 : Mathf.Sign(vector.x),
                vector.y == 0 ? 0 : Mathf.Sign(vector.y),
                vector.z == 0 ? 0 : Mathf.Sign(vector.z)
                );
        }

        private bool CheckSetup()
        {
            bool setupCorrect = true;
            if (xAxisWires.Length < 4)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(xAxisWires));
                setupCorrect = false;
            }
            if (yAxisWires.Length < 4)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(yAxisWires));
                setupCorrect = false;
            }
            if (zAxisWires.Length < 4)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(zAxisWires));
                setupCorrect = false;
            }

            return setupCorrect;
        }
    }
}