Swal.fire({
    icon: 'success',
    html: '<section class="d-flex justify-content-between align-items-center p-2"><div class="d-flex flex-column align-items-start"><small class="badge text-body-tertiary px-0">Step 3 of 3</small><h2 class="fs-4 fw-semibold">Reset Password</h2></div><img class="opacity-75" src="https://cdn2.iconfinder.com/data/icons/leto-blue-gdpr/64/gdpr-23-1024.png" style="width:80px;height:80px"></section><span class="fw-semibold small text-start">Password has been successfully reset. Plz check your email and attempt to sign in to complete the password reset process. Tks u for using our services! 🙂</span>',
    timer: 300000,
    timerProgressBar: true,
    allowOutsideClick: false,
    allowEscapeKey: false,
    allowEnterKey: true,
    showClass: { popup: 'animate__animated animate__fadeInDown' },
    hideClass: { popup: 'animate__animated animate__fadeOutUp' },
    customClass: { icon: 'fs-8 position-absolute start-50 translate-middle-x' }
}).then(() => { window.location.href = 'SignIn' })