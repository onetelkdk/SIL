$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

$(document).ready(function(){
    $(".hidemenu").click(function(){
        if ($('.menuleft').is(':visible')) {
        	$('.menuleft').css("display","none");
  			$('.main_data').animate({marginLeft: "+=-300px"});
        $('.Mfooter').css("marginLeft","-290px");
		}else{
			$('.main_data').animate({marginLeft: "+=300px"}); 
  			$('.menuleft').animate({marginLeft: "+=-100px"});
        $('.menuleft').animate({marginLeft: "+=100px"});
        $('.menuleft').css("display","block");
         $('.Mfooter').css("marginLeft","0")
		};
    });

    function menuActivo(){

        $('#accordion').find('li').each(function(){
            var url = window.location.pathname;
            var pagNom = url.substr(1);
            $('ul.submenu li a').each(function(){
             if ($(this).attr('href') == pagNom){
                $(this).addClass("active-menu").parent().parent('ul.submenu')
                .css("display","block")
                .parent('.accordion li').addClass("open");
           }
          });
        })
    }
menuActivo();

    function changeBloqueMenu(){
      $(".bloque_body").each(function () 
        { 

          var $div = $(this).children('div').length;

            if ($div > 11 && $div < 21) {
                $(this).css("min-width","550px")
                $(this).children('.menu').css({
                  float:'left',
                  marginRight:'10px'
                });
                $('.main_container').css({
                  width: '1300px',
                  overflow:'hidden'
                })

            }
            if ($div >= 21) {
                $(this).css("min-width","840px")
                $(this).children('.menu').css({
                  float:'left',
                  marginRight:'10px'
                });
                $('.main_container').css({
                  width: '1500px',
                  overflow:'hidden'
                })
            }
            console.log('Options Count are ' + $div)
        }); 
    }
    changeBloqueMenu();

    function chageModwidth(){
      var dv1 = $('#dvMenu1').children('div').length;
      var dv2 = $('.modulo_item').children('div').length;
        if(dv1, dv2 == 0){
          $('#main_modulo').css("width","auto")
        }
        if(dv1 > 0 ){
          $('#main_modulo').css("width","1360px");
        }
        if(dv2 > 0 ){
            $('#main_modulo').css("width","1720px")
          }
    }
    chageModwidth()


});
          $(document).ready(function(){

            var url = window.location.pathname;
            var pagNom = url.substr(1);

            if (pagNom == 'Default.aspx'){

                $('.breadcrumb').remove();
                $('.header_container').css("border-bottom", "1px solid #676A6C")
           }
           if (pagNom == 'accesodenegado.aspx'){

                $('.breadcrumb').css("border", "0");
           }

          })
            


/* TAGS */

/*!
 * jQuery Tags plugin v1.1
 * http://github.com/aquilez/jquery-tags
 *
 * Copyright 2011, Santiago Dimattia
 * Released under the MIT license.
 * http://www.opensource.org/licenses/mit-license.php
 *
 */
