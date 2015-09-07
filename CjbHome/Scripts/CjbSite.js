// Handle scroll for sticky navbar
// When scrolling down, if we scroll further than the header image than
// change the class of the menu to make it stick to the top
// Also need to put in a dummy element to replace the height that is lost
// when changing the menu position to fixed, otherwise there can be some nasty flickering
$(document).ready(function () {
        $(window).bind("scroll", function() {
            var shouldBeSticky = $(window).scrollTop() > $(".intro-header").height();
            var menuIsSticky = ($("#sticky-spacer").length > 0);
            if (shouldBeSticky && !menuIsSticky) {
                jQuery("<div/>", {
                    id: "sticky-spacer",
                    height: $("#menu").height()
                }).insertBefore("#menu");
                $("#menu").addClass("menu-fixed");
            } else if (!shouldBeSticky && menuIsSticky) {
                $("#menu").removeClass("menu-fixed");
                $("#sticky-spacer").remove();
            }
        });
    }
)