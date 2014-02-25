IocCore
=======

IocCore是一个.Net环境下基于Castle的可插拔开发组件库
============================================

组件包括：
    1.日志组件
    2.异常处理组件
    3.数据库操作组件
    4.缓存组件
    5.权限控制组件
    6.消息通知组件
    ……
    

    使用前请先了解IOC，AOP相关概念



配置文件
-----------------------------------------------
组件库配置文件基于Castle的配置文件
用于设置使用Castle动态代理的接口、功能实现类，以及设置拦截器
一个完整的配置文件的示例

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <components>
    <!--Ioc全局配置-->
    <component id="IocGlobalConfig" type="IocCore.Helper.IocGlobalConfig,IocCore" lifestyle="singleton">
      <parameters>
        <!--全局log级别，各Ioc拦截器的log级别若低于全局log级别，拦截器内log级别将被设置为与全局级别相同-->
        <LogLevel>debug</LogLevel>
      </parameters>
    </component>

    <!--注册测试类-->
    <component id="IocCoreTest" type="IocDemo.IocCoreTest, IocDemo"
               inspectionBehavior="all">
      <interceptors>
        <interceptor>${ExHandleInterceptor}</interceptor>
        <interceptor>${PermissionPointInterceptor}</interceptor>
        <interceptor>${LogInterceptor}</interceptor>
        <interceptor>${CacheInterceptor}</interceptor>
      </interceptors>
    </component>
    
    <!--数据库操作组件注册-->
    <component id="OracleOp" service="IocCore.IocDbOp.IDbOp, IocCore" type="IocPlugin.IocDbOp.OracleOp.OracleOp, IocPlugin.IocDbOp.OracleOp"
        inspectionBehavior="all"
        lifestyle="singleton">
    </component>
    
    <!--日志 拦截器-->
    <component id="LogInterceptor" type="IocCore.IocLog.LogInterceptor, IocCore">
      <parameters>
        <LogLevel>debug</LogLevel>
      </parameters>
    </component>
    <!--日志组件注册-->
    <component id="log4net" service="IocCore.IocLog.ILog, IocCore" type="IocPlugin.IocLog.Log4net.Log4netProxy, IocPlugin.IocLog.Log4net"
      inspectionBehavior="all" lifestyle="singleton">
      <parameters>
        <ConfigFile>ClientBin\log4net.xml</ConfigFile>
      </parameters>
    </component>
    
    <!--异常处理 拦截器-->
    <component id="ExHandleInterceptor" type="IocCore.IocExHandle.ExHandleInterceptor, IocCore">
      <parameters>
        <LogLevel>debug</LogLevel>
      </parameters>
    </component>
    
    <!--权限 拦截器-->
    <component id="PermissionPointInterceptor" type="IocCore.IocPermission.PermissionPointInterceptor, IocCore">
      <parameters>
        <LogLevel>debug</LogLevel>
      </parameters>
    </component>
    <!--权限组件注册-->
    <component id="DefaultAccessDecider" service="IocCore.IocPermission.IAccessDecider, IocCore" type="IocCore.IocPermission.DefaultAccessDecider, IocCore">
    </component>
    <component id="DefaultPermissionPointResolve" service="IocCore.IocPermission.IPermissionPointResolve, IocCore" type="IocCore.IocPermission.DefaultPermissionPointResolve, IocCore">
    </component>

    <!--缓存 拦截器-->
    <component id="CacheInterceptor" type="IocCore.IocCache.CacheInterceptor, IocCore">
      <parameters>
        <LogLevel>debug</LogLevel>
      </parameters>
    </component>
    <!--缓存组件注册-->
    <component id="DotNetCache" service="IocCore.IocCache.ICache,IocCore" type="IocPlugin.IocCache.DotNetCache.DotNetCache, IocPlugin.IocCache.DotNetCache">
    </component>
    <!--缓存依赖包装器注册-->
    <component id="DBDependency" service="IocCore.IocDependency.IDependencyWrapper,IocCore" type="IocPlugin.IocDbOp.OracleOp.OracleCacheDependencyWrapper, IocPlugin.IocDbOp.OracleOp">
    </component>
    <component id="DbConnDependency" service="IocCore.IocDependency.IDependencyWrapper,IocCore" type="IocPlugin.IocCache.DotNetCache.DbConnDependencyWrapper, IocPlugin.IocCache.DotNetCache">
    </component>
    <component id="AggregateDependency" service="IocCore.IocDependency.IDependencyWrapper,IocCore" type="IocPlugin.IocCache.DotNetCache.AggregateCacheDependencyWrapper, IocPlugin.IocCache.DotNetCache">
    </component>
  </components>
</configuration>
