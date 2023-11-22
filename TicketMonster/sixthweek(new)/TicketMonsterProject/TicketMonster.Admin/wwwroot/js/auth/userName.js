const api = {
    getUserName: '/Auth/GetUserName'
}

const apiCaller = {
    getUserName: () => httpGet(api.getUserName)
}

const authUserNameVue = Vue.createApp({
    data() {
        return {
            userName: ''
        }
    },
    methods: {
        clickMe: function () {
            apiCaller.getUserName()
                .then((res) => {
                    console.log(res)
                    this.userName = res.body
                })
        }
    }
}).mount('#auth-username')