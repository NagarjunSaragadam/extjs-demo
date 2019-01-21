Ext.application({
    name: 'SSC',    
    launch: function(){     
/////////////////////////////////////////////////////////////////////////////////////////////////
Ext.define('Employee', {
    extend: 'Ext.data.Model',
    fields:  [ 'Sno','Username','Gender','Email', 'Phonenumber' ],
});
 var URLdata = "http://localhost/webapitest/api/Home/getraw";
      var storedata = new Ext.data.Store({
          fields: [ 'Sno','Username','Gender','Email', 'Phonenumber' ],
            proxy: {
                type: 'rest',
               url:URLdata,
                reader: {
                    type: 'json',
                    root: 'items',                   
                }
            }
        });
storedata.load();

var filterPanel1 = Ext.create('Ext.grid.Panel', {    
    renderTo: document.body,
    id: 'Usrgrid',
    store: storedata,        
    width: 520,
    height: 280,
	margin: '5 1 10 5',
    title: 'Application Users',
    columns: [
	{	
		xtype: 'rownumberer',
		width: 20,
		sortable: true		
    }, 
	{
		text: 'Sno',
		width: 50,
		sortable: true,
		hideable: false,
		dataIndex: 'Sno'
    },	
	{
		text: 'Name',
		width: 100,
		sortable: false,
		hideable: false,
		dataIndex: 'Username'
    },
    {
		text: 'Gender',
		width: 70,
		sortable: false,
		hideable: false,
		dataIndex: 'Gender'
    },	
    {
		text: 'Email Address',
		width: 150,
		dataIndex: 'Email'            
    },
    {
        text: 'Phone Number',
        width: 200,
		flex: 1,
		dataIndex: 'Phonenumber'
    }
    ]
});
filterPanel1.on('rowclick', function(grid, rowIndex, columnIndex, e) {
    if (filterPanel1.getSelectionModel().hasSelection()) {
        var row = filterPanel1.getSelectionModel().getSelection()[0];        
        Ext.get('accountID').dom.value=row.get('Sno');
        var v = Ext.get('accountID').dom.value;            
        var name = Ext.getCmp('idusername').setValue(row.get('Username'));
        var gender = Ext.getCmp('idgender').setValue({ugenderreq:row.get('Gender')});
        var email = Ext.getCmp('idemail').setValue(row.get('Email'));
        var phone = Ext.getCmp('idphonenumber').setValue(row.get('Phonenumber'));        
        var submitbutton=Ext.getCmp('sbmtBtn').setText("Update");                   
    }
}, this);
//////////////////////////////////////////////////////////////////////////////////////////////////////
var filterPanel2 = Ext.create('Ext.form.Panel', {
    id: 'UsrFrm',
    bodyPadding: 5,  
    width: 460,height: 280,
    margin: '5 1 10 5',
    title: 'Transactions',
    items: [{
        xtype: 'textfield',
        name: 'uName',
        fieldLabel: 'Name',
        id: 'idusername',
        width: 400,
        allowBlank: false,
        required: true,
        blankText : 'Please enter Username',
            },				
            {
                xtype: 'radiogroup',  
                fieldLabel: 'Gender',
                id: 'idgender',
                name: 'ugender',
                anchor: '70%',
                columns: 2,
                defaults: {xtype: "radio",name: "genderr"},
                items: [
                    { boxLabel: 'Male', name: 'ugenderreq', inputValue: 'Male', checked: true },
                    { boxLabel: 'Female', name: 'ugenderreq', inputValue: 'Female' }

                ]
            },            
 	        {
        xtype: 'textfield',
        name: 'uemail',
        id: 'idemail',
        width: 400,
        fieldLabel: 'Email Address',
        allowBlank: false,
        required: true,        
        regex:/^((([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z\s?]{2,5}){1,25})*(\s*?;\s*?)*)*$/,
        regexText:'This field must contain single or multiple valid email addresses separated by semicolon (;)',
        blankText : 'Please enter email address(s)',
            },
	        {
        xtype: 'textfield',
        name: 'uphonenumber',
        width: 400,
        id: 'idphonenumber',
        fieldLabel: 'Phone number',
        allowBlank: false,
        required: true,
        regex: /^[0-9]+(?:\.[0-9]+)?$/,
        regexText:'This field must contain phone number',
        blankText : 'Please enter phonenumber',
            },		
			{        
        xtype: 'panel',  
        border:0,		
		items: [ 
            {
                xtype: 'button',
                width: '49%',
                margin: '0px 2px 0 0px', 
                text: 'Reset',
                listeners: {            
                    click: function () {                                               
                         var formres = Ext.getCmp('UsrFrm');                               
                         formres.getForm().reset();
                        }
                    }
            },
            {
                xtype: 'button',
                text: 'Add',
                width: '50%',
                id: 'sbmtBtn',
                listeners: {
                    click: function () {
                        var form = Ext.getCmp('UsrFrm');
                        var values = form.getValues();
                        if (form.isValid()) {   
                            var submitbuttondis=Ext.getCmp('sbmtBtn').getText();     
                            if(submitbuttondis=="Add")
                            {
                            var Employee = {};
                            Employee.Username = values.uName;
                            Employee.Gender = values.ugenderreq;
                            Employee.Email = values.uemail;
                            Employee.Phonenumber = values.uphonenumber;
                            Employee.Openumber='1';
                            Ext.Ajax.request({
                                url: 'http://localhost/webapitest/api/Home/transctionsdatabypost',
                                method: 'Post',
                                jsonData: Employee,
                                success: function () {                                                                        
                                    Ext.getCmp('Usrgrid').store=storedata;
                                    storedata.load();
                                    Ext.getCmp('Usrgrid').getView().refresh();                                                                                                                                                
                                    alert('Employee inserted..'); 
                                },
                                failure: function () {
                                    alert('fail');
                                }
                            });
                            }
                            else
                            {
                                var v = Ext.get('accountID').dom.value;            
                                var Employee = {};
                                Employee.Sno = v;
                                Employee.Username = values.uName;
                                Employee.Gender = values.ugenderreq;
                                Employee.Email = values.uemail;
                                Employee.Phonenumber = values.uphonenumber;
                                Employee.Openumber='2';
                                Ext.Ajax.request({
                                    url: 'http://localhost/webapitest/api/Home/transctionsdatabypost',
                                    method: 'Post',
                                    jsonData: Employee,
                                    success: function () {                                       
                                        Ext.getCmp('Usrgrid').store=storedata;
                                        storedata.load();
                                        Ext.getCmp('Usrgrid').getView().refresh();                                                                                                                                                    
                                        alert('Employee updated..');                                     
                                    },
                                    failure: function () {
                                        alert('fail');
                                    }
                                });   
                            }

                        }
                        else alert('Invalid Form');
    }
    }
    }   
		    ]
            },
	],
    renderTo: Ext.getBody()
});
////////////////////////////////////////////////////////////////////////////////////
var p = new Ext.Panel( {
		title: 'Ext JS 6 practice',
		renderTo: document.body,
		margin: '5 0 0 0',
		width: 1000,
		height: 348,
        sortable: true,		
        header:{
            titlePosition: 0,
            items:[
                {   
                    xtype: 'panel',                  
                    border:0,	
                    layout: 'hbox',	
                    frame: false,
                    border: false,
                    bodyStyle: 'background:transparent',  
                    items: [ 
                        {
                            xtype: 'button',
                            width: '100px',
                            margin: '0px 10px 0 0px', 
                            text: 'Add',                        
                            id: 'sbmtBtnadd',
                            iconCls  : 'fa fa-plus-circle',                        
                            listeners: {            
                                click: function () {                                                                                    
                                     Ext.get('accountID').dom.value='';
                                     var formres = Ext.getCmp('UsrFrm');                               
                                     formres.getForm().reset();   
                                     var submitbutton=Ext.getCmp('sbmtBtn').setText("Add");                                   
                                    }
                                }
                        },                        
                        {
                            xtype: 'button',
                            text: 'Delete',
                            iconCls  : 'fa fa-trash',
                            width: '100px',
                            margin: '0px 10px 0 0px', 
                            id: 'sbmtBtndel',                        
                            listeners: {
                                click: function () {                          
                                    var empsno = Ext.get('accountID').dom.value;
                                    var Employee = {};
                                    Employee.Sno = empsno;                                                                
                                    Employee.Openumber='0';
                                    Ext.Ajax.request({
                                        url: 'http://localhost/webapitest/api/Home/transctionsdatabypost',
                                        method: 'Post',                                        
                                        jsonData: Employee,
                                        success: function () {
                                            Ext.getCmp('Usrgrid').store=storedata;
                                            storedata.load();
                                            Ext.getCmp('Usrgrid').getView().refresh();      
                                            Ext.getCmp('Usrgrid').getView().ds.reload();
                                            alert('Employee deleted..');                                            
                                        },
                                        failure: function () {
                                            alert('fail');
                                        }
                                    });
                                    }
                                }
                        }           
                        ]
            },
            ]    
        },
		items: [             
                  {   
                        xtype: 'panel',                  
                        border:0,	
                        layout: 'hbox',	
                        items: [filterPanel1,filterPanel2]
                   },     
            ]
    });
//////////////////////////////////////////////////////////////////////////////////////////////
    },
});