$(function($) {

  $.fn.tags = function(options)
  {
    var defaults = {
      separator:   ';',
      maxTagWords: 0,
      tagAdded:    function() { },
      tagRemoved:  function() { },
    };
    var settings = $.extend(defaults, options);

    $(this).each(function(){
      var e = $(this);
      var instance = {

        textfield: e,
        taglist: { },

        init: function(){
          var that = this;
          var e = this.textfield;

          if(!e.is('input[type=text]'))
          {
            return;
          }

          // Wrap the input field and create the tag list
          e.wrap('<div class="tag-manager" />');
          e.before('<ul class="tag-list"></ul>');

          // Replace the text field with a hidden one
          e.before('<input type="hidden" name="' + e.attr('name') + '" />');
          e.removeAttr('name');

          that.add_tag();

          // Bind the container so it focus the text field when you click on it
          e.parent().bind('click', function(){
            e.focus();
          });

          // Bind the input field
          e.bind('blur', function(){
            that.add_tag();
          }).keydown(function(event){
            if(event.keyCode == 13)
            {
              event.preventDefault();
              that.add_tag();
            }
          });
        },

        // Update the hidden field
        updateHiddenField: function(){
          var string = '';
          for(i in this.taglist)
          {
            string += settings.separator + this.taglist[i];
          }
          this.textfield.parent().children('input[type=hidden]').val(string.substring(1));
        },

        // Add one or more tags
        add_tag: function() {
          var that = this;
          var e = this.textfield;

          if(e.val() == '')
          {
            return;
          }

          // Separe the tags by comma
          tags = e.val().split(settings.separator);

          // For each tag
          for(i in tags)
          {
            // Trim
            tag = tags[i].replace(/^\s+|\s+$/g, '');

            // Apply maxTagWords
            var words = tag.split(" ");
            if(settings.maxTagWords != 0 && words.length > settings.maxTagWords)
            {
              for(var m = 0; m < words.length - settings.maxTagWords; m++)
              {
                tag = tag.substring(0, tag.lastIndexOf(" "));
              }
            }

            // Add the tag only if it isn't on the list already
            if(that.taglist[tag.toLowerCase()] === undefined)
            {
              // Add the tag on the list
              e.parent().children('ul.tag-list').append('<li data-name="' + tag + '">' + tag + ' <a class="tag-remove-link">X</a></li>');

              // Add the tag on the array
              that.taglist[tag.toLowerCase()] = tag;

              // Delete the tag when the link is clicked!
              e.parent().find('li[data-name="' + tag + '"] a').unbind().click(function(){
                tagname = $(this).parent().data('name').toString().toLowerCase();

                // Delete the key
                delete that.taglist[tagname];

                // Update the hidden input
                that.updateHiddenField();

                // Remove the li
                $(this).parent().remove();

                // Callback
                settings.tagRemoved(tag, e);
              });
            }

            // Callback
            settings.tagAdded(tag, e);
          }

          // Update the hidden input
          that.updateHiddenField();

          e.val('');
        }
      };

      instance.init();
    });
  }

});

(jQuery);
function cambiarTipoDocumentos() {
    var tipoDocVal = parseInt(document.getElementById("opt").value);
    if (tipoDocVal == 1) {
        $("#dvIniciativas").fadeIn(1000);
        $("#dvSesiones").css("display", "none");
        $("#dvComisiones").css("display", "none");
    }
    if (tipoDocVal == 2) {
        $("#dvSesiones").fadeIn( 1000 );
        $("#dvIniciativas").css("display", "none");
        $("#dvComisiones").css("display", "none");
        
    }
    if (tipoDocVal == 3) {
        $("#dvComisiones").fadeIn(1000);
        $("#dvIniciativas").css("display", "none");
        $("#dvSesiones").css("display", "none");
    }

}
function traerFormAdjuntar() {
    $("#formularioAdjuntar").fadeIn(1000);
    $("#gridresultados").css("display", "none");
    $("#ic_adjuntardoc").css("display", "none");
    $("#opt").css("display", "none");
    
}
function cancelarAdjuntado() {
    $("#cuerpoAdjuntar").fadeIn(1000);
    $("#formularioAdjuntar").css("display", "none");
}
function verificaMarcado(campo) {

    for (var i = 0; i < dataPrincipal.fnGetNodes().length; i++) {
        if (campo.id != dataPrincipal.fnGetNodes()[i].cells[0].childNodes[1].id) {
            dataPrincipal.fnGetNodes()[i].cells[0].childNodes[1].checked = false;
        }
    }
}

/* END TAGS*/

$(function () {

    var url = window.location.pathname;
    var pagNom = url.substr(1);

    if (pagNom == 'Menu.aspx') {

        $("body").mousewheel(function (event, delta) {

            this.scrollLeft -= (delta * 30);

            event.preventDefault();
        });

    }

        $('.mask-tel').mask('999-999-9999');
        $('.mask-ced').mask('999-9999999-9');

        $('.time').timepicker();
});

