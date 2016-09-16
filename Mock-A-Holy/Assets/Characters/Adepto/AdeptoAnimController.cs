using UnityEngine;
using System.Collections;
using Spine.Unity;
using UniRx;
using System;
using Spine.Unity.Modules;

public class AdeptoAnimController : MonoBehaviour {

    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string idleAnimationName;

    [SpineAnimation]
    public string upAnimationName;

    [SpineAnimation]
    public string downAnimationName;

    [SpineAnimation]
    public string rightAnimationName;
    [SpineAnimation]
    public string leftAnimationName;
    #endregion

    SkeletonAnimation skeletonAnimation;

    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    public Sprite mySprite;

    void Start()
    {
        // Make sure you get these AnimationState and Skeleton references in Start or Later. Getting and using them in Awake is not guaranteed by default execution order.
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.state;
        skeleton = skeletonAnimation.skeleton;

        spineAnimationState.Data.DefaultMix = 0.0f;

        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f).Time = 1.0f;

        var clickStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0));

        clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
            .Where(xs => xs.Count >= 2)
            .Subscribe(xs =>
            {
                if (spineAnimationState.GetCurrent(0).Loop) // || spineAnimationState.GetCurrent(0).Time > 0.5f)
                    spineAnimationState.Complete += SpineAnimationState_Complete;
            });

        var skeletonRenderer = GetComponent<SkeletonRenderer>();
        skeletonRenderer.skeleton.AttachUnitySprite("gamba des", mySprite);
    }

    bool firstTime = false;
    void Update()
    {
        if (firstTime)
        {
            Spine.Animation walkAnimation = skeleton.Data.FindAnimation(idleAnimationName);
            walkAnimation.Apply(skeleton, 0f, 1.0f, true, null);
            skeleton.UpdateWorldTransform();
            firstTime = false;
        }
    }

    int prog = 0;
    private void SpineAnimationState_Complete(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        switch (prog)
        {
            case 0:
        spineAnimationState.SetAnimation(0, upAnimationName, false);
                break;
            case 1:
        spineAnimationState.SetAnimation(0, downAnimationName, false);
                break;
            case 2:
                spineAnimationState.SetAnimation(0, rightAnimationName, false);
                break;
            case 3:
                spineAnimationState.SetAnimation(0, leftAnimationName, false);
                break;
        }
        if (++prog > 3) prog = 0;


        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
        spineAnimationState.Complete -= SpineAnimationState_Complete;
    }
}
