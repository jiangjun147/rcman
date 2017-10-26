﻿using RemoteConnectionManager.Core;
using System;

namespace RemoteConnectionManager.ExternalProcess
{
    public class PuTTYConnection : HostedProcessConnection
    {
        internal PuTTYConnection(ConnectionSettings connectionSettings)
            : base(connectionSettings)
        {
        }

        protected override string GetFileName()
        {
            return @"dist\putty.exe";
        }

        protected override string GetArguments()
        {
            var command = "-" + Enum.GetName(typeof(Protocol), ConnectionSettings.Protocol).ToLower();

            var credentials = ConnectionSettings.Credentials;
            if (credentials != null)
            {
                command += " -l " + credentials.Username; 
                if (!string.IsNullOrEmpty(credentials.Password))
                {
                    command += " -pw " + credentials.Password;
                }
            }
            if (!string.IsNullOrEmpty(ConnectionSettings.Port))
            {
                command += " -P " + ConnectionSettings.Port;
            }

            command += " " + ConnectionSettings.Server;

            return command;
        }
    }
}