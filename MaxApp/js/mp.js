
var subConsulta = '';
var button = "";
var recipiente = "";
var valigridimag = 0;
var imgcount = 0;

$(document).ready(function () {
   
    llenaTagSelect();
    llenaComboB();


   // $(".modal-dialog").draggable({ handle: '.modal-header' });

    var depe = '';
    var depende = '';

    //$('#divConsultaGeneral').on('show.bs.modal', function (event) {

    //    button = $(event.relatedTarget);

    //    recipiente = button.data('whatever');

    //    depe = button.data('depende');
    //    if (depe != undefined) {
    //        depende = nombreCampo(depe) + '_' + $("#" + depe).val();
    //    }
    //    if (event.relatedTarget.id.indexOf("_w") > 0) {
    //        subConsulta = nombreCampo(event.relatedTarget.id);
    //        consultaGral(recipiente, depende);
    //    }
    //    else {
    //        consultaGral(recipiente, depende);
    //    }

    //});

    $('#divConsultaGeneral').on('hidden.bs.modal', function () {
        document.getElementById('divTitulo').innerHTML = '';
        document.getElementById('divConsulta').innerHTML = '';
    });


    $("input,select,textarea").focus(function () {

        if (this.id.toUpperCase().substring(0, 3) == 'MON') {
            this.value = formatNumber(this.value);
            $(this).css('text-align', 'left');

        }
    }).keyup(function (e) {
        if (e.which == '13') {
            buscarRegistro(this);
            proximoCampo(this.id);
        }

    }).change(function () {
        buscarRegistro(this);
    });



});
function llenaComboB() {

    $(document).find("select").each(function () {
        if ($(this)[0].id.substring(0, 5) == 'ddlit') {            
            if ($(this)[0].id.indexOf("_NO") == -1) {
                llenaCombo($(this)[0].id, '');
            }
        }
    });
}

function llenaTagSelect() {

    $(document).find("select").each(function () {
        if ($(this)[0].id.substring(0, 5) == 'ddlid') {
            if ($(this)[0].id.indexOf("_NO") == -1) {
                llenaComboConsulta($(this)[0].id);
            }
        }
    });
}

function Estado() {

    if (modifica == true) {
        $('.status').empty().html('<span class="txt-bold">Modo:<span/> <span class="text-danger">Modifica<span/>');
    }
    else {
        $('.status').empty().html('<span class="txt-bold">Modo:<span/> <span class="text-success">Adicciona<span/>');
    }
}

function nombreCampo(campo) {
    campo = campo.substring(3);
    if (campo.indexOf("_") > 0) {
        campo = campo.substring(0, campo.indexOf("_"))
    }
    return campo;

}

function buscarRegistro(campo) {
    if (typeof tabla == 'undefined') {
        return false;
    };

    if (campo.id.indexOf('_W') > 0) {
        buscarConsulta(nombreCampo(campo.id), campo.value);
    }

    else if (campo.id.indexOf('_K') > 0) {
        modifica = true;
        $('#txtmodo').html('Modo: Adiciona');
        $('#txtmodo').remove();
        if (campo.value.length == 0) {
            return false;
        }
        waitingDialog.show();
        var param = "'llave':'" + nombreCampo(campo.id) + "=" + campo.value + "'";
        $.ajax({
            type: "POST",
            async: false,
            url: "uti.aspx/buscarRegistro",
            data: "{" + param + ",'tabla':'" + tabla + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var m = response.d;
                if (m != '') {
                    modifica = true;
                    eval(m);
                    waitingDialog.hide();
                }
                Estado(); 

                return false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                waitingDialog.hide();
                alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown, "Error: Llenando Combo");
            }
        });
    }
}

function llenaCombo(control, where) {

    var param = "'combo':'" + control + "','where':'" + where + "'";

    $.ajax({
        type: "POST",
        async: false,
        url: "uti.aspx/LlenaCombo",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            $('#' + control).find('option').remove().end();
            $('#' + control).append($('<option></option>').val('0').html('** Seleccione**'));
            $.each(m, function (val, text) {
                $('#' + control).append($('<option></option>').val(text.Value).html(text.Text));
            });
            $('#' + control).get(0).selectedIndex = 0;
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown, "Error: Llenando Combo");
        }
    });
}

function llenaComboConsulta(control) {

    var param = "'combo':'" + control + "'";

    $.ajax({
        type: "POST",
        async: false,
        url: "uti.aspx/llenaComboConsulta",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            $('#' + control).find('option').remove().end();
            $('#' + control).append($('<option></option>').val('0').html('** Seleccione**'));
            $.each(m, function (val, text) {
                $('#' + control).append($('<option></option>').val(text.Value).html(text.Text));
            });
            $('#' + control).get(0).selectedIndex = 0;
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown, "Error: Llenando Combo");
        }
    });
}

