using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ArrowTypes
{
    Normal,
    Fire,
    Ice,
    Lightning,
    Water
}

public class AE_AnimatorEvents : MonoBehaviour
{
    [SerializeField] public List<EffectDict> Effect1;
    [SerializeField] public List<EffectDict> Effect2;
    [SerializeField] public List<EffectDict> Effect3;
    [SerializeField] public List<EffectDict> Effect4;
    public GameObject Bow;
    public GameObject Arrow;

    [HideInInspector] public float HUE = -1;
    public ArrowTypes CurrentArrowType = ArrowTypes.Normal;
    [System.Serializable]
    public struct EffectDict
    {
        public ArrowTypes Type;
        public AE_EffectAnimatorProperty Effect;
   
    }
    private void Start()
    {
        CurrentArrowType = ArrowTypes.Normal;
    }
    [System.Serializable]
    public class AE_EffectAnimatorProperty
    {
        [HideInInspector] public RuntimeAnimatorController TargetAnimation;
        public GameObject Prefab;
        public Transform BonePosition;
        public Transform BoneRotation;
        public float DestroyTime = 10;
        [HideInInspector] public GameObject CurrentInstance;
    }

    void InstantiateEffect(AE_EffectAnimatorProperty effect, bool returnIfCreatedInstance = false)
    {
        if (effect.Prefab == null) return;
       // if (returnIfCreatedInstance && effect.CurrentInstance!= null && GameObject.Find(effect.CurrentInstance.name)) return;

        if (effect.BonePosition != null && effect.BoneRotation != null)
            effect.CurrentInstance = Instantiate(effect.Prefab, effect.BonePosition.position, effect.BoneRotation.rotation);
        else effect.CurrentInstance = Instantiate(effect.Prefab);

        if(effect.TargetAnimation != null)
        {
            effect.CurrentInstance.GetComponent<Animator>().runtimeAnimatorController = effect.TargetAnimation;
        }

        if (Bow != null)
        {
            var setMeshToEffect = effect.CurrentInstance.GetComponent<AE_SetMeshToEffect>();
            Debug.Log(effect.CurrentInstance);
            if (setMeshToEffect != null && setMeshToEffect.MeshType == AE_SetMeshToEffect.EffectMeshType.Bow)
            {
                setMeshToEffect.Mesh = Bow;
            }
        }

        if (Arrow != null)
        {
            var setMeshToEffect = effect.CurrentInstance.GetComponent<AE_SetMeshToEffect>();
            if (setMeshToEffect != null && setMeshToEffect.MeshType == AE_SetMeshToEffect.EffectMeshType.Arrow)
            {
                setMeshToEffect.Mesh = Arrow;
            }
        }


        if(HUE > -0.9f)
        {
            UpdateColor(effect);
        }

        if (effect.DestroyTime > 0.001f) Destroy(effect.CurrentInstance, effect.DestroyTime);
    }

    public void ActivateEffect1()
    {
        AE_EffectAnimatorProperty eff = Effect1.Find(e => e.Type == CurrentArrowType).Effect;
        if (eff == null) return;
        InstantiateEffect(eff);
    }

    public void ActivateEffect2()
    {
        AE_EffectAnimatorProperty eff = Effect2.Find(e => e.Type == CurrentArrowType).Effect;
        if (eff == null) return;
        InstantiateEffect(eff);
    }

    public void ActivateEffect3()
    {
        AE_EffectAnimatorProperty eff = Effect3.Find(e => e.Type == CurrentArrowType).Effect;
        if (eff == null) return;
        InstantiateEffect(eff, true);
    }

    public void ActivateEffect4()
    {
        AE_EffectAnimatorProperty eff = Effect4.Find(e => e.Type == CurrentArrowType).Effect;
        if (eff == null) return;
        InstantiateEffect(eff);
    }

    private void UpdateColor(AE_EffectAnimatorProperty effect)
    {
        var settingColor = effect.CurrentInstance.GetComponent<AE_EffectSettingColor>();
        if (settingColor == null) settingColor = effect.CurrentInstance.AddComponent<AE_EffectSettingColor>();
        var hsv = AE_ColorHelper.ColorToHSV(settingColor.Color);
        hsv.H = HUE;
        settingColor.Color = AE_ColorHelper.HSVToColor(hsv);
    }
}
