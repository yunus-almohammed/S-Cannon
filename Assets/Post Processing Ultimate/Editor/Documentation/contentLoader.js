function setContentOnLoad(...names) {
    window.onload = function () {
        for (let i = 0; i < names.length; i += 2) {
            load(names[i], names[i + 1])
        }
    }
}

function load(container, data) {
    document.getElementById(container).innerHTML = data;
}