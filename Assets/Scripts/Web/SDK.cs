using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using Agava.VKGames;
using Agava.VKGames.Samples;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SDK : MonoBehaviour
{
    private UnityEvent _onRewardViewed = new UnityEvent();

    public event UnityAction OnRewardViewed
    {
        add => _onRewardViewed.AddListener(value);
        remove => _onRewardViewed.RemoveListener(value);
    }

    private void Awake()
    {
#if YANDEX_GAMES
        YandexGamesSdk.CallbackLogging = true;
# endif
    }

    private IEnumerator Start()
    {
#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize();
        ShowBannerAd();
#endif

#if VK_GAMES
        yield return VKGamesSdk.Initialize();
        ShowBannerAd();
#endif
    }

#if YANDEX_GAMES
    public void ShowRewardAd()
    {
        Agava.YandexGames.VideoAd.Show(onRewardedCallback: _onRewardViewed.Invoke);
    }

    private void ShowBannerAd()
    {
        InterstitialAd.Show();
    }
#endif

#if VK_GAMES
    private void ShowBannerAd()
    {
        Interstitial.Show();
    }

    public void ShowRewardAd()
    {
        Agava.VKGames.VideoAd.Show(onRewardedCallback: _onRewardViewed.Invoke);
    }
#endif

    public void InviteFriendsButton()
    {
#if VK_GAMES
        SocialInteraction.InviteFriends(_onRewardViewed.Invoke);
#endif
    }
}
