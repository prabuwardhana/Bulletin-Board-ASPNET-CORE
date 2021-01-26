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

$(function () {
    $('[data-toggle="popover"]').popover({
        placement: "bottom",
        content: function () {
            return $("#notification-content").html();
        },
        html: true,
    });

    $("body").append(`<div id="notification-content" class="hide"></div>`);

    function getNotification() {
        var res = "<ul class='list-group'>";
        $.ajax({
            url: "/Notification/getNotification",
            method: "GET",
            success: function (result) {
                console.log(result);
                if (result.count != 0) {
                    $("#notificationCount").html(result.count);
                    $("#notificationCount").show("slow");
                } else {
                    $("#notificationCount").html();
                    $("#notificationCount").hide("slow");
                    $("#notificationCount").popover("hide");
                }

                var notifications = result.userNotification;
                notifications.forEach((element) => {
                    res = res + "<li class='list-group-item notification-text' data-id='" + element.notification.id + "'>" + element.notification.text + "</li>";
                });

                res = res + "</ul>";

                $("#notification-content").html(res);
            },
            error: function (error) {
                console.log(error);
            },
        });
    }

    $("li.notification-text").on("click", function (e) {
        var target = e.target;
        var id = $(target).data("id");
        console.log(id)

        readNotification(id, target);
    });

    function readNotification(id, target) {
        $.ajax({
            url: "/Notification/ReadNotification",
            method: "GET",
            data: { notificationId: id },
            success: function (result) {
                getNotification();
                $(target).fadeOut("slow");
            },
            error: function (error) {
                console.log(error);
            },
        });
    }

    getNotification();

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalServer")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    
    connection.on("displayNotification", () => {
        getNotification();
    });

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    }

    connection.onclose(start);

    // Start the connection.
    start();
});
