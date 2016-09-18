using UnityEngine;
using System.Collections;
using Spine.Unity;
using System;
using Spine.Unity.Modules;

public class CharacterAnimController : MonoBehaviour {

    public enum AnimType
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_BOLT,
        DOWN_BOLT,
        LEFT_BOLT,
        RIGHT_BOLT,
        UP_FEAR,
        DOWN_FEAR,
        LEFT_FEAR,
        RIGHT_FEAR
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

    [SpineAnimation]
    public string upBoltAnimationName;

    [SpineAnimation]
    public string downBoltAnimationName;

    [SpineAnimation]
    public string leftBoltAnimationName;

    [SpineAnimation]
    public string rightBoltAnimationName;

    [SpineAnimation]
    public string upFearAnimationName;

    [SpineAnimation]
    public string downFearAnimationName;

    [SpineAnimation]
    public string leftFearAnimationName;

    [SpineAnimation]
    public string rightFearAnimationName;

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
        if (m_eNextAnim != AnimType.NONE)
        {
            if (spineAnimationState.GetCurrent(0).Loop)
            {
                switch (m_eNextAnim)
                {
                    case AnimType.UP:
                        spineAnimationState.SetAnimation(0, upAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                    case AnimType.DOWN:
                        spineAnimationState.SetAnimation(0, downAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                    case AnimType.RIGHT:
                        spineAnimationState.SetAnimation(0, rightAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                    case AnimType.LEFT:
                        spineAnimationState.SetAnimation(0, leftAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                    case AnimType.UP_BOLT:
                        spineAnimationState.SetAnimation(0, upBoltAnimationName, false);
                        break;
                    case AnimType.RIGHT_BOLT:
                        spineAnimationState.SetAnimation(0, rightBoltAnimationName, false);
                        break;
                    case AnimType.LEFT_BOLT:
                        spineAnimationState.SetAnimation(0, leftBoltAnimationName, false);
                        break;
                    case AnimType.DOWN_BOLT:
                        spineAnimationState.SetAnimation(0, downBoltAnimationName, false);
                        break;
                    case AnimType.UP_FEAR:
                        spineAnimationState.SetAnimation(0, upFearAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                    case AnimType.DOWN_FEAR:
                        spineAnimationState.SetAnimation(0, downFearAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                    case AnimType.LEFT_FEAR:
                        spineAnimationState.SetAnimation(0, leftFearAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                    case AnimType.RIGHT_FEAR:
                        spineAnimationState.SetAnimation(0, rightFearAnimationName, false);
                        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0f);
                        break;
                }
            }

            m_eNextAnim = AnimType.NONE;
        }
    }

    void Update()
    {

    }

    public bool IsOnIdle()
    {
        return m_eNextAnim == AnimType.NONE;
    }

    public void StartAnim(AnimType anim)
    {
        m_eNextAnim = anim;
    }
}
