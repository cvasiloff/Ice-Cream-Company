//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    
    public class ButtonEffect : MonoBehaviour
    {
        public Material baseColor;
        public Material activeColor;
        public void OnButtonDown(Hand fromHand)
        {
            if(activeColor)
                ColorSelf(activeColor.color);
            else
            {
                ColorSelf(Color.white);
            }
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {
            if (baseColor)
                ColorSelf(baseColor.color);
            else
                ColorSelf(Color.cyan);
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}