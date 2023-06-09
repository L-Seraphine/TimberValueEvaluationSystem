﻿using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimberValueEvaluationSystem.Services
{
    public static class MessageHelper
    {
        //消息自动关闭通知时间
        public static int waitTime = 5;

        //普通消息通知
        public static void Info(string message)
        {
            GrowlInfo growlInfo = new GrowlInfo();
            growlInfo.WaitTime = waitTime;
            growlInfo.Message = message;
            Growl.Info(growlInfo);
        }

        //成功消息通知
        public static void Success(string message)
        {
            GrowlInfo growlInfo = new GrowlInfo();
            growlInfo.WaitTime = waitTime;
            growlInfo.Message = message;
            Growl.Success(growlInfo);
        }

        //警告消息通知
        public static void Warning(string message)
        {
            GrowlInfo growlInfo = new GrowlInfo();
            growlInfo.WaitTime = waitTime;
            growlInfo.Message = message;
            Growl.Warning(growlInfo);
        }

        //错误消息通知
        public static void Error(string message)
        {
            GrowlInfo growlInfo = new GrowlInfo();
            growlInfo.WaitTime = waitTime;
            growlInfo.Message = message;
            Growl.Error(growlInfo);
        }

        //读取资源字典
        public static string GetString(string key)
        {
            return (string)Application.Current.Resources[key];
        }

    }
}
