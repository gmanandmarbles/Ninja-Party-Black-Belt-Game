using ReadyPlayerMe.Core;
using UnityEngine;
using LootLocker.Requests;

namespace ReadyPlayerMe.Samples
{
    public class ThirdPersonLoader : MonoBehaviour
    {
        private readonly Vector3 avatarPositionOffset = new Vector3(0, -0.08f, 0);

        [SerializeField]
        [Tooltip("RPM avatar URL or shortcode to load")]
        private string avatarUrl;
        private GameObject avatar;
        private AvatarObjectLoader avatarObjectLoader; // Add this line to declare the AvatarObjectLoader
        [SerializeField]
        [Tooltip("Animator to use on loaded avatar")]
        private RuntimeAnimatorController animatorController;
        [SerializeField]
        [Tooltip("If true it will try to load avatar from avatarUrl on start")]
        private bool loadOnStart = true;
        [SerializeField]
        [Tooltip("Preview avatar to display until avatar loads. Will be destroyed after the new avatar is loaded")]
        private GameObject previewAvatar;

        private void Start()
        {
            avatarObjectLoader = new AvatarObjectLoader(); // Initialize the AvatarObjectLoader
            avatarUrl = PlayerPrefs.GetString("AvatarKey");
            print("AvatarKey");
            print(avatarUrl);
            if (loadOnStart)
            {
                LoadAvatar(avatarUrl);
            }
        }

        private void OnLoadFailed(object sender, FailureEventArgs args)
        {
            // Handle avatar loading failure if needed.
        }

        private void OnLoadCompleted(object sender, CompletionEventArgs args)
        {
            if (previewAvatar != null)
            {
                Destroy(previewAvatar);
                previewAvatar = null;
            }
            SetupAvatar(args.Avatar);
        }

        private void SetupAvatar(GameObject targetAvatar)
        {
            if (avatar != null)
            {
                Destroy(avatar);
            }

            avatar = targetAvatar;
            // Re-parent and reset transforms
            avatar.transform.parent = transform;
            avatar.transform.localPosition = avatarPositionOffset;
            avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);

            var controller = GetComponent<ThirdPersonController>();
            if (controller != null)
            {
                controller.Setup(avatar, animatorController);
            }
        }

        public void LoadAvatar(string url)
        {
            // Remove any leading or trailing spaces
            avatarUrl = url.Trim();
            avatarObjectLoader.LoadAvatar(avatarUrl);
        }
    }
}
