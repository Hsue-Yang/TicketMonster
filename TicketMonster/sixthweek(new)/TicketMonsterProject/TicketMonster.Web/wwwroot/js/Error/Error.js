const { createApp } = Vue
createApp({
    data() {
        return {
            ufoS: [],
            monsterS: []
        }
    },
    mounted() {
        this.toastr()
        setInterval(this.addUfoS, 4040)
        setInterval(this.addMonsterS, 1111)
    },
    methods: {
        addUfoS() {
            const a = {
                x: Math.random() * 100 + '%',
                y: Math.random() * 100 + '%',
                rotate: Math.random() * 360 + 'deg',
                width: Math.floor(Math.random() * (150 - 100 + 1)) + 100
            }
            this.ufoS.push(a)
        },
        addMonsterS() {
            const m = {
                x: Math.random() * 100 + '%',
                y: Math.random() * 100 + '%',
                rotate: Math.random() * 360 + 'deg',
                width: Math.floor(Math.random() * (120 - 80 + 1)) + 80,
                src: Math.floor(Math.random() * (100 - 76 + 1)) + 76
            }
            this.monsterS.push(m)
        },
        toastr() {
            toastr.options = {
                newestOnTop: true,
                progressBar: true,
                preventDuplicates: true,
                positionClass: 'toast-top-right',
                timeOut: '404404'
            }
            toastr['error']('請找到 404UFO 帶您逃離引力吸引', 'sorry 您進入了宇宙的黑洞')
        },
        tooltip() {
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip()
            })
        }
    }
}).mount('#app')