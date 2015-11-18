$(document).ready(function () {
    var dataTable = null;

    var renderizarOpcoes = function (data, type, full, meta) {
        return ' \
<div class="btn-group">                                                                                       \
  <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false"> \
    <span class="fa fa-bars"></span>                                                                          \
  </button>                                                                                                   \
  <ul class="dropdown-menu" role="menu">                                                                      \
    <li><a href="/Home/Action/"        data-id="' + full.id + '">Action</a></li>                              \
    <li><a href="/Home/AnotherAction/" data-id="' + full.id + '">Another action</a></li>                      \
  </ul>                                                                                                       \
</div>                                                                                                        '
    };

    $('#filtros').on('submit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        if ($(this).valid()) {
            dataTable.draw();
        }
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
        "info": true,
        "paging": true,
        "pageLength": 5,
        "ordering": true,
        "filter": false,
        "lengthChange": false,
        "scrollY": "200px",
        "processing": true,
        "serverSide": true,
        "columns": [
            { "data": "id", "orderable": false },
            { "data": "compania", "orderable": true, "orderData": [1, 2] },
            { "data": "pais", "orderable": true, "orderData": [2, 1] }, 
            { "data": "preco", "orderable": true },
            { "data": "options", "orderable": false, "title": "opções", "render": renderizarOpcoes }
        ],
        "ajax": {
            "url": "/Compania"
            , "type": 'POST'
            , "dataType": 'json'
            , "contentType": 'application/x-www-form-urlencoded; charset=UTF-8' // NOTE: 'application/json', nao funciona com DataTablesBinder
            , "data": function (d) {
                $('#filtros').find('.dt-filtro').each(function () {
                    d[this.name] = this.value;
                });
                return d;
            }
        }
    });
});
