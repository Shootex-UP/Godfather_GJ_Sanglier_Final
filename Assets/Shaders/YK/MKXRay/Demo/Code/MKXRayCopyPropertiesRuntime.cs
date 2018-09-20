using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK.XRay
{
    public class MKXRayCopyPropertiesRuntime : MonoBehaviour
    {

        private Material src;
        private Material[] dst;

        public void Start()
        {
            src = transform.root.GetComponent<MeshRenderer>().sharedMaterial;

            Material[] tmp = GetComponent<MeshRenderer>().sharedMaterials;
            dst = new Material[tmp.Length];

            for(int i = 0; i < dst.Length; i++)
            {
                dst[i] = new Material(tmp[i]);
            }

            GetComponent<MeshRenderer>().sharedMaterials = dst;
        }

        void Update()
        {
            for (int i = 0; i < dst.Length; i++)
            {
                MKXRayMaterialHelper.SetEmissionColor(dst[i], MKXRayMaterialHelper.GetEmissionColor(src));
                MKXRayMaterialHelper.SetXRayInside(dst[i], MKXRayMaterialHelper.GetXRayInside(src));
                MKXRayMaterialHelper.SetXRayRimSize(dst[i], MKXRayMaterialHelper.GetXRayRimSize(src));
                MKXRayMaterialHelper.SetDissolveAmount(dst[i], MKXRayMaterialHelper.GetDissolveAmount(src));
                MKXRayMaterialHelper.SetDissolveAnimationDirection(dst[i], MKXRayMaterialHelper.GetDissolveAnimationDirection(src));

                if (src.IsKeywordEnabled("_MK_NOISE"))
                {
                    if (!dst[i].IsKeywordEnabled("_MK_NOISE"))
                        dst[i].EnableKeyword("_MK_NOISE");
                    MKXRayMaterialHelper.SetNoiseAnimationSpeed(dst[i], MKXRayMaterialHelper.GetNoiseAnimationSpeed(src));
                }
                else
                {
                    if (dst[i].IsKeywordEnabled("_MK_NOISE"))
                        dst[i].DisableKeyword("_MK_NOISE");
                }
            }
        }
    }
}
