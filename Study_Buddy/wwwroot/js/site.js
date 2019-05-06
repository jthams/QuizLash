// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Open when someone clicks on the element */
function openNav() {
    var overlay = document.getElementById("myNav");
    if (overlay.style.width === "50%") {
        overlay.style.width = "0%";
    } else {
        overlay.style.display = "50%"
    }
}

/* Close when someone clicks on the "x" symbol inside the overlay */
function closeNav() {
    document.getElementById("myNav").style.width = "0%";
}

/*function openNav() {
    var overlay = document.getElementById("myNav");
    if (overlay.style.width === "50%") {
        overlay.style.width = "0%";
    } else {
        overlay.style.display = "50%"
    }
}*/