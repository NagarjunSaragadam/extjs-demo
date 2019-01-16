Ext.onReady(function () {
    Ext.create('Ext.form.Panel', {//use class for form panel
        width: 500,
        height: 300,
        padding: 10,
        id: 'UsrFrm',
        layout: {   //define layout types
            type: 'vbox',
            pack: 'center',
            align: 'center'
        },
        title: "User Form",
        layout: 'form',
        renderTo: document.body,//associate with body tag
        bodyPadding: 5,
        items: [{
            xtype: 'textfield',
            name: 'uName',
            fieldLabel: 'Name',
            allowBlank: false
        }, {
            xtype: 'textfield',
            name: 'fName',
            fieldLabel: 'Father Name'
        }, {
            xtype: 'datefield',
            name: 'dob',
            fieldLabel: 'D O B'
        }, 
        {
            xtype: 'textfield',
            name: 'Mobile',
            fieldLabel: 'Mobile No.',            
        },
        {
            xtype: 'textareafield',
            name: 'addrss',
            fieldLabel: 'Address',
            maxValue: 250
        }, {
            xtype: 'numberfield',
            name: 'cntct',
            fieldLabel: 'Contact No.',
            maxValue: 9999999999,
        }, {
            xtype: 'button',
            text: 'Add',
            width: '100%',
            id: 'sbmtBtn',
            listeners: {            
                click: function () {
                    var form = Ext.getCmp('UsrFrm');
                    var values = form.getValues();
                    Ext.Ajax.request({
                        url: 'http://localhost:61642/Employeetc.asmx/insertdata',
                        method: 'POST',        
                            params: {
                        uName:values.uName,
                        fName:values.fName,
                        uDob: values.dob,
                        Mobile:values.Mobile,
                        uAddrss:values.addrss,
                        contact:values.cntct
                            },
                    success: function () {
                        alert('success');
                    },
                    failure: function () {
                        alert('fail');
                    }
 
                });
}
}
}],
});


Ext.create("Ext.panel.Panel", {
    width: 800,
    height: 500,
    renderTo: document.body,
    layout: 'fit',
    items: [{
        xtype: 'grid',            
        columns: [{
            text: 'Id',
            dataIndex: 'Sno',
            flex: 1
        }, {
            text: 'First Name',
            dataIndex: 'FName',
            flex: 1
        }, {
            text: 'Last Name',
            dataIndex: 'LName',
            flex: 1
        }],
        store: {
            autoLoad: true,
            fields: ['Sno', 'FName', 'LName'],               
            proxy: {
                type: 'ajax',
                url: 'http://localhost:61642/Employeetc.asmx/GetusersList',                      
                reader: {
                    type: 'json',
                    rootProperty: 'd'
                }
            }
        }
    }]
});


});