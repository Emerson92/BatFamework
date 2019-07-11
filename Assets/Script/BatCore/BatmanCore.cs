﻿using THEDARKKNIGHT.BatCore;
using THEDARKKNIGHT.Lua;

namespace THEDARKKNIGHT
{

    /// <summary>
    /// Program mainly enter root
    /// </summary>
    public class BatmanCore : BatMonoSingletion<BatmanCore>
    {
        

        public void Awake()
        {
            CodeWatcher.Instance().Init();
            BLuaControl.Instance().Init();
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void FixedUpdate()
        {
            LifeCycleControl.Instance().FixedUpdate(this);
        }

        public void LateUpdate()
        {
            LifeCycleControl.Instance().LateUpdate(this);
            LifeCycleControl.Instance().RecycleGarbageTool();
        }

        public void OnApplicationFocus(bool focus)
        {
            LifeCycleControl.Instance().OnApplicationFocus(this);
        }

        public void OnApplicationPause(bool pause)
        {
            LifeCycleControl.Instance().BOnApplicationPause(this);
        }

        public void OnApplicationQuit()
        {
            LifeCycleControl.Instance().OnApplicationQuit(this);
        }

        public override void OnDestroy()
        {
            LifeCycleControl.Instance().OnDestory(this);
        }

        public void OnDisable()
        {
            LifeCycleControl.Instance().OnDisable(this);
        }

        public void OnEnable()
        {
            LifeCycleControl.Instance().OnEnable(this);
        }

        void Update()
        {
            LifeCycleControl.Instance().Update(this);
        }
    }
}
