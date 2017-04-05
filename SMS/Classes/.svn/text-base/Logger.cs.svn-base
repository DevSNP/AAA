using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AbstractManagementSystem.Classes
{
    public static class TextLog
    {

        private static string GetBasePath()
        {
            String pathString = AppDomain.CurrentDomain.BaseDirectory + "\\Log";
            if (System.IO.Directory.Exists(pathString) == false)
            {
                System.IO.Directory.CreateDirectory(pathString);
            }

            pathString += "\\" + DateTime.Now.Date.ToString("ddMMyyyy");

            if (System.IO.Directory.Exists(pathString) == false)
            {
                System.IO.Directory.CreateDirectory(pathString);
            }
            return pathString;
        }

        public static void Write(Exception ex, String strFName)
        {
            try
            {
                WriteFinal(ex, strFName);
            }
            catch (Exception)
            {
            }
        }

        private static void WriteFinal(Exception ex, String strFName)
        {
            try
            {
                String pathString = GetBasePath();
                String EMessage = ex.Message;
                String ESource = ex.Source;
                String EStackTrace = ex.StackTrace;
                String ETypeName = ex.GetType().FullName;

                pathString += "\\ErrorFile.log";
                if (System.IO.File.Exists(pathString) == false)
                {
                    System.IO.FileStream create = System.IO.File.Create(pathString);
                    create.Close();
                    create.Dispose();
                }

                String strFullMsg = "";
                strFullMsg += "------------------------------------------------------" + Environment.NewLine;
                strFullMsg += DateTime.Now.ToString("HH:mm:ss") + ":" + DateTime.Now.Millisecond.ToString() + "[" + strFName + "]" + Environment.NewLine;
                strFullMsg += "------------------------------------------------------" + Environment.NewLine;
                strFullMsg += "Message : " + EMessage + Environment.NewLine;
                strFullMsg += "Source : " + ESource + Environment.NewLine;
                strFullMsg += "StackTrace : " + EStackTrace + Environment.NewLine;
                strFullMsg += "Type : " + ETypeName + Environment.NewLine;

                System.IO.StreamWriter stream = new System.IO.StreamWriter(new System.IO.FileStream(pathString, System.IO.FileMode.Append, System.IO.FileAccess.Write));
                stream.WriteLine(strFullMsg);
                stream.Close();
                stream.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static void WriteLog(String strMessage)
        {
            try
            {
                String pathString = GetBasePath();
                pathString += "\\logFile.log";
                WriteFinal(strMessage, pathString);
            }
            catch (Exception)
            {
            }
        }

        public static void WriteQuery(String strQueryData)
        {
            try
            {
                String pathString = GetBasePath();
                pathString += "\\QueryFile.log";
                WriteFinal(strQueryData, pathString);
            }
            catch (Exception)
            {
            }
        }

        private static void WriteFinal(string strMessage, string strPath)
        {
            try
            {
                if (System.IO.File.Exists(strPath) == false)
                {
                    System.IO.FileStream create = System.IO.File.Create(strPath);
                    create.Close();
                    create.Dispose();
                }
                System.IO.StreamWriter stream = new System.IO.StreamWriter(new System.IO.FileStream(strPath, System.IO.FileMode.Append, System.IO.FileAccess.Write));
                stream.WriteLine(System.DateTime.Now.ToString() + " : " + strMessage);
                stream.Close();
                stream.Dispose();
            }
            catch (Exception)
            {

            }
        }

    }

    public static class Log
    {
        private static AutoResetEvent objAre = new AutoResetEvent(false);
        private static Thread objMainThread = null;
        private static Queue<LogData> LogDataQueue = null;
        private static Boolean bRun = false;

        private static void Start()
        {
            bRun = true;
            LogDataQueue = new Queue<LogData>();
            objMainThread = new Thread(new ThreadStart(Run));
            objMainThread.Start();
        }

        private static void Stop()
        {
            try
            {
                bRun = false;
                objAre.Set();
                System.Threading.Thread.Sleep(1000);
                objMainThread = null;
                LogDataQueue = null;
            }
            catch (Exception ex)
            {
                TextLog.Write(ex, "Stop");
            }
        }

        private static void Run()
        {
            while (bRun)
            {
                try
                {
                    while (LogDataQueue.Count > 0)
                    {
                        LogData objLogData;
                        lock (LogDataQueue)
                        {
                            objLogData = LogDataQueue.Dequeue();
                        }

                        if (objLogData.Ex != null)
                        {
                            TextLog.Write(objLogData.Ex, "");
                        }
                        else if (objLogData.MessageData != null && objLogData.MessageData.Length > 0)
                        {
                            TextLog.WriteLog(objLogData.MessageData);
                        }
                        else
                        {
                            TextLog.WriteQuery(objLogData.QueryData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    TextLog.Write(ex, "Run");
                }
                objAre.WaitOne(5000, false);
            }
        }

        public static void Write(Exception ex)
        {

            try
            {

                if (LogDataQueue == null) Start();

                LogData objLogData = new LogData();

                objLogData.Ex = ex;
                lock (LogDataQueue)
                {
                    LogDataQueue.Enqueue(objLogData);
                }
                objAre.Set();

            }
            catch (Exception)
            {
                TextLog.Write(ex, "Write(ex, strfname , userid)");
            }

        }

        public static void Write(string strMessage)
        {
            try
            {
                bool isWriteLog = true;
                System.Configuration.Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

                if (myConfiguration.AppSettings.Settings["IsWriteLog"] != null)
                {
                    if (!string.IsNullOrEmpty(myConfiguration.AppSettings.Settings["IsWriteLog"].Value))
                    {
                        isWriteLog = Convert.ToBoolean(myConfiguration.AppSettings.Settings["IsWriteLog"].Value);
                    }
                }


                if (isWriteLog)
                {
                    if (LogDataQueue == null) Start();

                    LogData objLogData = new LogData();
                    objLogData.MessageData = strMessage;
                    lock (LogDataQueue)
                    {
                        LogDataQueue.Enqueue(objLogData);
                    }
                    objAre.Set();
                }
            }
            catch (Exception ex)
            {
                TextLog.Write(ex, "Write(strMessage , userid)");
            }
        }

        public static void WriteQuery(string strQuery)
        {
            try
            {
                bool isWriteLog = true;
                System.Configuration.Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

                if (myConfiguration.AppSettings.Settings["IsWriteLog"] != null)
                {
                    if (!string.IsNullOrEmpty(myConfiguration.AppSettings.Settings["IsWriteLog"].Value))
                    {
                        isWriteLog = Convert.ToBoolean(myConfiguration.AppSettings.Settings["IsWriteLog"].Value);
                    }
                }


                if (isWriteLog)
                {
                    if (LogDataQueue == null) Start();

                    LogData objLogData = new LogData();
                    objLogData.QueryData = strQuery;
                    lock (LogDataQueue)
                    {
                        LogDataQueue.Enqueue(objLogData);
                    }
                    objAre.Set();
                }
            }
            catch (Exception ex)
            {
                TextLog.Write(ex, "WriteQuery(strMessage , userid)");
            }
        }
    }

    public class LogData
    {
        public Exception Ex = null;
        public string MessageData = "";
        public string QueryData = "";
    }
}