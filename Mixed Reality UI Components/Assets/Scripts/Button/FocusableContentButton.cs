using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace i5.MixedRealityUIComponents.Button
{

    public class FocusableContentButton : FocusableButton
    {

        private string content;
        private TextMesh contentTextMesh;

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                if (contentTextMesh == null)
                {
                    contentTextMesh = transform.Find("Content").GetComponent<TextMesh>();
                }


                if (contentTextMesh != null)
                {
                    contentTextMesh.text = content;
                }
            }
        }
    }
}
