"use strict";

(function ($) {
    // Navigation
    $.fn.mkNav = function (options) {
        // Variables
        var headerContainer = $(".header-area");
        var navContainer = $(".mk-nav-container");
        var mk_nav = $(".mknav ul");
        var mk_navli = $(".mknav > ul > li");
        var navbarToggler = $(".mk-navbar-toggler");
        var closeIcon = $(".mkcloseIcon");
        var navToggler = $(".navbarToggler");
        var mkMenu = $(".mk-menu");
        var var_window = $(window);

        // default options
        var defaultOpt = $.extend(
            {
                theme: "light",
                breakpoint: 991,
                openCloseSpeed: 350,
                megaopenCloseSpeed: 700,
                alwaysHidden: false,
                openMobileMenu: "left",
                stickyNav: true,
            },
            options
        );

        return this.each(function () {
            // light or dark theme
            if (defaultOpt.theme === "light" || defaultOpt.theme === "dark") {
                navContainer.addClass(defaultOpt.theme);
            }

            // open mobile menu direction 'left' or 'right' side
            if (defaultOpt.openMobileMenu === "left" || defaultOpt.openMobileMenu === "right") {
                navContainer.addClass(defaultOpt.openMobileMenu);
            }

            // navbar toggler
            navbarToggler.on("click", function () {
                navToggler.toggleClass("active");
                mkMenu.toggleClass("menu-on");
            });

            // close icon
            closeIcon.on("click", function () {
                mkMenu.removeClass("menu-on");
                navToggler.removeClass("active");
            });

            // add dropdown & megamenu class in parent li class
            mk_navli.has(".dropdown").addClass("cn-dropdown-item");

            // adds toggle button to li items that have children
            mk_nav.find("li a").each(function () {
                if ($(this).next().length > 0) {
                    $(this).parent("li").addClass("has-down").append('<span class="dd-trigger"></span>');
                }
            });

            // expands the dropdown menu on each click
            mk_nav.find("li .dd-trigger").on("click", function (e) {
                e.preventDefault();
                $(this).parent("li").children("ul").stop(true, true).slideToggle(defaultOpt.openCloseSpeed);
                $(this).parent("li").toggleClass("active");
            });

            // check browser width in real-time
            function breakpointCheck() {
                var windoWidth = window.innerWidth;
                if (windoWidth <= defaultOpt.breakpoint) {
                    navContainer.removeClass("breakpoint-off").addClass("breakpoint-on");
                } else {
                    navContainer.removeClass("breakpoint-on").addClass("breakpoint-off");
                }
            }

            breakpointCheck();

            var_window.on("resize", function () {
                breakpointCheck();
            });

            // always hidden enable
            if (defaultOpt.alwaysHidden === true) {
                navContainer.addClass("breakpoint-on").removeClass("breakpoint-off");
            }

            // sticky
            if (defaultOpt.stickyNav === true) {
                var_window.on("scroll", function () {
                    if (var_window.scrollTop() > 0) {
                        headerContainer.addClass("sticky");
                    } else {
                        headerContainer.removeClass("sticky");
                    }
                });
            }
        });
    };

    // Input file
    var $input = $(".inputfile");
    var $label = $input.next("label");
    var labelVal = $label.html();

    $input.on("change", function (e) {
        var fileName = "";

        if (e.target.value) fileName = e.target.value.split("\\").pop();

        if (fileName) $label.find("span").html(fileName);
        else $label.html(labelVal);
    });

    // Firefox bug fix
    $input
        .on("focus", function () {
            $input.addClass("has-focus");
        })
        .on("blur", function () {
            $input.removeClass("has-focus");
        });
})(jQuery, window, document);

$("#mkNav").mkNav();

// Offset position for body element depending on header height
$("#content").css("margin-top", $(".header-area").height());

$(".editor").trumbowyg({
    btns: [
        ["strong", "em"],
        ["justifyLeft", "justifyCenter", "justifyRight", "justifyFull"],
        ["insertImage", "link"],
    ],
});

$(".regs").trumbowyg({
    btns: [
        ["strong", "em"],
        ["justifyLeft", "justifyFull"],
    ],
});
