using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OculusSampleFramework
{
    public class MainMenuUiInit : MonoBehaviour
    {
        [SerializeField]
        DistanceGrabber[] m_grabbers = null;

        // Use this for initialization
        void Start()
        {
            DebugUIBuilder.instance.AddLabel("Press 'A' to open settings (lover button of right controller)");
            DebugUIBuilder.instance.Show();

            // Forcing physics tick rate to match game frame rate, for improved physics in this sample.
            // See comment in OVRGrabber.Update for more information.
            float freq = OVRManager.display.displayFrequency;
            if (freq > 0.1f)
            {
                Debug.Log("Setting Time.fixedDeltaTime to: " + (1.0f / freq));
                Time.fixedDeltaTime = 1.0f / freq;
            }
        }
    }
}
