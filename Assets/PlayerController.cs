using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* public class PlayerController : MonoBehaviour
{
    //bool型 = trueもしくはfalseの値を取る変数の型
    protected bool isMovable;
    protected bool isMoving;

    public bool IsMovable
        {
            set
            {
                isMovable = value;
                rigidBodyCache.constraints = value ? RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.FreezeAll;
            }
        }

        protected void Awake()
        {
            rigidBodyCache = GetComponent<Rigidbody2D>();
        }

    void Start()
    {

            var eventTrigger = GetComponent<EventTrigger>();
            var beginDragEntry = new EventTrigger.Entry();
            
            beginDragEntry.eventID = EventTriggerType.PointerDown;
            //ラムダ式？
            beginDragEntry.callback.AddListener(data =>
            {
                OnBeginDrag((PointerEventData)data);
            });
            eventTrigger.triggers.Add(beginDragEntry);

            var dragEntry = new EventTrigger.Entry();
            dragEntry.eventID = EventTriggerType.Drag;
            //ラムダ式？
            dragEntry.callback.AddListener(data =>
            {
                OnDrag((PointerEventData)data);
            });
            eventTrigger.triggers.Add(dragEntry);

            var endDragEntry = new EventTrigger.Entry();
            endDragEntry.eventID = EventTriggerType.EndDrag;
            //ラムダ式？
            endDragEntry.callback.AddListener(data =>
            {
                OnEndDrag((PointerEventData)data);
            });
            eventTrigger.triggers.Add(endDragEntry);
    }

    void Update()
    {
        if (isMoving)
            {
                if (speedKeepCount <= 0)
                {
                    // 摩擦減速処理
                    rigidBodyCache.velocity *= 0.99f - (0.09f * (1f - slipRate));
                }
                if (rigidBodyCache.velocity.magnitude <= 10f)
                {
                    // ある程度遅くなったら動きを止める
                    rigidBodyCache.velocity = Vector2.zero;
                    isMoving = false;
                    if (null != onMoveFinishedAction)
                    {
                        OnMoveFinished();
                        onMoveFinishedAction();
                    }
                }
            }
    }
    //ドラッグした時に呼ばれる
        void OnBeginDrag(PointerEventData data)
        {
            if (!isMovable || isMoving)
            {
                return;
            }

            Debug.Log("begin drag");
            Debug.Log(data.position);
            startPosition = data.position;
        }

        //ドラック中に呼ばれる
        void OnDrag(PointerEventData data)
        {
            if (!isMovable || isMoving)
            {
                return;
            }

            var diff = data.position - startPosition;
            Debug.Log(diff);
        }

        //ドラック後に呼ばれる
        void OnEndDrag(PointerEventData data)
        {
            if (!isMovable || isMoving)
            {
                return;
            }

            var diff = data.position - startPosition;
            Debug.Log("go!!");
            var magnitudeLimit = 50f;
            Debug.Log(diff.magnitude);
            var magnitudeLimitRatio = magnitudeLimit / Mathf.Max(diff.magnitude, magnitudeLimit);
            var speedBonus = speedKeepCount > 0 ? 2f : 1f;
            Move(-diff * magnitudeLimitRatio * speed * speedBonus);
        }

        protected void OnMoveFinished()
        {
            base.OnMoveFinished();
            speedKeepCount = 0;
            isLaserSkillActive = false;
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);

            if (isLaserSkillActive && collision.gameObject.tag == "Enemy")
            {
                FireLaser();
            }
        }
}
*/