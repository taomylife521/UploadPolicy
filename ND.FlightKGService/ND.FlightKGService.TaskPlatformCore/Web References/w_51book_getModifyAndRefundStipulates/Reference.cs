﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.18444 版自动生成。
// 
#pragma warning disable 1591

namespace ND.FlightKGService.TaskPlatformCore.w_51book_getModifyAndRefundStipulates {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="GetModifyAndRefundStipulatesServiceImpl_1_0ServiceSoapBinding", Namespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
        ".com/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(abstractLiantuoReply))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(abstractLiantuoRequest))]
    public partial class GetModifyAndRefundStipulatesServiceImpl_1_0Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getModifyAndRefundStipulatesOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public GetModifyAndRefundStipulatesServiceImpl_1_0Service() {
            this.Url = global::ND.FlightKGService.TaskPlatformCore.Properties.Settings.Default.ND_FlightKGService_TaskPlatformCore_w_51book_getModifyAndRefundStipulates_GetModifyAndRefundStipulatesServiceImpl_1_0Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getModifyAndRefundStipulatesCompletedEventHandler getModifyAndRefundStipulatesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
            ".com/", ResponseNamespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
            ".com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public getModifyAndRefundStipulatesReply getModifyAndRefundStipulates([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] getModifyAndRefundStipulatesRequest request) {
            object[] results = this.Invoke("getModifyAndRefundStipulates", new object[] {
                        request});
            return ((getModifyAndRefundStipulatesReply)(results[0]));
        }
        
        /// <remarks/>
        public void getModifyAndRefundStipulatesAsync(getModifyAndRefundStipulatesRequest request) {
            this.getModifyAndRefundStipulatesAsync(request, null);
        }
        
