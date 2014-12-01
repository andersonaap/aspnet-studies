$(document).ready(function () {
    var dataTable = null;

    var renderizarOpcoes = function (data, type, full, meta) {
        return ' \
<div class="btn-group">                                                                                       \
  <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false"> \
    <span class="glyphicon glyphicon-align-justify"></span>                                                   \
  </button>                                                                                                   \
  <ul class="dropdown-menu" role="menu">                                                                      \
    <li><a href="/Home/Action/"        data-id="' + data[0] + '">Action</a></li>                              \
    <li><a href="/Home/AnotherAction/" data-id="' + data[0] + '">Another action</a></li>                      \
  </ul>                                                                                                       \
</div>                                                                                                        '
    };

    $('#filtros').on('submit', function (e) {
        e.preventDefault();
        dataTable.draw();
    });

    $('#resultado').on('draw.dt', function () {
        $(this).find('a').on('click', function (e) {
            e.preventDefault();
            alert('get [' + this.href + '] para id [' + $(this).data('id') + ']');
        });
    });

    dataTable = $('#resultado').DataTable({
        "language": { "url": '/Scripts/DataTables-1.10.3/Portuguese-Brasil.json' },
        "autoWidth": true,
        "paging": true,
        "ordering": true,
        "filter": false,
        "lengthChange": false,
        "processing": true,
        "serverSide": true,
        "columnDefs": [
            { "targets": -1, "data": null, "name": "opcoes", "searchable": false, "sortable": false, "render": renderizarOpcoes }
        ],
        //"ajax": {
        //    "url": "/Compania",
        //    "type": 'POST',
        //    "dataType": 'json',
        //    "contentType": 'application/json',
        //    "data": function (d) {
        //        var pesquisaAdicional = d;
        //        $('#filtros').find('.dt-filtro').each(function () {
        //            pesquisaAdicional[this.name] = this.value;
        //        });
        //        //return pesquisaAdicional; 
        //        return JSON.stringify(pesquisaAdicional);
        //    }
        //}
        "sAjaxSource": "/Compania",
        "fnServerData": function (sSource, aoData, fnCallback) {
            $('#filtros .dt-filtro').each(function () {
                aoData.push({ "name": this.name, "value": this.value });
            });
            $.ajax({
                "dataType": 'json',
                "type": 'POST',
                "url": sSource,
                "contentType": 'application/x-www-form-urlencoded; charset=UTF-8', // 'application/json', //
                "data": aoData // JSON.stringify(aoData) //
            })
            .done(fnCallback)
        }
    });
});
