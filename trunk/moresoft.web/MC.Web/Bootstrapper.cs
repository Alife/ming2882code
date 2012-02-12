using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using MC.DAO;
using MC.Service;
using MC.IBLL;

namespace Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<Imc_User, mc_UserService>();
            container.RegisterType<IKeywords_key, Keywords_keyService>();
            container.RegisterType<ILink_lnk, Link_lnkService>();
            container.RegisterType<IPage_pag, Page_pagService>();
            container.RegisterType<ISetting_set, Setting_setService>();
            container.RegisterType<IIndexTag_itg, IndexTag_itgService>();
            container.RegisterType<IInfo_inf, Info_infService>();
            container.RegisterType<IInfoType_ift, InfoType_iftService>();
            container.RegisterType<IRequire_req, Require_reqService>();
            //container.RegisterType<IDao, DaoImpl>(new HierarchicalLifetimeManager());
            container.RegisterType<IDao, DaoImpl>(new ContainerControlledLifetimeManager());//单例模式的注册
            container.RegisterControllers();

            return container;
        }
    }
}