        /// <remarks/>
        public void getModifyAndRefundStipulatesAsync(getModifyAndRefundStipulatesRequest request, object userState) {
            if ((this.getModifyAndRefundStipulatesOperationCompleted == null)) {
                this.getModifyAndRefundStipulatesOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetModifyAndRefundStipulatesOperationCompleted);
            }
            this.InvokeAsync("getModifyAndRefundStipulates", new object[] {
                        request}, this.getModifyAndRefundStipulatesOperationCompleted, userState);
        }
        
        private void OngetModifyAndRefundStipulatesOperationCompleted(object arg) {
            if ((this.getModifyAndRefundStipulatesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getModifyAndRefundStipulatesCompleted(this, new getModifyAndRefundStipulatesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
        ".com/")]
    public partial class getModifyAndRefundStipulatesRequest : abstractLiantuoRequest {
        
        private string lastModifiedAtField;
        
        private int lastSeatIdField;
        
        private bool lastSeatIdFieldSpecified;
        
        private string param1Field;
        
        private string param2Field;
        
        private string param3Field;
        
        private string param4Field;
        
        private int rowPerPageField;
        
        private bool rowPerPageFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lastModifiedAt {
            get {
                return this.lastModifiedAtField;
            }
            set {
                this.lastModifiedAtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int lastSeatId {
            get {
                return this.lastSeatIdField;
            }
            set {
                this.lastSeatIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool lastSeatIdSpecified {
            get {
                return this.lastSeatIdFieldSpecified;
            }
            set {
                this.lastSeatIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param1 {
            get {
                return this.param1Field;
            }
            set {
                this.param1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param2 {
            get {
                return this.param2Field;
            }
            set {
                this.param2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param3 {
            get {
                return this.param3Field;
            }
            set {
                this.param3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param4 {
            get {
                return this.param4Field;
            }
            set {
                this.param4Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int rowPerPage {
            get {
                return this.rowPerPageField;
            }
            set {
                this.rowPerPageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool rowPerPageSpecified {
            get {
                return this.rowPerPageFieldSpecified;
            }
            set {
                this.rowPerPageFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(getModifyAndRefundStipulatesRequest))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
        ".com/")]
    public abstract partial class abstractLiantuoRequest {
        
        private string agencyCodeField;
        
        private string signField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string agencyCode {
            get {
                return this.agencyCodeField;
            }
            set {
                this.agencyCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sign {
            get {
                return this.signField;
            }
            set {
                this.signField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
        ".com/")]
    public partial class modifyAndRefundStipulateVo {
        
        private string airlineCodeField;
        
        private double changePercentAfterField;
        
        private bool changePercentAfterFieldSpecified;
        
        private double changePercentBeforeField;
        
        private bool changePercentBeforeFieldSpecified;
        
        private string changeStipulateField;
        
        private int changeTimePointField;
        
        private bool changeTimePointFieldSpecified;
        
        private string modifiedAtField;
        
        private string modifyStipulateField;
        
        private string param1Field;
        
        private string param2Field;
        
        private string param3Field;
        
        private string param4Field;
        
        private double refundPercentAfterField;
        
        private bool refundPercentAfterFieldSpecified;
        
        private double refundPercentBeforeField;
        
        private bool refundPercentBeforeFieldSpecified;
        
        private string refundStipulateField;
        
        private int refundTimePointField;
        
        private bool refundTimePointFieldSpecified;
        
        private string seatCodeField;
        
        private int seatIdField;
        
        private bool seatIdFieldSpecified;
        
        private int seatTypeField;
        
        private bool seatTypeFieldSpecified;
        
        private string suitableAirlineField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string airlineCode {
            get {
                return this.airlineCodeField;
            }
            set {
                this.airlineCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double changePercentAfter {
            get {
                return this.changePercentAfterField;
            }
            set {
                this.changePercentAfterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool changePercentAfterSpecified {
            get {
                return this.changePercentAfterFieldSpecified;
            }
            set {
                this.changePercentAfterFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double changePercentBefore {
            get {
                return this.changePercentBeforeField;
            }
            set {
                this.changePercentBeforeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool changePercentBeforeSpecified {
            get {
                return this.changePercentBeforeFieldSpecified;
            }
            set {
                this.changePercentBeforeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string changeStipulate {
            get {
                return this.changeStipulateField;
            }
            set {
                this.changeStipulateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int changeTimePoint {
            get {
                return this.changeTimePointField;
            }
            set {
                this.changeTimePointField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool changeTimePointSpecified {
            get {
                return this.changeTimePointFieldSpecified;
            }
            set {
                this.changeTimePointFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string modifiedAt {
            get {
                return this.modifiedAtField;
            }
            set {
                this.modifiedAtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string modifyStipulate {
            get {
                return this.modifyStipulateField;
            }
            set {
                this.modifyStipulateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param1 {
            get {
                return this.param1Field;
            }
            set {
                this.param1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param2 {
            get {
                return this.param2Field;
            }
            set {
                this.param2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param3 {
            get {
                return this.param3Field;
            }
            set {
                this.param3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param4 {
            get {
                return this.param4Field;
            }
            set {
                this.param4Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double refundPercentAfter {
            get {
                return this.refundPercentAfterField;
            }
            set {
                this.refundPercentAfterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool refundPercentAfterSpecified {
            get {
                return this.refundPercentAfterFieldSpecified;
            }
            set {
                this.refundPercentAfterFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double refundPercentBefore {
            get {
                return this.refundPercentBeforeField;
            }
            set {
                this.refundPercentBeforeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool refundPercentBeforeSpecified {
            get {
                return this.refundPercentBeforeFieldSpecified;
            }
            set {
                this.refundPercentBeforeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string refundStipulate {
            get {
                return this.refundStipulateField;
            }
            set {
                this.refundStipulateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int refundTimePoint {
            get {
                return this.refundTimePointField;
            }
            set {
                this.refundTimePointField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool refundTimePointSpecified {
            get {
                return this.refundTimePointFieldSpecified;
            }
            set {
                this.refundTimePointFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string seatCode {
            get {
                return this.seatCodeField;
            }
            set {
                this.seatCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int seatId {
            get {
                return this.seatIdField;
            }
            set {
                this.seatIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool seatIdSpecified {
            get {
                return this.seatIdFieldSpecified;
            }
            set {
                this.seatIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int seatType {
            get {
                return this.seatTypeField;
            }
            set {
                this.seatTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool seatTypeSpecified {
            get {
                return this.seatTypeFieldSpecified;
            }
            set {
                this.seatTypeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string suitableAirline {
            get {
                return this.suitableAirlineField;
            }
            set {
                this.suitableAirlineField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(getModifyAndRefundStipulatesReply))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
        ".com/")]
    public abstract partial class abstractLiantuoReply {
        
        private string returnCodeField;
        
        private string returnMessageField;
        
        private string returnStackTraceField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string returnCode {
            get {
                return this.returnCodeField;
            }
            set {
                this.returnCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string returnMessage {
            get {
                return this.returnMessageField;
            }
            set {
                this.returnMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string returnStackTrace {
            get {
                return this.returnStackTraceField;
            }
            set {
                this.returnStackTraceField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://getmodifyandrefundstipulates.b2b.service.version1_0.webservice.model.ltips" +
        ".com/")]
    public partial class getModifyAndRefundStipulatesReply : abstractLiantuoReply {
        
        private int leftPagesField;
        
        private bool leftPagesFieldSpecified;
        
        private modifyAndRefundStipulateVo[] modifyAndRefundStipulateListField;
        
        private string param1Field;
        
        private string param2Field;
        
        private string param3Field;
        
        private string param4Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int leftPages {
            get {
                return this.leftPagesField;
            }
            set {
                this.leftPagesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool leftPagesSpecified {
            get {
                return this.leftPagesFieldSpecified;
            }
            set {
                this.leftPagesFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("modifyAndRefundStipulateList", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public modifyAndRefundStipulateVo[] modifyAndRefundStipulateList {
            get {
                return this.modifyAndRefundStipulateListField;
            }
            set {
                this.modifyAndRefundStipulateListField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param1 {
            get {
                return this.param1Field;
            }
            set {
                this.param1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param2 {
            get {
                return this.param2Field;
            }
            set {
                this.param2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param3 {
            get {
                return this.param3Field;
            }
            set {
                this.param3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string param4 {
            get {
                return this.param4Field;
            }
            set {
                this.param4Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void getModifyAndRefundStipulatesCompletedEventHandler(object sender, getModifyAndRefundStipulatesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getModifyAndRefundStipulatesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getModifyAndRefundStipulatesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public getModifyAndRefundStipulatesReply Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((getModifyAndRefundStipulatesReply)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591