var waitingDialog = (function ($) {

    // Creating modal dialog's DOM
    var $dialog = $(
		'<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
		'<div class="modal-dialog modal-m">' +
		'<div class="modal-content">' +
			'<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
			'<div class="modal-body">' +
				'<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
			'</div>' +
		'</div></div></div>');

    return {
   
        show: function (message, options) {
            // Assigning defaults
            var settings = $.extend({
                dialogSize: 'm',
                progressType: ''
            }, options);
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            if (typeof options === 'undefined') {
                options = {};
            }
            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            // Opening dialog
            $dialog.modal();
        },
        /**
        * Closes dialog
        */
        hide: function () {
            $dialog.modal('hide');
        }
    }

})(jQuery);

function consultaGral(recipient, depende) {
    waitingDialog.show();
    param = "'consulta':'" + recipient + "','depende':'" + depende + "'";

    $.ajax({
        type: "POST",
        async: false,
        url: "uti.aspx/consultaGral",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d.split('|');
            var modal = $(this)
            waitingDialog.hide();
            document.getElementById('divTitulo').innerHTML = m[0];
            document.getElementById('divConsulta').innerHTML = m[1];
            $('#tbconsultaGeneral').dataTable();
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown, "Error: modalA");
        }
    });

}

function buscarLlave() {
    campoLlave = '';
    $(div).find("input:text").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf("_k") > 0) {
            campoLlave = $(this)[0].id;
            return false;
        }
    });

    return campoLlave;
}

function buscarLlaveCampo() {
    campoLlave = '';
    $(div).find("input:text").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf("_k") > 0) {
            campoLlave = this.value;
            return false;
        }
    });

    return campoLlave;
}

function ponerValorConsulta(respuesta) {

    $('#divConsultaGeneral1').modal('hide');
    $('#divConsultaGeneral').modal('hide');
    $(div).find(':input').each(function () {
        // if (subConsulta == '') {
        if ($(this)[0].id.indexOf('_K') > 0 || $(this)[0].id.indexOf('_CK') > 0) {
                    campo = '#' + $(this)[0].id;
                    if ($(campo).is(':visible')) {
                        $(this)[0].value = respuesta;
                        campo = '#' + $(this)[0].id;
                        $(campo).focus().trigger({ type: 'keyup', which: 13 }).blur();
                        modifica = true;
                        return false;
                    }
                }
        // }
        //        else {

        //            if ($(this)[0].id.toUpperCase().indexOf(subConsulta.toUpperCase()) == 3) {
        //                $(this)[0].value = respuesta;
        //                buscarConsulta(nombreCampo($(this)[0].id), respuesta);
        //                return false;
        //            }
        //        }
    });
    //    subConsulta = '';
    //    return false;

}

function buscarConsulta(campo, valor) {
    $.ajax({
        type: "POST",
        url: "uti.aspx/buscarConsulta",
        data: "{'campo':'" + campo + "','valor':'" + valor + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            eval(m);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown, "Error: buscarConsulta");
        }
    });

}

function limpiaGeneral(div) {

    $(div).find(":input").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf('_no') == -1) {
            this.value = '';
        }
    });

    $(div).find("select").each(function () {
        if (this.id != '') {
            if ($(this)[0].id.toLowerCase().indexOf('_no') == -1) {
                this.value = 0;
            }
        }
    });

    $(div).find("textarea").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf('_no') == -1) {
            this.value = '';
        }
    });


    $(div).find("input:password").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf('_no') == -1) {
            this.value = '';
        }
    });


    $(div).find('input:checkbox').each(function () {
        if ($(this)[0].id.toLowerCase().indexOf('_no') == -1) {
            if (this.id != '') {
                this.checked = false;
            }
        }
    });
}

function proximoCampo(obj) {
    var cont = 0;
    $("input:not([readonly]), select,textarea").each(function () {

        if (obj.id == this.id) {
            cont = 1;
        }
        else {
            if (cont == 1) {
                this.focus();
                return false;
            }
        }
    });
}

