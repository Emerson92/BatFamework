﻿using System;
using System.Collections;
using System.Collections.Generic;
using THEDARKKNIGHT.ConstDefine;
using THEDARKKNIGHT.InputOperate;
using THEDARKKNIGHT.ProcessCore;
using THEDARKKNIGHT.ProcessCore.Lua;
using UnityEngine;
namespace THEDARKKNIGHT.Example {
    public class LuaProcessDemo : MonoBehaviour
    {

        ProcessDemo ProcessControl;

#if UNITY_EDITOR
        public TextAsset text;
#else
        private TextAsset text;
#endif
        GameObject PlayerAreaTwo { get; set; }
        GameObject PlayerAreaOne { get; set; }
        GameObject PlayerAreaThree { get; set; }
        GameObject PlayAreaFour { get; set; }
        GameObject PlayAreaFive { get; set; }
        GameObject PlayAreaSix { get; set; }

        // Use this for initialization
        void Start()
        {
#if !UNITY_EDITOR
            text = Resources.Load<TextAsset>("Test\\ProcessDemo\\ProcessConfig\\LuaProcess\\ProcessConfig");
#endif
            BInputOperateEngine.Instance();
            ProcessControl = new ProcessDemo();
            ProcessControl.AddProcessUnitByJson(text.text);
            ProcessControl.SetProcessUnitStartCallback(ProcessUnitStart);
            ProcessControl.SetProcessUnitFinishCallback(ProcessUnitFinish);
            ProcessControl.StartProcess();

        }

        private bool ProcessUnitFinish(string name, object data)
        {
            switch (name)
            {
                case "PlayerOneUnit":
                case "PlayerTwoUnit":
                case "PlayerThreeUnit":
                case "PlayerFourUnit":
                      break;
            }
            return true;
        }

        private void ProcessUnitStart(string name, object data)
        {
            switch (name)
            {
                case "PlayerOneUnit":
                    if (!PlayerAreaOne)
                    {
                        GameObject PlayerArea1 = Resources.Load(BFameWorkPathDefine.BFameResourceTestProcessPath + "/PlayAreaOne") as GameObject;
                        PlayerAreaOne = GameObject.Instantiate(PlayerArea1);
                    }
                    CameraCtrl.Instance().LerpFocusCenter(PlayerAreaOne.transform.position);
                    break;
                case "PlayerTwoUnit":
                    if (!PlayerAreaTwo)
                    {
                        GameObject PlayerArea2 = Resources.Load(BFameWorkPathDefine.BFameResourceTestProcessPath + "/PlayAreaTwo") as GameObject;
                        PlayerAreaTwo = GameObject.Instantiate(PlayerArea2);
                    }
                    CameraCtrl.Instance().SetObserverRadius(8f);
                    CameraCtrl.Instance().LerpFocusCenter(PlayerAreaTwo.transform.position);
                    break;
                case "PlayerThreeUnit":
                    if (!PlayerAreaThree)
                    {
                        GameObject PlayerArea3 = Resources.Load(BFameWorkPathDefine.BFameResourceTestProcessPath + "/PlayAreaThree") as GameObject;
                        PlayerAreaThree = GameObject.Instantiate(PlayerArea3);
                    }
                    CameraCtrl.Instance().LerpFocusCenter(PlayerAreaThree.transform.position);
                    break;
                case "PlayerFourUnit":
                    if (!PlayAreaFour)
                    {
                        GameObject PlayerArea4 = Resources.Load(BFameWorkPathDefine.BFameResourceTestProcessPath + "/PlayAreaFour") as GameObject;
                        PlayAreaFour = GameObject.Instantiate(PlayerArea4);
                    }
                    CameraCtrl.Instance().LerpFocusCenter(PlayAreaFour.transform.position);
                    break;
                case "PlayerFiveUnit":
                    if (!PlayAreaFive)
                    {
                        GameObject PlayerArea5 = Resources.Load(BFameWorkPathDefine.BFameResourceTestProcessPath + "/PlayAreaFive") as GameObject;
                        PlayAreaFive = GameObject.Instantiate(PlayerArea5);
                    }
                    CameraCtrl.Instance().LerpFocusCenter(PlayAreaFive.transform.position);
                    break;
                case "PlayerSixUnit":
                    if (!PlayAreaSix)
                    {
                        GameObject PlayerArea6 = Resources.Load(BFameWorkPathDefine.BFameResourceTestProcessPath + "/PlayAreaSix") as GameObject;
                        PlayAreaSix = GameObject.Instantiate(PlayerArea6);
                    }
                    CameraCtrl.Instance().LerpFocusCenter(PlayAreaSix.transform.position);
                    break;

            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
