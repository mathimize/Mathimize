        //Update that validator
        $.validator.setDefaults({
            highlight: function (element) {
                $(element).closest(".control-group").addClass("error");
                //$("#Rows").tooltip({ trigger: "focus", placement: 'right' }).attr("data-original-title", "blah");
                //$("#Rows").tooltip('show');
                
            },
            unhighlight: function (element) {
                $(element).closest(".control-group").removeClass("error");
                
            },
            showErrors: function (errorMap, errorList) {
                this.defaultShowErrors();

                // destroy tooltips on valid elements                              
                $("." + this.settings.validClass).tooltip("destroy");

                // add/update tooltips 
                for (var i = 0; i < errorList.length; i++) {
                    var error = errorList[i];
                    $("#" + error.element.id).tooltip({ trigger: "focus", placement: 'right' }).attr("data-original-title", error.message);
                }
            }
        });

        $(document).ready(function () {

            var resultId = $("#ResultId").val();
            if (resultId != '') {

                $("#pdf").removeClass("disabled");
            }
            else {
                $("#pdf").addClass("disabled");
            }



            $('#pdf').click(function () {
                //window.open("/Math/BasicArithmeticResult?resultId=" + resultId, "Results");
                window.open("ExportResultToPDF2?resultId=" + resultId, "Results");
            });


            //--------------------------------------------------------------------------
            // Converts MVC Validation classes to Bootstratp validation classes
            //--------------------------------------------------------------------------

            //            $('span.field-validation-valid, span.field-validation-error').each(function () {
            //                $(this).addClass('help-inline');
            //            });

            $('form').submit(function () {
                if ($(this).valid()) {
                    $(this).find('div.control-group').each(function () {
                        if ($(this).find('span.field-validation-error').length == 0) {
                            $(this).removeClass('error');
                        }
                    });
                }
                else {
                    $(this).find('div.control-group').each(function () {
                        if ($(this).find('span.field-validation-error').length > 0) {
                            $(this).addClass('error');
                        }
                    });
                }
            });

            $('form').each(function () {
                $(this).find('div.control-group').each(function () {
                    if ($(this).find('span.field-validation-error').length > 0) {
                        $(this).addClass('error');
                    }
                });
            });









            //--------------------------------------------------------------------------
        });