function ponerValor(nombre, valor) {
    var paso = 0
    var guion = 0;

    $(div).find(":input").each(function () {
        if ($(this)[0].id.toUpperCase().indexOf(nombre.toUpperCase()) == 3) {
            guion = 3 + nombre.length;
            if ($(this)[0].id.substr(guion, 1) == "_") {
                paso = 1;
                modifica = true;
                if ($(this)[0].id.toUpperCase().substring(0, 3) == 'FEC') {
                    valor = fechaDma(valor);
                }
                if ($(this)[0].id.toUpperCase().substring(0, 3) == 'MON') {
                    valor = formatCurrency(valor);
                }
                $(this)[0].value = valor;
                if ($(this)[0].id.indexOf("_W") > 0) {
                    buscarConsulta(nombreCampo($(this)[0].id), valor);
                }

                return false;
            }
        }
    });

    if (paso == 0) {
        $(div).find("input:checkbox").each(function () {
            if ($(this)[0].id.toUpperCase().indexOf(nombre.toUpperCase()) == 3) {
                guion = 3 + nombre.length;
                if ($(this)[0].id.substr(guion, 1) == "_") {
                    paso = 1;
                    if (valor == 'False') {
                        $(this)[0].checked = false;
                    }
                    else {
                        $(this)[0].checked = true;
                    }

                    return false;
                }
            }
        });
    }

    if (paso == 0) {
        $(div).find("input:radio").each(function () {
            if ($(this)[0].name.toUpperCase().indexOf(nombre.toUpperCase()) == 3) {
                guion = 3 + nombre.length;
                if ($(this)[0].name.substr(guion, 1) == "_") {
                    if ($(this)[0].value == valor) {
                        $(this)[0].checked = true;
                        return false;
                    }
                }
            }
        });
    }

    if (paso == 0) {

        $(div).find("select").each(function () {
            if ($(this)[0].id.toUpperCase().indexOf(nombre.toUpperCase()) == 3) {
                guion = 3 + nombre.length;
                if ($(this)[0].id.substr(guion, 1) == "_") {
                    paso = 1;
                    $(this)[0].value = valor;
                    if ($(this)[0].id == 'cobitMarca') {
                        buscaModelo(valor);
                    }
                    return false;
                }
            }
        });
    }

    if (paso == 0) {
        $(div).find("textarea").each(function () {
            if ($(this)[0].id.toUpperCase().indexOf(nombre.toUpperCase()) == 3) {
                guion = 3 + nombre.length;
                if ($(this)[0].id.substr(guion, 1) == "_") {
                    paso = 1;
                    $(this)[0].value = valor;
                    return false;
                }
            }
        });
    }

    if (paso == 0) {
        $(div).find("input:radio").each(function () {
            if ($(this)[0].id.toUpperCase().indexOf(nombre.toUpperCase()) == 3) {
                guion = 3 + nombre.length;
                if ($(this)[0].id.substr(guion, 1) == "_") {
                    ponerValorRadio($(this)[0].id, valor);
                    return false;
                }
            }
        });
    }

    if (paso == 0) {
        $(div).find("input:password").each(function () {
            if ($(this)[0].id.toUpperCase().indexOf(nombre.toUpperCase()) == 3) {
                guion = 3 + nombre.length;
                if ($(this)[0].id.substr(guion, 1) == "_") {
                    paso = 1;
                    $(this)[0].value = valor;
                    return false;
                }
            }
        });
    }
}

function intVal(v) {
    var valor = 0;
    if (String(v).indexOf(".") > -1) {
        valor = parseFloat(v);
    }
    else {
        valor = parseInt(v);
    }

    if (isNaN(valor))
        return 0;
    else
        return valor;
}

function grabaGeneral(tipo) {
    var sql = '';
    var nombre = '';
    //    div = ("#tabs").children()[$("#tabs").tabs('option', 'selected') + 1].id;
    $(div).find("input:text").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf(tipo) > 0) {
            sql += $(this)[0].id + '=' + $(this)[0].value + '|';
        }
    });

    $(div).find("select").each(function () {
        if (this.id != '') {
            if ($(this)[0].id.toLowerCase().indexOf(tipo) > 0) {
                sql += this.id + '=' + this.value + '|';
            }
        }
    });

    $(div).find("textarea").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf(tipo) > 0) {
            sql += this.id + '=' + this.value + '|';
        }
    });


    $(div).find("input:password").each(function () {
        if ($(this)[0].id.toLowerCase().indexOf(tipo) > 0) {
            sql += this.id + '=' + this.value + '|';
        }
    });

    $(div).find("input:radio").each(function () {
        if ($(this)[0].name.toLowerCase().indexOf(tipo) > 0) {
            if ($(this)[0].name != nombre) {
                if (sql.indexOf($(this)[0].name) == -1) {
                    sql += $(this)[0].name + '=' + getRadioValor($(this)[0].name) + '|';
                }
            }
            nombre = $(this)[0].name;
        }
    });

    $(div).find('input:checkbox').each(function () {
        if ($(this)[0].id.toLowerCase().indexOf(tipo) > 0) {
            if (this.id != '') {
                if (this.checked == true)
                    sql += this.id + '=' + this.value + '|';
                else
                    sql += this.id + '=' + 'NULL' + '|';
            }
        }
    });

    return sql;
}

