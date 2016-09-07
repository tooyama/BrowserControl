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
        [SerializeField]
        private BlockGetter rightHandState;
        [SerializeField]
        private BlockGetter leftHandState;
        //private Quaternion rightRotation;
        //private Quaternion leftRotation;
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

            /*
            rightRotation = rightHand.transform.rotation;

            leftRotation = leftHand.transform.rotation;
            */
        }

        void Update()
        {
            Vector3 rightPos = pinchDetectorRight.transform.position;
            Vector3 leftPos = pinchDetectorLeft.transform.position;

            GameObject leftState = leftHandState.GetBlock();
            GameObject rightState = rightHandState.GetBlock();

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
            else if(rightState == null && leftState == null)
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
            else
            {
                if(pinchDetectorRight.IsPinching)
                {
                    if(rightState != null)
                    {
                        //Quaternion newRightRotation = rightHand.transform.rotation;

                        blockState.gameObject.transform.position = rightHand.transform.position;

                        blockState.gameObject.transform.rotation = rightHand.transform.rotation;
                    }
                }
                else if(pinchDetectorLeft.IsPinching)
                {
                    if (leftState != null)
                    {
                        //Quaternion newLeftRotation = leftHand.transform.rotation;

                        blockState.gameObject.transform.position = leftHand.transform.position;

                        blockState.gameObject.transform.rotation = leftHand.transform.rotation;
                    }
                }
            }

            distance = newDistance;

            /*
            rightRotation = rightHand.transform.rotation;

            leftRotation = leftHand.transform.rotation;
            */
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