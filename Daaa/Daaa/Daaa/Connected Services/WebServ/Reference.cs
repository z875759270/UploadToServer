﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Daaa.WebServ {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WebServ.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetYZM", ReplyAction="http://tempuri.org/IService/GetYZMResponse")]
        byte[] GetYZM(string checkcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/InsertGoodIntoDataBase", ReplyAction="http://tempuri.org/IService/InsertGoodIntoDataBaseResponse")]
        int InsertGoodIntoDataBase(string Name, int Price, string Type, string PicUrl, string LinkUrl, string VrUrl, string VrData, string remark);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/InsertVRIntoDataBase", ReplyAction="http://tempuri.org/IService/InsertVRIntoDataBaseResponse")]
        int InsertVRIntoDataBase(string name, string url, string prePicUrl, string type, string remark);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/VersionUpdate", ReplyAction="http://tempuri.org/IService/VersionUpdateResponse")]
        int VersionUpdate(string str);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetLatestVersion", ReplyAction="http://tempuri.org/IService/SetLatestVersionResponse")]
        int SetLatestVersion(string num, string time, string info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetLatestVersion", ReplyAction="http://tempuri.org/IService/GetLatestVersionResponse")]
        string[] GetLatestVersion();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetFolderNum", ReplyAction="http://tempuri.org/IService/GetFolderNumResponse")]
        int GetFolderNum();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : Daaa.WebServ.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<Daaa.WebServ.IService>, Daaa.WebServ.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public byte[] GetYZM(string checkcode) {
            return base.Channel.GetYZM(checkcode);
        }
        
        public int InsertGoodIntoDataBase(string Name, int Price, string Type, string PicUrl, string LinkUrl, string VrUrl, string VrData, string remark) {
            return base.Channel.InsertGoodIntoDataBase(Name, Price, Type, PicUrl, LinkUrl, VrUrl, VrData, remark);
        }
        
        public int InsertVRIntoDataBase(string name, string url, string prePicUrl, string type, string remark) {
            return base.Channel.InsertVRIntoDataBase(name, url, prePicUrl, type, remark);
        }
        
        public int VersionUpdate(string str) {
            return base.Channel.VersionUpdate(str);
        }
        
        public int SetLatestVersion(string num, string time, string info) {
            return base.Channel.SetLatestVersion(num, time, info);
        }
        
        public string[] GetLatestVersion() {
            return base.Channel.GetLatestVersion();
        }
        
        public int GetFolderNum() {
            return base.Channel.GetFolderNum();
        }
    }
}