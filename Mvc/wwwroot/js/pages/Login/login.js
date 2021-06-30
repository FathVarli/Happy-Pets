"use strict";

// Class Definition
var KTLogin = function () {
    var _login;

    var _showForm = function (form) {
        var cls = 'login-' + form + '-on';
        var form = 'kt_login_' + form + '_form';

        _login.removeClass('login-forgot-on');
        _login.removeClass('login-signin-on');
        _login.removeClass('login-signup-on');

        _login.addClass(cls);

        KTUtil.animateClass(KTUtil.getById(form), 'animate__animated animate__backInUp');
    }

    var _handleSignInForm = function () {
        var validation;

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            KTUtil.getById('kt_login_signin_form'),
            {
                fields: {
                    Email: {
                        validators: {
                            notEmpty: {
                                message: 'Email zorunludur.'
                            }
                        }
                    },
                    Password: {
                        validators: {
                            notEmpty: {
                                message: 'Parola zorunludur.'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    submitButton: new FormValidation.plugins.SubmitButton(),
                    //defaultSubmit: new FormValidation.plugins.DefaultSubmit(), // Uncomment this line to enable normal button submit after form validation
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );





        $('#kt_login_signin_submit').on('click', function (e) {
            e.preventDefault();

            var _email = $("input[name=Email]").val();
            var _pass = $("input[name=Password]").val();

            console.log(_email);
            console.log(_pass);

            var _loginViewModel = {
                userLoginDto: {
                    Email: _email,
                    Password: _pass
                }
            }


            validation.validate().then(function (status) {
                if (status == 'Valid') {
                    $.ajax({
                        type: "POST",
                        url: "/Auth/Login",
                        dataType: 'json',
                        data: _loginViewModel,
                        success: function (response) {
                            if (response.success) {
                                location.href = response.url;
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
                                text: "Üzgünüm, giriş yaparken bir sorunla karşılaştık.",
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
                        text: "Üzgünüm, giriş yaparken bir sorunla karşılaştık.",
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

        // Handle forgot button
        $('#kt_login_forgot').on('click', function (e) {
            e.preventDefault();
            _showForm('forgot');
        });

        // Handle signup
        $('#kt_login_signup').on('click', function (e) {
            e.preventDefault();
            _showForm('signup');
        });
    }

    var _handleSignUpForm = function (e) {
        var validation;
        var form = KTUtil.getById('kt_login_signup_form');

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            form,
            {
                fields: {
                    FirstName: {
                        validators: {
                            notEmpty: {
                                message: 'Ad alanı zorunludur.'
                            }
                        }
                    },
                    LastName: {
                        validators: {
                            notEmpty: {
                                message: 'Soyad alanı zorunludur.'
                            }
                        }
                    },
                    Eposta: {
                        validators: {
                            notEmpty: {
                                message: 'Email alanı zorunludur.'
                            },
                            emailAddress: {
                                message: 'Lütfen doğru email formatında giriniz.'
                            }
                        }
                    },
                    Password: {
                        validators: {
                            notEmpty: {
                                message: 'Parola alanı zorunludur.'
                            }
                        }
                    },
                    ConfirmPassword: {
                        validators: {
                            notEmpty: {
                                message: 'Parola tekrar alanı zorunludur.'
                            },
                            identical: {
                                compare: function () {
                                    return form.querySelector('[name="password"]').value;
                                },
                                message: 'Paralonız ile eşleşmemektedir.'
                            }
                        }
                    },
                    agree: {
                        validators: {
                            notEmpty: {
                                message: 'Tüm şartları ve koşullarını onaylayınız.'
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

        $('#kt_login_signup_submit').on('click', function (e) {

            e.preventDefault();

            var _firstname = $("input[name=FirstName]").val();
            var _surname = $("input[name=LastName]").val();
            var _email = $("input[name=Eposta]").val();
            var _pass = $("input[name=RPassword]").val();
            var _cPass = $("input[name=ConfirmPassword]").val();


            var _loginViewModel = {
                registerDto: {
                    FirstName: _firstname,
                    LastName: _surname,
                    Email: _email,
                    Password: _pass,
                    ConfirmPassword: _cPass
                }
            }

            validation.validate().then(function (status) {
                if (status == 'Valid') {
                    $.ajax({
                        type: "POST",
                        url: "/Auth/Register",
                        dataType: 'json',
                        data: _loginViewModel,
                        success: function (response) {
                            if (response.success) {
                                swal.fire({
                                    text: "Kaydınız başarıyla oluşturulmuştur.",
                                    icon: "success",
                                    buttonsStyling: false,
                                    confirmButtonText: "Tamam",
                                    customClass: {
                                        confirmButton: "btn font-weight-bold btn-light-primary"
                                    }
                                }).then(function () {
                                    $("input[name=FirstName]").val('');
                                    $("input[name=LastName]").val('');
                                    $("input[name=Eposta]").val('');
                                    $("input[name=RPassword]").val('');
                                    $("input[name=ConfirmPassword]").val('');
                                    _showForm('signin');
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
                                text: "Üzgünüm, kayıt olurken bir sorunla karşılaştık.",
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
                        text: "Üzgünüm, kayıt olurken bir sorunla karşılaştık.",
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

        // Handle cancel button
        $('#kt_login_signup_cancel').on('click', function (e) {
            e.preventDefault();

            _showForm('signin');
        });
    }

    var _handleForgotForm = function (e) {
        var validation;

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            KTUtil.getById('kt_login_forgot_form'),
            {
                fields: {
                    Email: {
                        validators: {
                            notEmpty: {
                                message: 'Email alanı zorunludur.'
                            },
                            emailAddress: {
                                message: 'Lütfen doğru email formatında giriniz.'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );

        // Handle submit button
        $('#kt_login_forgot_submit').on('click', function (e) {
            e.preventDefault();

            var _email = $("input[name=FEmail]").val();
            var _loginViewModel = {
                forgotPasswordDto: {
                    Email: _email,
                }
            }
            validation.validate().then(function (status) {
                if (status == 'Valid') {

                    $.ajax({
                        type: "POST",
                        url: "/Auth/ForgotPassword",
                        dataType: 'json',
                        data: _loginViewModel,
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
                                    $("input[name=FEmail]").val('');
                                    _showForm('signin');
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
                                text: "Üzgünüm, kayıt olurken bir sorunla karşılaştık.",
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
                        text: "Üzgünüm, parolanızı sıfırlarken bir sorunla karşılaştık. Lütfen tekrar deneyiniz.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn font-weight-bold btn-light-primary"
                        }
                    }).then(function () {
                        KTUtil.scrollTop();
                    });
                }
            });
        });

        // Handle cancel button
        $('#kt_login_forgot_cancel').on('click', function (e) {
            e.preventDefault();

            _showForm('signin');
        });
    }

    // Public Functions
    return {
        // public functions
        init: function () {
            _login = $('#kt_login');

            _handleSignInForm();
            _handleSignUpForm();
            _handleForgotForm();
        }
    };
}();

// Class Initialization
jQuery(document).ready(function () {
    KTLogin.init();
});
