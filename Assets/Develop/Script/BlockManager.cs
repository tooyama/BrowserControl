using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Leap.Unity
{
    public class BlockManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject rightHand;
        [SerializeField]
        private GameObject leftHand;
        private PinchDetector pinchDetectorRight;
        private PinchDetector pinchDetectorLeft;
        private BlockState blockState;
        private float distance = 0;

        void Awake()
        {
            blockState = GetComponentInChildren<BlockState>();
        }

        void Start()
        {
            pinchDetectorRight = rightHand.GetComponent<PinchDetector>();

            pinchDetectorLeft = leftHand.GetComponent<PinchDetector>();
        }

        void Update()
        {
            Vector3 rightPos = pinchDetectorRight.transform.position;
            Vector3 leftPos = pinchDetectorLeft.transform.position;

            float newDistance = Vector3.Distance(rightPos, leftPos);

            if (pinchDetectorRight.IsPinching && pinchDetectorLeft.IsPinching)
            {
                float dis = newDistance - distance;

                if(0.01f < dis)
                {
                    blockState.changeScale(0.05f);
                }
                else if (dis < -0.01f)
                {
                    blockState.changeScale(-0.05f);
                }
            }
            else
            {
                if(pinchDetectorRight.DidStartPinch)
                {
                    StartCoroutine(WaitForRightHand());
                }
                else if(pinchDetectorLeft.DidStartPinch)
                {
                    StartCoroutine(WaitForLeftHand());
                }
            }

            distance = newDistance;
        }

        IEnumerator WaitForRightHand()
        {
            yield return new WaitForSeconds(0.5f);

            if (pinchDetectorRight.IsPinching && !pinchDetectorLeft.IsPinching)
            {
                iTween.MoveTo(blockState.gameObject, rightHand.transform.position, 0.5f);
            }
        }

        IEnumerator WaitForLeftHand()
        {
            yield return new WaitForSeconds(0.5f);

            if(!pinchDetectorRight.IsPinching && pinchDetectorLeft.IsPinching)
            {
                iTween.MoveTo(blockState.gameObject, leftHand.transform.position, 0.5f);
            }
        }
    }
}