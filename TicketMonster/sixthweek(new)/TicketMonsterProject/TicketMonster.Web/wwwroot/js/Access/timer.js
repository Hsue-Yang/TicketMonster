const { createApp } = Vue
createApp({
    data() {
        return {
            time: parseInt(sessionStorage.getItem('timer'))
        }
    },
    mounted() {
        setInterval(this.countdown, 1000)
    },
    methods: {
        countdown() {
            this.time--
            this.warning()
            this.timer()
        },
        timer() {
            if (this.time > 0) {
                sessionStorage.setItem('timer', this.time.toString())
            } else {
                clearInterval()
                sessionStorage.removeItem('timer')
                window.location.href = "SignIn"
            }
        },
        warning() {
            if (this.time == 6) {
                toastr.warning('The page will be leaving soon ⚠️')
            }
        }
    }
}).mount('#app')