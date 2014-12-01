$(document).ready(function () {
    var dataTable = null;

    $('#bt-submit').on('click', function (e) {
        e.preventDefault();
        dataTable.draw();
    })
    dataTable = $('#resultado').DataTable({
        "language": { "url": '/Content/DataTables-1.10.3/Portuguese-Brasil.txt' },
        "paging": true,
        "processing": true,
        "serverSide": true,
        "sAjaxSource": "/Home/Obter",
        "fnServerData": function (sSource, aoData, fnCallback) {
            $('.dt-filtro').each(function () {
                aoData.push({ "name": this.name, "value": this.value });
            });
            $.ajax({
                "dataType": 'json',
                "type": 'POST',
                "url": sSource,
                "contentType": 'application/x-www-form-urlencoded; charset=UTF-8', // 'application/json', //
                "data": aoData, // JSON.stringify(aoData), //
                "success": fnCallback
            })
        }
    });
});
/*
      "aoColumns": [
                      {   "sName": "ID",
                          "bSearchable": false,
                          "bSortable": false,
                          "fnRender": function (oObj) {
                              return '<a href=\"Company/Details/' + oObj.aData[0] + '\">View</a>';
                          }
                      },
                      { "sName": "COMPANY_NAME" },
                      { "sName": "ADDRESS" },
                      { "sName": "TOWN" }
                  ]
*/
