﻿function password() {
    var icon = $('.bi')
    var pass = $('#password')
    if (icon.hasClass('bi-eye')) {
        icon.removeClass('bi-eye')
        icon.addClass('bi-eye-slash')
        pass.attr('type', 'text')
    } else {
        icon.addClass('bi-eye')
        icon.removeClass('bi-eye-slash')
        pass.attr('type', 'password')
    }
}