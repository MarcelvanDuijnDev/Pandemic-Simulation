using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    public bool Notifications_Active = true;

    [Header("Ref")]
    public TextMeshProUGUI Notification_Title;
    public TextMeshProUGUI Notification_Information;


    [SerializeField] private Transform _Obj;
    [SerializeField] private Transform _TargetPosition;
    private Vector3 _OriginalPos;
    
    [Header("Settings")]
    [SerializeField] private float _Speed;
    [SerializeField] private float _Duration;

    private float _Timer;
    private bool _Active;
    private bool _NewNotification;
    private string _TempTitle;
    private string _TempInfo;
    private int _Priority;

    public static NotificationHandler NOTIF;

    void Start()
    {
        NOTIF = this;

        _OriginalPos = _Obj.position;

        Notification_Title.text = "Notification title test";
        Notification_Information.text = "Notification information test";
    }

    void Update()
    {
        if(_Active && !_NewNotification)
        {
            _Timer += 1 * Time.deltaTime;
            if(_Timer >= _Duration)
            {
                _Active = false;
                _Timer = 0;
            }
            _Obj.transform.position = Vector3.MoveTowards(_Obj.position, _TargetPosition.position, _Speed);
        }
        
        if(_NewNotification)
        {
            _Obj.transform.position = Vector3.MoveTowards(_Obj.position, _OriginalPos, _Speed * 2);
            if(_Obj.transform.position == _OriginalPos)
            {
                Notification_Title.text = _TempTitle;
                Notification_Information.text = _TempInfo;
                _NewNotification = false;
            }
        }
        else
            if(!_Active)
        {
            _Obj.transform.position = Vector3.MoveTowards(_Obj.position, _OriginalPos, _Speed);
        }
    }

    public void SetNotification(string title, string info)
    {
        if (!_Active)
        {
            Notification_Title.text = title;
            Notification_Information.text = info;
            _Active = true;
        }
        else
        {
            _TempTitle = title;
            _TempInfo = info;
            _NewNotification = true;
        }
    }
}
