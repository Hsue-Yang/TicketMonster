const { createApp } = Vue
createApp({
    data() {
        return {
            showPassword: false
        }
    },
    methods: {
        passwordVisibility() {
            this.showPassword ^= true
        }
    }
}).mount('#app')