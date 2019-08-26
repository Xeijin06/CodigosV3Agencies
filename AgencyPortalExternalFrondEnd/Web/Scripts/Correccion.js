var APP = APP || {};

APP.correccion = {

    changeTextFile: function () {
        $('input[type="file"]').change(function () {
            var totalBytes = this.files[0].size;

            if (totalBytes < 2097152) {
                var file = $('input[type="file"]')[0].files[0].name;
                $('.filehiddenroute').html(file);
                $('.filehiddenroute').removeClass("hidden");
                $('.filehiddenroute').removeClass("error-label");
            } else {
                $('.filehiddenroute').html("La imagen debe ser menor a 2MB");
                $('.filehiddenroute').removeClass("hidden");
                $('.filehiddenroute').addClass("error-label");
                $('input[type="file"]')[0].val('');
            }
        });
    },


    validateInputFile: function (model) {
        var formData = new FormData();
        formData.append('model', JSON.stringify(model));

        if ($('input[type=file]')[0].files.length > 0) {

            formData.append('image', $('input[type=file]')[0].files[0]);
            return formData;
        }else {
            $('.filehiddenroute').html("Debe adjuntar una foto del pasaporte.");
            $('.filehiddenroute').removeClass("hidden");
            $('.filehiddenroute').addClass("error-label");

            return null;
        }
    },

    registerCorrection: function () {
        $("#btnregister").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            var form = "#form-correction";
            if (!$(form).valid())
                return;

            var model = APP.common.SerializeInputs(form);
            var formData = APP.correccion.validateInputFile(model);
            if (formData === null)
                return;

            APP.correccion.callApiRequest(formData);

        });
    },

    callApiRequest: function (formData) {
        APP.common.mostrarBackloading();

        $.ajax({
            //url: 'https://hdq-it-01/agencyportalexternalfrontend/Correction/SendRequest',
            url: APP.common.webServer + 'Correction/SendRequest',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function (xhr) {
                //$(".loading").show();
            }
        })
            .done(function (data) {

                alert(data.Message);
                if (data.Result) {
                    $(':input').val('');

                    $('.filehiddenroute').removeClass("error-label");
                    $('.filehiddenroute').addClass('hidden');
                    $('.filehiddenroute').html('');
                }

                APP.common.ocultarBackLoading();

            }).fail(function (data) {
                console.log(data);
                APP.common.ocultarBackLoading();
            });
    }


};

$(document).ready(function () {
    APP.correccion.changeTextFile();
    APP.correccion.registerCorrection();
});
