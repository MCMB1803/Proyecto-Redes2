

var datos = { id: $('.textC').attr("id") };
$.ajax({
    url: '/Game/GetComment',
    type: 'POST',
    data: datos,
    success: function (result) {
        $("#get").html(result);
    },
    error: function () {
        // Manejar errores en la solicitud Ajax
        alert('Error al obtener la vista parcial');
    }
});