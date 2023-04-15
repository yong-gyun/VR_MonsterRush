using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : UI_WorldSpace
{
    enum Buttons
    {
        BuyDamageBoomButton,
        BuySlowBoomButton,
        BuyStunBoomButton,
        UpgradeFireSpeedButton,
        UpgradeDamageButton
    }

    enum Texts
    {
        DamageStatText,
        FireSpeedStatText,
        DamagePrice,
        SlowPrice,
        StunPrice,
        AttackPrice,
        FireSpeedPrice,
    }

    int _damageBoomPrice = 30;
    int _slowBoomPrice = 20;
    int _stunBoomPrice = 25;
    int _upgradeDamagePrice = 6;
    int _upgradeFireSpeedPrice = 4;

    float maxFireSpeed = 0.1f;
    float maxDamage = 35;

    float _upgradeDamage = 2f;
    float _upgradeFireSpeed = 0.025f;
    float _coolTime = 5f;

    Bomb bomb = null;

    public override void Init()
    {
        base.Init();
        Managers.UI.worldSpaces.Add(this);
        bomb = FindObjectOfType<Bomb>();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetButton((int)Buttons.BuyDamageBoomButton).onClick.AddListener(OnBuyDamageBoom);
        GetButton((int)Buttons.BuySlowBoomButton).onClick.AddListener(OnBuySlowBoom);
        GetButton((int)Buttons.BuyStunBoomButton).onClick.AddListener(OnBuyStunBoom);
        GetButton((int)Buttons.UpgradeDamageButton).onClick.AddListener(UpgradeDamage);
        GetButton((int)Buttons.UpgradeFireSpeedButton).onClick.AddListener(UpgradeFireSpeed);

        GetText((int)Texts.DamageStatText).text = $"{Managers.Game.Player.damage}";
        GetText((int)Texts.FireSpeedStatText).text = $"{Managers.Game.Player.fireSpeed}";
        GetText((int)Texts.DamagePrice).text = $"{_damageBoomPrice}G";
        GetText((int)Texts.SlowPrice).text = $"{_slowBoomPrice}G";
        GetText((int)Texts.StunPrice).text = $"{_stunBoomPrice}G";
        GetText((int)Texts.AttackPrice).text = $"{_upgradeDamagePrice}G";
        GetText((int)Texts.FireSpeedPrice).text = $"{_upgradeFireSpeedPrice}G";
    }

    protected override void Start()
    {
        base.Start();
        GetText((int)Texts.DamageStatText).text = $"{Managers.Game.Player.damage}";
        GetText((int)Texts.FireSpeedStatText).text = $"{Managers.Game.Player.fireSpeed}";
    }

    void OnBuyDamageBoom()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);

        if (Managers.Game.CurrentGold < _damageBoomPrice)
            return;

        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Buy);
        Managers.Game.CurrentGold -= _damageBoomPrice; 
        bomb.OnExplosion(Define.BombType.Damage);
        StartCoroutine(Cooltime(Define.BombType.Damage));
    }

    void OnBuySlowBoom()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);

        if (Managers.Game.CurrentGold < _slowBoomPrice)
            return;

        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Buy);
        Managers.Game.CurrentGold -= _slowBoomPrice;
        bomb.OnExplosion(Define.BombType.Slow);
        StartCoroutine(Cooltime(Define.BombType.Slow));
    }

    void OnBuyStunBoom()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);

        if (Managers.Game.CurrentGold < _stunBoomPrice)
            return;

        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Buy);
        Managers.Game.CurrentGold -= _stunBoomPrice;
        bomb.OnExplosion(Define.BombType.Stun);
        StartCoroutine(Cooltime(Define.BombType.Stun));
    }

    void UpgradeDamage()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);

        if (Managers.Game.Player.damage > maxDamage)
        {
            GetText((int)Texts.FireSpeedPrice).text = $"Max";
            Managers.Game.Player.damage = maxDamage;
            return;
        }

        if (Managers.Game.CurrentGold < _upgradeDamagePrice)
            return;

        Managers.Game.CurrentGold -= _upgradeDamagePrice;
        Managers.Game.Player.damage += _upgradeDamage;
        _upgradeDamagePrice += 2;
        GetText((int)Texts.DamageStatText).text = $"{Managers.Game.Player.damage}";
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Upgrade);
        GetText((int)Texts.AttackPrice).text = $"{_upgradeDamagePrice}G";
    }

    void UpgradeFireSpeed()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);

        if(Managers.Game.Player.fireSpeed <= maxFireSpeed)
        {
            Managers.Game.Player.fireSpeed = maxFireSpeed;
            GetText((int)Texts.FireSpeedPrice).text = $"Max";
            return;
        }

        if (Managers.Game.CurrentGold < _upgradeFireSpeedPrice)
            return;

        Managers.Game.CurrentGold -= _upgradeFireSpeedPrice;
        Managers.Game.Player.fireSpeed -= _upgradeFireSpeed;
        float fireSpeed = Managers.Game.Player.fireSpeed;
        _upgradeFireSpeedPrice += 1;
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Upgrade);
        GetText((int)Texts.FireSpeedStatText).text = string.Format("{0:0.##}", fireSpeed);
        GetText((int)Texts.FireSpeedPrice).text = $"{_upgradeFireSpeedPrice}G";
    }

    IEnumerator Cooltime(Define.BombType type)
    {
        Button btn = null;

        switch (type)
        {
            case Define.BombType.Damage:
                btn = GetButton((int)Buttons.BuyDamageBoomButton);
                break;

            case Define.BombType.Stun:
                btn = GetButton((int)Buttons.BuyStunBoomButton);
                break;

            case Define.BombType.Slow:
                btn = GetButton((int)Buttons.BuySlowBoomButton);
                break;
        }

        Color color = btn.image.color;
        color.a = 0.5f;
        btn.image.color = color;
        btn.enabled = false;

        yield return new WaitForSeconds(_coolTime);
        
        btn.enabled = true;
        color.a = 1f;
        btn.image.color = color;
    }
}
