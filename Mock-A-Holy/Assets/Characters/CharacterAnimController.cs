using UnityEngine;
using System.Collections;
using Spine.Unity;
using UniRx;
using System;
using Spine.Unity.Modules;

public class CharacterAnimController : MonoBehaviour {

    public enum AnimType
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

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

    public float m_fStartTime = 0f;

    private AnimType m_eNextAnim = AnimType.NONE;

    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    void Start()
    {
        // Make sure you get these AnimationState and Skeleton references in Start or Later. Getting and using them in Awake is not guaranteed by default execution order.
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.state;
        skeleton = skeletonAnimation.skeleton;

        spineAnimationState.Data.DefaultMix = 0.0f;

        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f).Time = m_fStartTime;

        spineAnimationState.Complete += SpineAnimationState_Complete;

        //var clickStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0));

        //clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
        //    .Where(xs => xs.Count >= 2)
        //    .Subscribe(xs =>
        //    {
        //        if (spineAnimationState.GetCurrent(0).Loop) // || spineAnimationState.GetCurrent(0).Time > 0.5f)
        //            spineAnimationState.Complete += SpineAnimationState_Complete;
        //    });

        //var skeletonRenderer = GetComponent<SkeletonRenderer>();
        //skeletonRenderer.skeleton.AttachUnitySprite("gamba des", mySprite);
    }

    private void SpineAnimationState_Complete(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        if(m_eNextAnim != AnimType.NONE)
        {
            if (spineAnimationState.GetCurrent(0).Loop)
            {
                switch (m_eNextAnim)
                {
                    case AnimType.UP:
                        spineAnimationState.SetAnimation(0, upAnimationName, false);
                        break;
                    case AnimType.DOWN:
                        spineAnimationState.SetAnimation(0, downAnimationName, false);
                        break;
                    case AnimType.RIGHT:
                        spineAnimationState.SetAnimation(0, rightAnimationName, false);
                        break;
                    case AnimType.LEFT:
                        spineAnimationState.SetAnimation(0, leftAnimationName, false);
                        break;
                }
                spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
            }

            m_eNextAnim = AnimType.NONE;
        }
    }

    void Update()
    {

    }

    public void StartAnim(AnimType anim)
    {
        m_eNextAnim = anim;
    }
}
