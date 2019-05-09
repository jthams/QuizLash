// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Open when someone clicks on the element */
function toggleNav() {
    var width = document.getElementById("myNav").style.width;
    if (width === "50%") {
        document.getElementById("myNav").style.width = "0%";
    } 
    else {
        document.getElementById("myNav").style.width = "50%";
    }
}
