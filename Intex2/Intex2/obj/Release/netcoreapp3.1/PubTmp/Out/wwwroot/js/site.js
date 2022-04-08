// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//This method confrims whether you want the accident to be deleted or not
function ConfirmDelete(crashid) {





    if (confirm('Are you sure you want to save this thing into the database?')) {
        // Save it!

        //sends ajax request to delete if confrimed is true
        $.ajax({
            url: "/Home/Delete",
            data: { "crashid": crashid },
            type: 'POST'
        })

        location.reload(true);
        alert("Delete successful.")

    } else {
        // Do nothing!
        alert("Delete Cancelled");
    }
}


$(document).ready(function () {
    // Setup - add a text input to each footer cell
    $('#mytable tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input  type="text" class="align-content-center" placeholder="filter by ' + title.toLowerCase() + '" />');
    });

    // DataTable
    var table = $('#mytable').DataTable({
        "autoWidth": false,
        "columns": [
            { "width": "20%" },
            null, // automatically calculates
            null  // remaining width
        ],
        "destroy": true,
        "scrollY": "450px",
        "scrollX": "450px",
        "scrollCollapse": true,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "paging": true,

        "scrollX": true,

        initComplete: function () {
            // Apply the search
            this.api().columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });


});

