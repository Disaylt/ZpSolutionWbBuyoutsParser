﻿using System;
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
using ZpSolutionWbBuyoutsParser.OrdersManager;
using ZpSolutionWbBuyoutsParser.Parser;
using ZpSolutionWbBuyoutsParser.Proxy;
using ZpSolutionWbBuyoutsParser.WbStorage;
using ZpSolutionWbBuyoutsParser.ZennoPosterProjectObjects;

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
            ZennoPosterProfile zennoPosterProfile = new ZennoPosterProfile(project.Profile, sessionName);
            zennoPosterProfile.Load();
            StartParsingOrders(zennoPosterProfile);
            return 0;
        }

        private void StartParsingOrders(ZennoPosterProfile zpProfile)
        {
            using (RussianProxyStream proxyStream = new RussianProxyStream())
            {
                WbAccountOrdersParser ordersParser = new WbAccountOrdersParser(proxyStream.GetProxy(), zpProfile.Profile);
                ActivateArchiveOrdersManager(ordersParser, zpProfile);
                ActivateActiveOrdersManager(ordersParser, zpProfile);
            }
        }

        private void ActivateArchiveOrdersManager(WbAccountOrdersParser wbAccountOrdersParser, ZennoPosterProfile zpProfile)
        {
            IOrderArchiveStatusConverter orderArchiveStatusConverter = new ArchiveOrderStatusConverterV1();
            IOrdersManager archiveOrdersManager = new ArchiveOrdersManager(wbAccountOrdersParser, zpProfile, orderArchiveStatusConverter);
            archiveOrdersManager.UpdateOrdersData();
        }

        private void ActivateActiveOrdersManager(WbAccountOrdersParser wbAccountOrdersParser, ZennoPosterProfile zpProfile)
        {
            IOrderActiveStatusConverter orderActiveStatusConverter = new ActiveOrderStatusConverterV1();
            IOrdersManager activeOrdersManager = new ActiveOrdersManager(wbAccountOrdersParser, zpProfile, orderActiveStatusConverter);
            activeOrdersManager.UpdateOrdersData();
        }
    }
}