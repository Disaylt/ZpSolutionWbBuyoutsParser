using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using ZennoLab.CommandCenter;
using ZennoLab.Emulation;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZpSolutionWbBuyoutsParser.Models.Json;
using ZpSolutionWbBuyoutsParser.Mongo.Tests;
using ZpSolutionWbBuyoutsParser.Parser;
using ZpSolutionWbBuyoutsParser.Proxy;

namespace ZpSolutionWbBuyoutsParser
{
    /// <summary>
    /// Класс для запуска выполнения скрипта
    /// </summary>
    public class Program : IZennoExternalCode
    {
        /// <summary>
        /// Метод для запуска выполнения скрипта
        /// </summary>
        /// <param name="instance">Объект инстанса выделеный для данного скрипта</param>
        /// <param name="project">Объект проекта выделеный для данного скрипта</param>
        /// <returns>Код выполнения скрипта</returns>
        public int Execute(Instance instance, IZennoPosterProjectModel project)
        {
            ProjectConfig.Initialize(project);
            AccountsWorkQueue accountsWorkQueue = AccountsWorkQueue.Instance;
            accountsWorkQueue.SkipOrCreateQueue();
            string sessionName = accountsWorkQueue.TakeSession();
            LoadProfile(project.Profile, sessionName);
            StartParsingOrders(project.Profile);
            int executionResult = 0;
            return executionResult;
        }

        private void StartParsingOrders(IProfile project)
        {
            using (RussianProxyStream proxyStream = new RussianProxyStream())
            {
                WbAccountOrdersParser ordersParser = new WbAccountOrdersParser(proxyStream.GetProxy(), project);
                var orders = ordersParser.GetArchiveProducts();
                string test = "";
            }
        }

        private void LoadProfile(IProfile project, string sessionName)
        {
            WorkSettings workSettings = new WorkSettings();
            string pathToZpProfilese = workSettings.GetSettings().PathToZpProfiles;
            project.Load($"{pathToZpProfilese}{sessionName}.zpprofile");
        }
    }
}