function alertaClick(title, text, type) {
    Swal.fire({
        title: title,
        text: text,
        icon: type
    });
}

function delayRedirect(_link, _delayTime) {
    setTimeout(function () {
        window.location.href = _link;
    }, _delayTime);
}

