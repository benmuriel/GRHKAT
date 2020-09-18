function addDays(date, days) {
    var dat = new Date(date);
    dat.setDate(date.getDate() + days);
    return dat;
}

$('form').submit(function () {
    var btn = $(this).children('button[type="submit"]').first();
    btn.text('');
    btn.addClass("spinner-border text-success");
});
function initFilter() {
$('.searchField').keyup(function () {
    var me = $(this);
    var text = me.val().trim().toLocaleLowerCase();
    var target = me.attr('data-target') + ' tr.searchable';
    var foundtgr = me.attr('data-found');
    //$(target).show();
    var found = 0;
    $(target).each(function () {
        var me = $(this);
        me.hide();
        if (me.attr('data-content').toLocaleLowerCase().indexOf(text) >= 0 || text === '') {
            me.show();
            ++found;
        }
    });
    $(foundtgr).text(found);
});
}


function formatDate(date) {
    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}
var date = new Date();
var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());

//$('input[type="date"].datepicker-before-today')
//    .attr("min", "2013-12-25")
//    .attr("max", formatDate(today));


function dateSelected(sel, day, el) {
    //  console.log(sel) 
    var target = '#' + el.attr("data-target");
    var dialog = '#datepicker_' + el.attr("data-target");

    var date = new Date()
    date.setTime(sel[0])
    $(target).val(date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear())
    Metro.dialog.close(dialog)
}

$(".poste-filter").change(function () {
    var url = $(this).attr("dataurl");
    var target = $(this).attr("datatarget");
    $(target).html('');
    var id = $(this).val();
    $.ajax({
        url: url + "/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: JSON,
        success: function (result) {
            $('#rs_count').text(result.length)
            $(result).each(function () {
                $(target).append($("<option></option>").val(this.id).html(this.fonction));
            });
            // $('#poste_id').selectpicker();
        }, error: function (data) {
            console.log(data)
        }
    });
});

$(".service-name").change(function () {

    var url = $(this).attr("dataurl");
    var target = $(this).attr("datatarget");


    $(target).html('');
    var id = $(this).val();

    $.ajax({
        url: url + "/" + id,
        type: "GET",
        success: function (result) {
            $(target).text(result)
        }, error: function (data) {
            console.log(data)
        }
    });
});
var loader = null;
function loaderOpen() {
    if (loader !== null) return
    loader = Metro.infobox.create("Chargement en cours ...",
        "",
        {
            closeButton: false,
            onOpen: function () {
                var ib = $(this).data("infobox");
            }
        }
    );
}

function loaderClose() {
    if (loader !== null) {
        Metro.infobox.close(loader);
        loader = null
    }
}