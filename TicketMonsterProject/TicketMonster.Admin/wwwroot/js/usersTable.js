const { createApp, ref } = Vue
createApp({
    components: { EasyDataTable: window["vue3-easy-data-table"] },
    data() {
        return {
            user: '',
            loading: true,
            searchValue: '',
            items: [],
            headers: [
                { text: "ID", value: "ID" },
                { text: "Name", value: "LastName", sortable: true },
                { text: "Email", value: "Email", sortable: true },
                { text: "Country", value: "Country", sortable: true },
                { text: "Create Time", value: "CreateTime", sortable: true },
                { text: "Orders", value: "operation" }
            ]
        }
    },
    mounted() {
        this.getAll()
    },
    methods: {
        getAll() {
            axios.get('/api/CustomerTable/GetAllUsers')
                .then(res => this.items = res.data)
                .catch(ex => console.error(ex))
                .finally(() => this.loading = false)
        },
        showUserOrders(user) {
            axios.get(`/api/CustomerTable/GetEventsByUser/?id=${user.ID}`)
                .then(res => {
                    OrdersHTML = res.data.map(x => `<section class="position-relative" data-id="${x.ID}">
                    <div class="position-absolute bottom-0 rounded text-light m-2 p-2 lh-sm bg-text w-99"><span class="fw-semibold text-truncate-1">${x.EventName}</span>
                        <span class="fw-semibold d-flex justify-content-between"><b class="text-truncate">${x.VenueName}</b></span>
                        <span class="fw-semibold d-flex justify-content-between text-danger"><b class="text-truncate">$${x.BillingAmount.toLocaleString()}</b><small class="badge text-danger px-0">${x.OrderDate}</small></sapn>
                    </div><small class="position-absolute top-0 end-0 badge text-white opacity-25">${x.EventDate}</small><img src="${x.EventPic}" class="object-fit-cover w-100 ratio-5x3"></section>`)
                    Swal.fire({
                        html: `<article class="owl-carousel text-white" id="carousel">.${OrdersHTML}</article>`,
                        showCloseButton: true,
                        showLoaderOnConfirm: true,
                        showConfirmButton: true,
                        confirmButtonText: 'Order Details',
                        allowOutsideClick: true,
                        allowEscapeKey: true,
                        allowEnterKey: true,
                        customClass: { closeButton: 'shadow-none' },
                        showClass: { popup: 'animate__animated animate__fadeInDown' },
                        hideClass: { popup: 'animate__animated animate__fadeOutUp' },
                        preConfirm: () => { this.user = user
                            this.showUserOrderDetails($('.owl-carousel').find('.owl-item.active [data-id]').data('id'))
                        }
                    })
                    this.initCarousel()
                })
                .catch(ex => console.error(ex))
        },
        showUserOrderDetails(order) {
            axios.get(`/api/CustomerTable/GetSeatsByUser/?id=${order}`)
                .then(res => {
                    OrderDetailsHTML = res.data.map(x => `<tr><td>${x.EventSeat}</td><td>$${x.Price.toLocaleString()}</td></tr>`)
                    Swal.fire({
                        html: `<table class="table table-dark table-striped mt-2"><tbody>${OrderDetailsHTML}</tbody></taable>`,
                        showCloseButton: true,
                        showLoaderOnConfirm: true,
                        showConfirmButton: true,
                        confirmButtonText: 'Orders',
                        allowOutsideClick: true,
                        allowEscapeKey: true,
                        allowEnterKey: true,
                        customClass: { htmlContainer: 'text-truncate overflow mh-30', closeButton: 'shadow-none' },
                        showClass: { popup: 'animate__animated animate__fadeInDown' },
                        hideClass: { popup: 'animate__animated animate__fadeOutUp' },
                        preConfirm: () => this.showUserOrders(this.user)
                    })
                })
                .catch(ex => console.error(ex))
        },
        initCarousel() {
            $(document).ready(function () {
                $('#carousel').owlCarousel({
                    items: 1,
                    loop: true,
                    autoplay: true,
                    autoplaySpeed: 5000,
                    autoplayTimeout: 5000,
                    autoplayHoverPause: true,
                    nav: false,
                    dots: true,
                    center: true,
                    animateIn: 'fadeIn',
                    animateOut: 'fadeOut'
                })
            })
        }
    }
}).mount('#app')