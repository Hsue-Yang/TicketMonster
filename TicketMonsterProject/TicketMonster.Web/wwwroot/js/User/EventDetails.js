const { createApp } = Vue
createApp({
    methods: {
        transferTickets(id) {
            Swal.fire({
                icon: 'info',
                input: 'email',
                inputPlaceholder: 'input u wanna transferTicket email',
                showLoaderOnConfirm: true,
                showConfirmButton: true,
                showCancelButton: true,
                showCloseButton: true,
                customClass: { closeButton: 'shadow-none' },
                showClass: { popup: 'animate__animated animate__fadeInDown' },
                hideClass: { popup: 'animate__animated animate__fadeOutUp' },
                preConfirm: (transferTicketEmail) =>
                    axios.post(`/api/TransferTickets/?email=${transferTicketEmail}&id=${id}`)
                        .then(() => {
                            toastr['success']('Ticket Transfer Success!')
                            Swal.fire('Ticket Transfer Success', '', 'success').then(() => window.location.href = '/User/Events')
                        }).catch(() => {
                            toastr['error']('Ticket Transfer Error!')
                            Swal.fire('Ticket Transfer Error', '', 'error')
                        })
            })
        },
        sellTickets() {
            Swal.fire('','','question')
        },
        viewMap() {
            Swal.fire({
                html: VenueSvg,
                showConfirmButton: false,
                showCloseButton: true,
                customClass: { closeButton: 'shadow-none' },
                showClass: { popup: 'animate__animated animate__fadeInDown' },
                hideClass: { popup: 'animate__animated animate__fadeOutUp' }
            })
        },
        lineMe() {
            Swal.fire({
                html: '<img src="https://qr-official.line.me/gs/M_355jexxm_BW.png?oat_content=qr" width="200">',
                showConfirmButton: false,
                showCloseButton: true,
                customClass: { closeButton: 'shadow-none' },
                showClass: { popup: 'animate__animated animate__fadeInDown' },
                hideClass: { popup: 'animate__animated animate__fadeOutUp' }
            })
        }
    }
}).mount('#app')