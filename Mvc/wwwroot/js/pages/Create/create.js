"use strict";

var KTCreate = function () {

    var _handleCreateCatForm = function (e) {
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

            var _cName = $("input[name=CName]").val();
            var _weight = $("input[name=CWeight]").val();
            var _birthday = $("input[name=CBirthDay]").val();
            var _isParazit = $("input[name=isParazit]:checked").val();
            var _isKuduz = $("input[name=isKuduz]:checked").val();
            var _isKarmaI = $("input[name=isKarmaI]:checked").val();
            var _isKarmaII = $("input[name=isKarmaII]:checked").val();
            var _isLosemiI = $("input[name=isLosemiI]:checked").val();
            var _isLosemiII = $("input[name=isLosemiII]:checked").val();
            debugger;
            var _createPetModel = {

                typeId: 1,
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
                                    $("input[name=isParazit]").val(false);
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

    var _handleCreateDogForm = function (e) {
        var validation;
        var form = KTUtil.getById('dogForm');

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


        $('#dog_submit').on('click', function (e) {

            e.preventDefault();

            var _cName = $("input[name=DName]").val();
            var _weight = $("input[name=DWeight]").val();
            var _birthday = $("input[name=DBirthDay]").val();
            var _isParazit = $("input[name=isParazit]:checked").val();
            var _isKuduz = $("input[name=isKuduz]:checked").val();
            var _isKarmaI = $("input[name=isKarmaI]:checked").val();
            var _isKarmaII = $("input[name=isKarmaII]:checked").val();
            var _isLyme = $("input[name=isLyme]:checked").val();
            var _isBronshineI = $("input[name=isBronshineI]:checked").val();
            var _isBronshineII = $("input[name=isBronshineII]:checked").val();
            var _isCoronaI = $("input[name=isCoronaI]:checked").val();
            var _isCoronaII = $("input[name=isCoronaII]:checked").val();
            debugger;
            var _createPetModel = {

                typeId: 2,
                saveDogDto: {
                    Name: _cName,
                    Weight: _weight,
                    Birthday: _birthday,
                    isParazit: _isParazit,
                    isKuduz: _isKuduz,
                    isLyme: _isLyme,
                    isKarmaI: _isKarmaI,
                    isKarmaII: _isKarmaII,
                    isBronshineI: _isBronshineI,
                    isBronshineII: _isBronshineII,
                    isCoronaI: _isCoronaI,
                    isCoronaII: _isCoronaII
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
                                    $("input[name=DName]").val('');
                                    $("input[name=DWeight]").val('');
                                    $("input[name=DBirthDay]").val('');
                                    //$("input[name=isParazit]").val(false);
                                    //$("input[name=isKuduz]").val(false);
                                    //$("input[name=isKarmaI]").val(false);
                                    //$("input[name=isKarmaII]").val(false);
                                    //$("input[name=isLosemiI]").val(false);
                                    //$("input[name=isLosemiII]").val(false);
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

    var _handlerTab = function () {
        $('.nav-tabs a').click(function (e) {
            e.preventDefault()
            $(this).tab('show')
        });
    }

    return {
        // public functions
        init: function () {
            _handlerTab();
            _handleCreateCatForm();
            _handleCreateDogForm();
        }
    };

}();

jQuery(document).ready(function () {
    KTCreate.init();
});