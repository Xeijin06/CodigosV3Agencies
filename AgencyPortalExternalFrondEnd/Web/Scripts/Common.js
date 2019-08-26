var APP = APP || {};

APP.common = {
    imgLoading: '<i class="fas fa-spinner fa-pulse" style="font-size:100px; color: #0e3152;"></i>',

    mostrarBackloading: function () {
        $('#loadingContent').removeClass('hidden');
        $('#loadingContent').html(APP.common.imgLoading);
    },

    ocultarBackLoading: function () {
        $('#loadingContent').addClass('hidden');
        $('#loadingContent').html('');
    },

    SerializeInputs: function (form) {
        var o = {};
        var a = $(form).serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });

        return o;
    },


    apiServer: $("#api-server").val(),
    webServer: $("#web-server").val()
};

$(document).ready(function () {

});
