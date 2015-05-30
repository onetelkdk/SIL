var x;
	x=$(document);
		x.ready(function(){
			var x=$(".link_emails");
				x.click(mostrar_emails);
			});

function mostrar_emails(){
	var x=$("#emails");
		x.toggle("fast");
}

x=$(document);
		x.ready(function(){
			var x=$(".link_notificaciones");
				x.click(mostrar_notificaciones);
			});

function mostrar_notificaciones(){
	var x=$("#notificaciones");
		x.toggle("fast");
}