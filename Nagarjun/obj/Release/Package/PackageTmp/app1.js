Ext.onReady(function () {
    // setup the state provider, all state information will be saved to a cookie
    Ext.namespace('EXT');
    Ext.define('Actors', {
        extend: 'Ext.data.Model',
        fields: ['FirstName', 'LastName', 'EmailAddress', 'Salary']
    });


    var store = new Ext.data.Store(
        {
            proxy: new Ext.ux.AspWebAjaxProxy({
                url: '../Employeetc.asmx/LoadRecords',
                actionMethods: {
                    read: 'POST'
                },
                reader: {
                    type: 'json',
                    model: 'Actors',
                    root: 'd'
                },
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            })
        });

    // create the grid
    var grid = Ext.create('Ext.grid.Panel', {
        store: store,
        stateful: true,
        width: 835,
        height: 326,
        stateId: 'stateGrid',
        columns: [
            { text: 'FirstName', dataIndex: 'FirstName', width: 280, sortable: true },
            { text: 'LastName', dataIndex: 'LastName', sortable: true },
            { text: 'EmailAddress', dataIndex: 'EmailAddress', width: 150, sortable: true },
            { text: 'Salary', dataIndex: 'Salary', renderer: 'usMoney', sortable: true }
        ],        
        renderTo: 'ext-grid'
    });

    store.load();
});