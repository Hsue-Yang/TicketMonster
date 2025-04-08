const { createApp } = Vue
createApp({
    data() {
        return {
            showNewPassword: false,
            showRetypePassword: false,
        }
    },
    created() {
        if (!sessionStorage.getItem('authenticate')) this.authorize()
    },
    methods: {
        newPasswordVisibility() {
            this.showNewPassword ^= true
        },
        retypePasswordVisibility() {
            this.showRetypePassword ^= true
        },
        authorize() {
            Swal.fire({
                input: 'password',
                inputPlaceholder: 'Enter your current password',
                showConfirmButton: false,
                showLoaderOnConfirm: true,
                allowOutsideClick: false,
                allowEnterKey: true,
                showClass: { popup: 'animate__animated animate__fadeInDown' },
                hideClass: { popup: 'animate__animated animate__fadeOutUp' },
                preConfirm: (enterPassword) => {
                    if (this.toSHA256(enterPassword) === sessionStorage.getItem('hashPassword')) {
                        sessionStorage.setItem('authenticate', true)
                        editingForm = true
                    } else {
                        Swal.showValidationMessage('Incorrect password')
                    }
                },
            })
        },
        toSHA256(origin) {
            const hash = CryptoJS.SHA256(origin)
            const hashHex = hash.toString(CryptoJS.enc.Hex)
            return hashHex.toUpperCase()
        }
    }
}).mount('#app')