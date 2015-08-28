var datosCalendar = "";
var modoEvent = 0;
var idEvento = 0;
$(document).ready(function () {
    llenaComboit('');
    cargarDatosCalendario();
    

    $("#btnGuardarCalendario").click(function () {
        if (validaCampo('')) {
            var msg = validarCalendario();
            if (msg == "") {
                guardarCalendario();
            }
            else {
                alert(msg);
            }
        }
    });



    var calendar = $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        selectable: true,
        selectHelper: true,
        events: datosCalendar,
        eventClick: function (calEvent, jsEvent, view) {

            /*alert('Event: ' + calEvent.title);
            alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
            alert('View: ' + view.name);*/
            
            getEvento(calEvent.id);

            

            // change the border color just for fun
           // $(this).css('border-color', 'red');

        },
        dayClick: function () {
            limpiarGeneral();
            $("#mdlAddEvento").modal('show');
        }
    });
   
});

function validarCalendario() {
    var retorna = '';
    var param = "{'modo':" + modoEvent + ",'idEvento':" + idEvento + ",'idSalon':" + $('#AdmSalCodigo').val() + ",'titulo':'" + $("#titulo").val() + "'," +
        "'descEvento':'" + $("#descEvento").val() + "'," +
        "'fechaInicio':'" + $("#fechaInicio").val() + "'," +
        "'fechaFin':'" + $("#fechaFin").val() + "'," +
        "'horaInicio':'" + $("#horaInicio").val() + "'," +
        "'horaFin':'" + $("#horaFin").val() + "'}";

    $.ajax({
        url: 'uti.aspx/validarCalendario',
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: param,
    })
     .done(function (r) {
         
         retorna = r.d

       
     })
     .fail(function () {
    
     })
     .always(function () {
    
     });

    return retorna;
}
function guardarCalendario() {
    var param = "{'modo':" + modoEvent + ",'idEvento':" + idEvento + ",'idSalon':" + $('#AdmSalCodigo').val() + ",'titulo':'" + $("#titulo").val() + "'," +
        "'descEvento':'" + $("#descEvento").val() + "'," +
        "'fechaInicio':'" + $("#fechaInicio").val() + "'," +
        "'fechaFin':'" + $("#fechaFin").val() + "'," +
        "'horaInicio':'" + $("#horaInicio").val() + "'," +
        "'horaFin':'" + $("#horaFin").val() + "'}";

   $.ajax({
       url: 'uti.aspx/gabarCalendario',
        type: 'POST',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: param,
    })
	.done(function (r) {
	    location.reload();
	    modoEvent = 0;
	    //alert(r.d)
	    
        console.log("success");
	})
	.fail(function () {
		console.log("error");
	})
	.always(function () {
		console.log("complete");
	});
}

function cargarDatosCalendario(){
    $.ajax({
        url: 'uti.aspx/cargarCalendario',
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8"
        //data: param,
    })
     .done(function (r) {
         if (r.d != -1) {
             datosCalendar= eval(r.d)//.slice(0, -1);
         }else{
             alert(r.d);
        }
         console.log("success");
     })
     .fail(function () {
         console.log("error");
     })
     .always(function () {
         console.log("complete");
     });
}

function getEvento(idEvent) {
   
    $.ajax({
        url: 'uti.aspx/getEvento',
        type: 'POST',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: "{'idEvento':" + idEvent + "}",
    })
	.done(function (r) {
	    //location.reload();
	    eval(r.d);
	    $("#mdlAddEvento").modal('show');
	    modoEvent = 1;
	    idEvento = idEvent;
	})
	.fail(function () {

	})
	.always(function () {

	});
   
}