// VALIDACIONES 

var soloTexto = '#ContentBody_txtAdmlegNombres, #ContentBody_txtAdmlegApellido2, #ContentBody_txtAdmlegApellido1';
var soloNumeros = '#ContentBody_txtAdmLegCedula';
var requerido = '#ContentBody_txtDescripcion, #ContentBody_txtDescripcion, #ContentBody_txtDescripcionIniciativa, #ContentBody_txtNombre';
var requeridoCbo = '#ContentBody_DropDownAdmLegTipoId, #ContentBody_DropDownAdmLegSexo, #ContentBody_DropDownAdmPdoCodigo';

/// REQUERIDO

$(function(){
  $(requeridoCbo).click(function(){
    var elemento = $(requeridoCbo).val();
    
    if(elemento==0){
      $(this).addClass('requerido');
    }else{
      $(this).removeClass('requerido');
    }
  })
})

$(requerido).focus(function(){

    var elemento = $(this).val()
    if(elemento==''){

      $(this).addClass('requerido');
    }
});

$(requerido).keydown(function(){

  var elemento = $(this).val();

   $(this).removeClass('requerido');

      if(elemento==''){
        $(this).addClass('requerido');
      }
})
$(requerido).blur(function(){
  var elemento = $(this).val();

    if(elemento==''){
      $(this).addClass('requerido');
    }
})
/// END REQUERIDO

// Solo puede escribir Numeros
$(soloNumeros).focus(function(){

    var elemento = $(this).val()
    if(elemento==''){

      $(this).addClass('requerido');
    }
});

$(soloNumeros).keydown(function(){

  var elemento = $(this).val();

   $(this).removeClass('requerido');

      if(elemento==''){
        $(this).addClass('requerido');
      }

    // Solo puede escribir numeros
      $(this).bind('keypress', function (event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
           event.preventDefault();
           return false;
        }
    });
})

// Solo puede escribir Texto
$(soloTexto).focus(function(){

    var elemento = $(this).val()
    if(elemento==''){

      $(this).addClass('requerido');
    }
});

$(soloTexto).keydown(function(){

  var elemento = $(this).val();

   $(this).removeClass('requerido');

      if(elemento==''){
        $(this).addClass('requerido');
      }

    // Solo puede escribir texto
    $(this).bind('keypress', function (event) {
      var regex = new RegExp("^[a-zA-Z]+$");
      var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
      if (!regex.test(key)) {
         event.preventDefault();
         return false;
      }
    });
})
$(soloTexto).blur(function(){
  var elemento = $(this).val();

    if(elemento==''){
      $(this).addClass('requerido');
    }
})


$(document).ready(function(){  
  /*
    $('#Asistio').click(function(){
      var marcado = $(this).prop("checked");
        if(marcado==true){
          $('#noAsistio').prop("checked", "");
        }else{
          $(this).prop("checked");
        }
    })

    $('#noAsistio').click(function(){
      var marcado = $(this).prop("checked");
        if(marcado==true){
          $('#Asistio').prop("checked", "");
        }else{
          $(this).prop("checked");
        }
    })
  */

  $('#Asistencia').find('input:checkbox').each(function(){
      $(this).click(function(){

          $('#Asistencia input[type=checkbox]').prop('checked', '');
          $('#Asistencia input[type=checkbox]').attr('checked', false); 

          $(this).prop("checked", "checked");
          $(this).attr('checked', true); 
      })
  })
});  

$(document).ready(function(){

  $('#ContentBody_btnAgregarMiembro').click(function(){

    $('#tabContenedor').find('input,select,textarea').each(function(){

      var enFoco = $(':focus');

      enFoco.parent('.tab-pane').addClass('active')

      console.log(elemento)
    });

  })




  
})

// END VALIDACIONES 