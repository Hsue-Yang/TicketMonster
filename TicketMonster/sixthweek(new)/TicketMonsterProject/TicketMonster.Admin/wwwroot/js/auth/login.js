(function IIFE() {
    const HOME_PAGE = '/Dashboard/Index'
    const api = { login: '/Auth/Login' }
    const apiCaller = { login: (loginQuery) => httpPost(api.login, loginQuery) }

    const authLoginVue = Vue.createApp({
        data() {
            return {
                login: {
                    userName: '',
                    password: ''
                }
            }
        },
        methods: {
            loginBtn() {
                handleLogin({ ...this.login })
                //window.location.href = '~/Home/index.cshtml';
            }
        }
    }).mount('#authLogin')

    function handleLogin(loginQuery) {
        console.log('loginQuery:')
        console.log(loginQuery)
        apiCaller.login(loginQuery)
            .then((res) => {

                console.log('res:' + res.isSuccess)
                if (res.isSuccess) {
                    const { token, expireTime } = res.body
                    setToken(token, expireTime)
                    redirectToHome()
                } 
            })
            .catch((err) => {
                console.log('err:' + err)
                toastr.error('±b¸¹©Î±K½X¿ù»~');
            })   
    }

    function setToken(token, expire) {
        Cookies.set('token', token, {
            expires: new Date(expire * 1000)
        })
    }

    function redirectToHome() {
        window.location.href = HOME_PAGE
    }
})();