function getRadioValor(radioName) {
    var rads = document.getElementsByName(radioName);
    var retorna = false;
    var i = -1;

    for (var rad in rads) {
        i = i + 1;
        if (typeof rads[i] != 'undefined') {
            if (rads[i].checked) {
                return rads[i].value;
            }
        }
    }
    return '';
}

function validar() {
    var retorna = true;

    $(div).find(':input').each(function () {
        if ($(this)[0].id.toLowerCase().indexOf('_r') > 0) {
            switch ($(this)[0].id.substring(0, 3).toLowerCase()) {
                case 'txt':
                    if ($(this)[0].value == '') {
                        retorna = false;
                        alert('El campo ' + nombreCampo($(this)[0].id) + ' esta incompleto');
                        $(this)[0].focus();
                        return false;
                    }
                    break;
                case 'msk':
                    if (intVal($(this)[0].value) == "0") {
                        retorna = false;
                        alert('El campo ' + nombreCampo($(this)[0].id) + ' esta incompleto');
                        $(this)[0].focus();
                        return false;
                    }
                    break;
                case 'mon':
                    var valor = $(this)[0].value;
                    if (intVal(formatNumber(valor)) == "0") {
                        retorna = false;
                        alert('El campo ' + nombreCampo($(this)[0].id) + ' esta incompleto');
                        $(this)[0].focus();
                        return false;
                    }
                    break;
                case 'fec':
                    if (!(fechaValida(fechaAmd($(this)[0].value)))) {
                        retorna = false;
                        alert('El campo ' + nombreCampo($(this)[0].id) + ' esta incompleto');
                        $(this)[0].focus();
                        return false;
                    }
                    break;
                case 'hor':
                    if ($(this)[0].value == '') {
                        retorna = false;
                        alert('El campo ' + nombreCampo($(this)[0].id) + ' esta incompleto');
                        $(this)[0].focus();
                        return false;
                    }
                    break;
                case 'ddl':
                    if (intVal($(this)[0].value) == "0") {
                        retorna = false;
                        alert('El campo ' + nombreCampo($(this)[0].id) + ' esta incompleto');
                        $(this)[0].focus();
                        return false;
                    }
                    break;
            }
        }

    });



    if (retorna == true) {
        var nombre = '';
        $(div).find("input:radio").each(function () {
            if ($(this)[0].name != nombre) {
                if (getRadioChequeado($(this)[0].name) == false) {
                    if ($(this)[0].name.indexOf('_r') > 0) {
                        var numero = $(this)[0].name.split('_');
                        buscaNombreRadio(numero[1]);
                    }
                    else {
                        alert('el campo ' + nombreCampo($(this)[0].name) + ', no esta selecionado');
                    }
                    $(this)[0].focus();
                    retorna = false;
                    return false;
                }
            }
            nombre = $(this)[0].name;
        });
    }

    return retorna;
}

function grabarRegistro(ind, secuencia, llave) {
    retorna = 0;
    var modo = 0
    if (modifica) {
        modo = 2;
    }
    else {
        modo = 1;
    }
    var myData = grabaGeneral(ind);

    var param = "'modo':'" + modo + "','llave':'" + llave + "','campo':'" + myData + "','tabla':'" + tabla + "','secuencia':" + secuencia;

    $.ajax({
        type: "POST",
        async: false,
        url: "uti.aspx/GrabaDatosGeneral",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            retorna = m;
        },
        error: function (result) {
            alert(result.responseText);
        }
    });
    return retorna;
}
function consultaGralParametro(nombre, where) {

    param = "'consulta':'" + nombre + "','where':'" + where + "'";

    $.ajax({
        type: "POST",
        async: false,
        url: "uti.aspx/consultaGralParametro",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d.split(',');
            document.getElementById('divTitulo1').innerHTML = m[0];
            document.getElementById('divConsulta1').innerHTML = m[1];
            $('#tbconsultaGeneral').dataTable();
            $("#divConsultaGeneral1").modal('show');
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown, "Error: Llenando Combo");
        }
    });
}