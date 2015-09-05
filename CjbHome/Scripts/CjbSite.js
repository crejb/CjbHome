// Handle scroll for sticky navbar
$(document).ready(function () {
    $(window).bind('scroll', function () {
        // When scrolling down, if we scroll further than the header image than
        // change the class of the menu to make it stick to the top
        var navHeight = $('.intro-header').height();
        if ($(window).scrollTop() > navHeight) {
            $('#menu').addClass('menu-fixed');
        }
        else {
            $('#menu').removeClass('menu-fixed');
        }
    });
});