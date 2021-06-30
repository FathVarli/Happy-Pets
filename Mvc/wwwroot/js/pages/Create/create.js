"use strict";

var KTCreate = function () {

    var _handleCreateForm = function (e) {
        var validation;
        var form = KTUtil.getById('catForm');

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            form,
            {
                fields: {
                    CName: {
                        validators: {
                            notEmpty: {
                                message: 'Ad alanı zorunludur.'
                            }
                        }
                    },
                    Weight: {
                        validators: {
                            notEmpty: {
                                message: 'Kilo alanı zorunludur.'
                            },
                            integer: {
                                message: 'Virgül ve  boşluk kullanmadan gram şeklinde giriniz.'
                            }
                        }
                    },
                    BirthDay: {
                        validators: {
                            notEmpty: {
                                message: 'Doğum günü alanı zorunludur.'
                            }
                        }
                    },
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );

        $('#cat_submit').on('click', function (e) {

            e.preventDefault();
            debugger;
            var _cName = $("input[name=CName]").val();
            var _weight = $("input[name=Weight]").val();
            var _birthday = $("input[name=BirthDay]").val();
            var _isParazit = $("input[name=isParazit]").val();
            var _isKuduz = $("input[name=isKuduz]").val();
            var _isKarmaI = false
            var _isKarmaII = false
            var _isLosemiI = false
            var _isLosemiII = false

            var _createPetModel = {
                saveCatDto: {
                    Name: _cName,
                    Weight: _weight,
                    Birthday: _birthday,
                    isParazit: _isParazit,
                    isKuduz: _isKuduz,
                    isKarmaI: _isKarmaI,
                    isKarmaII: _isKarmaII,
                    isLosemiI: _isLosemiI,
                    isLosemiII: _isLosemiII
                }
            }

            validation.validate().then(function (status) {
                if (status == 'Valid') {
                    $.ajax({
                        type: "POST",
                        url: "/Pet/Create",
                        dataType: 'json',
                        data: _createPetModel,
                        success: function (response) {
                            if (response.success) {
                                swal.fire({
                                    text: response.message,
                                    icon: "success",
                                    buttonsStyling: false,
                                    confirmButtonText: "Tamam",
                                    customClass: {
                                        confirmButton: "btn font-weight-bold btn-light-primary"
                                    }
                                }).then(function () {
                                    $("input[name=CName]").val('');
                                    $("input[name=Weight]").val('');
                                    $("input[name=BirthDay]").val('');
                                    $("input[name=isParazit]").val('');
                                    $("input[name=isKuduz]").val(false);
                                    $("input[name=isKarmaI]").val(false);
                                    $("input[name=isKarmaII]").val(false);
                                    $("input[name=isLosemiI]").val(false);
                                    $("input[name=isLosemiII]").val(false);
                                    location.href = "/Pet/Index";
                                });
                            } else {
                                swal.fire({
                                    text: response.message,
                                    icon: "error",
                                    buttonsStyling: false,
                                    confirmButtonText: "Tamam",
                                    customClass: {
                                        confirmButton: "btn font-weight-bold btn-light-primary"
                                    }
                                }).then(function () {
                                    KTUtil.scrollTop();
                                });
                            }
                        },
                        error: function () {
                            swal.fire({
                                text: "Üzgünüz, kayıt olurken bir sorunla karşılaştık.",
                                icon: "error",
                                buttonsStyling: false,
                                confirmButtonText: "Tamam",
                                customClass: {
                                    confirmButton: "btn font-weight-bold btn-light-primary"
                                }
                            }).then(function () {
                                KTUtil.scrollTop();
                            });
                        },
                    });

                } else {
                    swal.fire({
                        text: "Üzgünüz, kayıt olurken bir sorunla karşılaştık.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Tamam",
                        customClass: {
                            confirmButton: "btn font-weight-bold btn-light-primary"
                        }
                    }).then(function () {
                        KTUtil.scrollTop();
                    });
                }
            });
        });

    }

    return {
        // public functions
        init: function () {

            _handleCreateForm();
        }
    };

}();

jQuery(document).ready(function () {
    KTCreate.init();
});