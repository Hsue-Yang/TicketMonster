const { createApp } = Vue
createApp({
    methods: {
        mailSender() {
        },
        gcAlert() {
            toastr.success('OK')
        },
        lineMe() {
            Swal.fire({
                html: '<img src="https://qr-official.line.me/gs/M_355jexxm_GW.png?oat_content=qr" width="200">',
                showConfirmButton: false,
                showCloseButton: true,
                customClass: { closeButton: 'shadow-none' },
                showClass: { popup: 'animate__animated animate__fadeInDown' },
                hideClass: { popup: 'animate__animated animate__fadeOutUp' }
            })
        }
    }
}).mount('#app')

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

$(document).ready(function () {
    $('#carousel').owlCarousel({
        items: 1,
        margin: 20,
        nav: false,
        dots: false,
        loop: true,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplayHoverPause: true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        navText: ["<i class='fa fa-chevron-left'></i>", "<i class='fa fa-chevron-right'></i>"],
        responsive: {
            576: { items: 2 },
            768: { items: 3 }
        }
    })
})