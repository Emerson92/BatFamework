﻿using System;
using System.Collections;
using System.Collections.Generic;
using THEDARKKNIGHT.ConstDefine;
using THEDARKKNIGHT.EventDefine;
using THEDARKKNIGHT.EventSystem;
using THEDARKKNIGHT.InputOperate;
using THEDARKKNIGHT.InputOperate.DataStruct;
using THEDARKKNIGHT.ProcessCore;
using UnityEngine;
namespace THEDARKKNIGHT.Example{

    public class ProcessTreePlayerOneDemo : BProcessItem
    {
        GameObject PlayerOne;

        public bool IsNeedToMove = false;

        private Vector3 MovePosition;

        private float time = 0;

        public ProcessTreePlayerOneDemo(string name) {
            this.TaskName = name;
        }

        public override void AssetInit(object data)
        {
            GameObject playerOne = Resources.Load(BFameWorkPathDefine.BFameResourceTestProcessPath + "/PlayerOne") as GameObject;
            PlayerOne  = GameObject.Instantiate(playerOne);
            ReadyToExcute();
        }

        public override void DataInit(object data)
        {
            AddListerner();
            MovePosition = PlayerOne.transform.position + PlayerOne.transform.forward * 3;
        }

        private void AddListerner()
        {
            BEventManager.Instance().AddListener(BatEventDefine.LEFTPRESSEVENT, LeftPressCallback);
        }

        private object LeftPressCallback(object data)
        {
            InputDataPacket<Vector3> packet = (InputDataPacket<Vector3>)data;
            if (packet.Info.CastGameObject == PlayerOne) {
                IsNeedToMove = true;
            }
            return null;
        }

        public override void FixedUpdate()
        {

        }

        public override void ProcessExcute()
        {

        }

        public override void StopExcute()
        {

        }

        public override void Update()
        {
            if (IsNeedToMove)
            {
                PlayerOne.transform.position = Vector3.Lerp(PlayerOne.transform.position, MovePosition, time / 10);
                time += Time.deltaTime;
                if (time > 3)
                {
                    time = 0;
                    ProcessFinish("PlayerTwoUnit");
                    IsNeedToMove = false;
                }
            }
        }

        public override void Destory()
        {
            BEventManager.Instance().RemoveListener(BatEventDefine.LEFTPRESSEVENT, LeftPressCallback);
            GameObject.Destroy(PlayerOne);
        }
    }
}

