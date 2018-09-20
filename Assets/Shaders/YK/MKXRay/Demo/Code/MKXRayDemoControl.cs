using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MK.XRay
{
    public class MKXRayDemoControl : MonoBehaviour
    {
        private static bool settingsUsed = false;
        public static bool SettingsUsed
        {
            get { return settingsUsed; }
        }

        private static int currentModel = -1;
        public static int CurrentModel
        {
            get { return currentModel; }
        }

        [SerializeField]
        private List<Material> baseMaterials = new List<Material>();
        private List<Material> currentMaterials = new List<Material>();

        [SerializeField]
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<MeshRenderer> renderers = new List<MeshRenderer>();


        [SerializeField]
        private Slider emissionIntensitySlider;
        private float emissionIntensity;
        public float EmissionIntensity
        {
            get { return emissionIntensity; }
            set
            {
                emissionIntensity = value;
                MKXRayMaterialHelper.SetEmissionColor(currentMaterials[currentModel], Color.Lerp(Color.black, new Color(2, 2, 2, 1), emissionIntensity));
            }
        }

        [SerializeField]
        private Slider xRayInsideSlider;
        private float xRayInside;
        public float XRayInside
        {
            get { return xRayInside; }
            set
            {
                xRayInside = value;
                MKXRayMaterialHelper.SetXRayInside(currentMaterials[currentModel], xRayInside);
            }
        }

        [SerializeField]
        private Slider xRayRimSizeSlider;
        private float xRayRimSize;
        public float XRayRimSize
        {
            get { return xRayRimSize; }
            set
            {
                xRayRimSize = value;
                MKXRayMaterialHelper.SetXRayRimSize(currentMaterials[currentModel], xRayRimSize);
            }
        }

        [SerializeField]
        private Slider noiseSpeedSlider;
        private float noiseSpeed;
        public float NoiseSpeed
        {
            get { return noiseSpeed; }
            set
            {
                noiseSpeed = value;
                MKXRayMaterialHelper.SetNoiseAnimationSpeed(currentMaterials[currentModel], noiseSpeed);
            }
        }

        public bool UseNoise
        {
            set
            {
                if (value)
                {
                    foreach (Material mat in currentMaterials)
                    {
                        mat.EnableKeyword("_MK_NOISE");
                    }
                    noiseSpeedSlider.gameObject.SetActive(true);
                }
                else
                {
                    foreach (Material mat in currentMaterials)
                    {
                        mat.DisableKeyword("_MK_NOISE");
                    }
                    noiseSpeedSlider.gameObject.SetActive(false);
                }
            }
        }

        [SerializeField]
        private Slider dissolveAmountSlider;
        private float dissolveAmount;
        public float DissolveAmount
        {
            get { return dissolveAmount; }
            set
            {
                dissolveAmount = value;
                MKXRayMaterialHelper.SetDissolveAmount(currentMaterials[currentModel], dissolveAmount);
                if (dissolveAmount > 0.0f)
                    dissolveAnimationSpeedSlider.gameObject.SetActive(true);
                else
                    dissolveAnimationSpeedSlider.gameObject.SetActive(false);
            }
        }

        [SerializeField]
        private Slider dissolveAnimationSpeedSlider;
        private float dissolveAnimationSpeed;
        public float DissolveAnimationSpeed
        {
            get { return dissolveAnimationSpeed; }
            set
            {
                dissolveAnimationSpeed = value;
                MKXRayMaterialHelper.SetDissolveAnimationDirection(currentMaterials[currentModel], new Vector4(0, dissolveAnimationSpeed, 0,0));
            }
        }

        private void SetupMaterials()
        {
            currentMaterials.Clear();
            renderers.Clear();
            foreach (GameObject go in gameObjects)
            {
                renderers.Add(go.GetComponent<MeshRenderer>());
            }
            foreach (Material m in baseMaterials)
            {
                currentMaterials.Add(new Material(m));
            }
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].material = currentMaterials[i];
            }
        }

        private void Awake()
        {
            SetupMaterials();
            ChangeModel();
        }

        public void ChangeModel()
        {
            currentModel++;
            if (currentModel > gameObjects.Count - 1)
                currentModel = 0;
            foreach (GameObject go in gameObjects)
                go.SetActive(false);
            gameObjects[currentModel].SetActive(true);
            SetValuesFromMaterial();
            SetMaterialSettingsToSliders();
        }

        private void SetMaterialSettingsToSliders()
        {
            emissionIntensitySlider.value = emissionIntensity;
            xRayInsideSlider.value = xRayInside;
            xRayRimSizeSlider.value = xRayRimSize;
            noiseSpeedSlider.value = noiseSpeed;
            dissolveAmountSlider.value = dissolveAmount;
            dissolveAnimationSpeedSlider.value = dissolveAnimationSpeed;

            if (dissolveAmount > 0.0f)
                dissolveAnimationSpeedSlider.gameObject.SetActive(true);
            else
                dissolveAnimationSpeedSlider.gameObject.SetActive(false);
        }

        private void SetValuesFromMaterial()
        {
            emissionIntensity = MKXRayMaterialHelper.GetEmissionColor(currentMaterials[currentModel]).r / 2.0f;
            xRayInside = MKXRayMaterialHelper.GetXRayInside(currentMaterials[currentModel]);
            xRayRimSize = MKXRayMaterialHelper.GetXRayRimSize(currentMaterials[currentModel]);
            noiseSpeed = MKXRayMaterialHelper.GetNoiseAnimationSpeed(currentMaterials[currentModel]);
            dissolveAmount = MKXRayMaterialHelper.GetDissolveAmount(currentMaterials[currentModel]);
            dissolveAnimationSpeed = MKXRayMaterialHelper.GetDissolveAnimationDirection(currentMaterials[currentModel]).y;
        }

        private void Update()
        {

#if !UNITY_ANDROID || UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                settingsUsed = true;
            }

            if (Input.GetMouseButtonUp(0))
                settingsUsed = false;
#else
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            settingsUsed = true;
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            settingsUsed = false;
        }
#endif
        }
    }
}