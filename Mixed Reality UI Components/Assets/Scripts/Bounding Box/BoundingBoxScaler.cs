using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using i5.Utilities.Debugging;

namespace i5.MixedRealityUIComponents.BoundingBox
{

    [RequireComponent(typeof(BoxCollider))]
    public class BoundingBoxScaler : MonoBehaviour
    {
        [SerializeField] private Transform[] xAxisWires;
        [SerializeField] private Transform[] yAxisWires;
        [SerializeField] private Transform[] zAxisWires;
        [SerializeField] private Transform[] moveWidgets;
        [SerializeField] private Transform[] scaleWidgets;
        [SerializeField] private Transform content;


        [SerializeField] private float wireThickness = 0.01f;
        [SerializeField] private float moveWidgetTargetSize = 0.3f;
        [SerializeField] private float scaleWidgetTargetSize = 0.1f;
        [SerializeField] private Vector3 boundingBoxSize = Vector3.one;

        private BoxCollider boxCollider;

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
            boxCollider = GetComponent<BoxCollider>();
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
            if (boundingBoxSize.z == 0)
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

            foreach (Transform moveWidget in moveWidgets)
            {
                UpdateWidget(moveWidget, moveWidgetTargetSize);
            }
            foreach(Transform scaleWidget in scaleWidgets)
            {
                UpdateWidget(scaleWidget, scaleWidgetTargetSize);
            }

            boxCollider.size = boundingBoxSize;
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

        private void UpdateWidget(Transform widget, float targetSize)
        {
            Vector3 uniformPos = ToUniSpaceVector(widget.localPosition);
            widget.localPosition = 0.5f * Vector3.Scale(uniformPos, boundingBoxSize);

            // in the uniformPos, the position with 1 or -1 is the only irrelevant size
            float minValue = targetSize;

            for (int i=0;i<3;i++)
            {
                if (uniformPos[i] == 0 && boundingBoxSize[i] < minValue)
                {
                    minValue = boundingBoxSize[i];
                }
            }
            widget.localScale = new Vector3(minValue, minValue, minValue);
        }

        private void OnValidate()
        {
            if (CheckSetup())
            {
                if (boxCollider == null)
                {
                    boxCollider = GetComponent<BoxCollider>();
                }
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

        public void EncapsulateContent()
        {
            content.localPosition = Vector3.zero;
            MeshRenderer[] rends = content.GetComponentsInChildren<MeshRenderer>();
            Bounds overallBounds = new Bounds();
            bool boundsUninitialized = true;
            foreach (MeshRenderer rend in rends)
            {
                if (boundsUninitialized)
                {
                    overallBounds = rend.bounds;
                    boundsUninitialized = false;
                }
                else
                {
                    overallBounds.Encapsulate(rend.bounds);
                }
            }
            BoundingBoxSize = overallBounds.size;
            content.localPosition = -overallBounds.center;
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
            if (moveWidgets.Length < 6)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(moveWidgets));
                setupCorrect = false;
            }
            if (scaleWidgets.Length < 8)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(scaleWidgets));
                setupCorrect = false;
            }

            if (content == null)
            {
                DebugMessages.LogMissingReferenceError(this, nameof(content));
                setupCorrect = false;
            }

            return setupCorrect;
        }
    }
}