using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform r_firePos;
    private Transform l_firePos;
    public float damage { get; set; } = 7;
    public float fireSpeed { get; set; } = 0.5f;
    public GameObject rightGun { get; set; }
    public GameObject leftGun { get; set; }
    private bool isRightFire = false;
    //private bool isLeftFire = false;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RHand))
            StartCoroutine(RightFire());
        //else if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LHand))
        //    StartCoroutine(LeftFire());
    }
    
    public void Init()
    {
        rightGun = Managers.Resource.Instantiate("Item/Gun", ARAVRInput.RHandPosition, Quaternion.identity, ARAVRInput.RHand);
        //leftGun = Managers.Resource.Instantiate("Item/Gun", ARAVRInput.RHandPosition, Quaternion.identity, ARAVRInput.LHand);
        rightGun.transform.localPosition = new Vector3(0.3f, -0.5f, 0.5f);
        //leftGun.transform.localPosition = new Vector3(-0.3f, -0.5f, 0.5f);

        UI_Crosshair r_Crosshair = Managers.UI.MakeWorldSpaceUI<UI_Crosshair>(Managers.UI.Root.transform);
        //UI_Crosshair l_Crosshair = Managers.UI.MakeWorldSpaceUI<UI_Crosshair>(Managers.UI.Root.transform);
        r_Crosshair.SetDirection(ARAVRInput.Controller.RTouch);
        //r_Crosshair.SetDirection(ARAVRInput.Controller.LTouch);
        r_firePos = Util.FindChild(rightGun, "FirePos", true).transform;
        //l_firePos = Util.FindChild(leftGun, "FirePos", true).transform;
    }


    IEnumerator RightFire()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() || isRightFire == true)
            yield break;

        Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        RaycastHit hit;

        ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch);
        GameObject shotParticle = Managers.Resource.Instantiate($"Effect/ShotParticle", r_firePos.position, Quaternion.Euler(-90, 0, 0));
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Fire);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Mob"))
            {
                hit.transform.GetComponent<MobBase>().OnDamaged(damage, Define.Hit.Bullet);
            }

            GameObject hitParticle = Managers.Resource.Instantiate("Effect/HitParticle", hit.point, Quaternion.identity);
        }

        isRightFire = true;
        yield return new WaitForSeconds(fireSpeed);
        isRightFire = false;
    }

    //IEnumerator LeftFire()
    //{
    //    if (isLeftFire == true)
    //        yield break;

    //    Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
    //    RaycastHit hit;

    //    GameObject shotParticle = Managers.Resource.Instantiate($"Effect/ShotParticle", l_firePos.position, Quaternion.Euler(-90, 0, 0));
    //    Managers.Sound.PlaySoundEffect(Define.SoundEffect.Fire);

    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //    {
    //        if (hit.transform.gameObject.CompareTag("Mob"))
    //        {
    //            hit.transform.GetComponent<MobBase>().OnDamaged(damage);
    //        }

    //        GameObject hitParticle = Managers.Resource.Instantiate("Effect/HitParticle", hit.point, Quaternion.identity);
    //    }

    //    isLeftFire = true;
    //    yield return new WaitForSeconds(fireSpeed);
    //    isLeftFire = false;
    //}
}