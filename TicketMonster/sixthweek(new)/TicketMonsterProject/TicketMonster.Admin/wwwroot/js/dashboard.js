const { createApp } = Vue
createApp({
    data() {
        return {
            loading: true,
            monthlyEarnings: '',
            annualEarnings: '',
            totolUsers: '',
            totalEvents: '',
            currentYear: new Date().getFullYear(),
            charts: ['line', 'bar'] /*this.charts[Math.floor(Math.random() * 2)]*/
        }
    },
    mounted() {
        this.chart = 'line'
        this.dashCards()
        this.initChart()
    },
    methods: {
        dashCards() {
            //axios.get('/api/Dashboard/GetEarningsByMonthly')
            //    .then(res => this.monthlyEarnings = res.data.filter(x => x.Year == new Date().getFullYear() && x.Month == new Date().getMonth() + 1).map(x => x.EarningsByMonthly).toLocaleString())
            //axios.get('/api/Dashboard/GetEarningsByAnnual')
            //    .then(res => this.annualEarnings = res.data.filter(x => x.Year == new Date().getFullYear()).map(x => x.EarningsByAnnual).toLocaleString())
            //axios.get('/api/Dashboard/GetUsersCount')
            //    .then(res => this.totolUsers = res.data)
            //axios.get('/api/Dashboard/GetEventsCount')
            //    .then(res => this.totalEvents = res.data)
            axios.get('/api/Dashboard/GetDashboardCards')
                .then(res => {
                    this.monthlyEarnings = res.data.earningsByMonthly.toLocaleString()
                    this.annualEarnings = res.data.earningsByAnnual.toLocaleString()
                    this.totolUsers = res.data.usersCount
                    this.totalEvents = res.data.eventsCount
                }); this.loadingDone()
        },
        drawMonthlyEarnings() {
            axios.get('/api/Dashboard/GetEarningsByMonthly')
                .then(res => {
                    this.drawChart('#drawMonthlyEarnings', 'bar', res.data.filter(x => x.Year == this.currentYear).map(x => x.Month), this.currentYear, res.data.filter(x => x.Year == this.currentYear).map(x => x.EarningsByMonthly), 'Monthly Earnings')
                })
        },
        drawAnnualEarnings() {
            axios.get('/api/Dashboard/GetEarningsByAnnual')
                .then(res => {
                    this.drawChart('#drawAnnualEarnings', 'line', res.data.map(x => x.Year), Math.ceil(this.currentYear/ 100) + 'st C', res.data.map(x => x.EarningsByAnnual), 'Annual Earnings')
                })
        },
        drawUserGrowth() {
            axios.get('/api/Dashboard/GetUserGrowth')
                .then(res => {
                    this.drawChart('#drawUserGrowth', 'line', res.data.filter(x => x.Year == this.currentYear).map(x => x.Month), this.currentYear, res.data.filter(x => x.Year == this.currentYear).map(x => x.NewUsersCount), 'New User Growth')
                })
        },
        drawOrderDesire() {
            axios.get('/api/Dashboard/GetOrderDesire')
                .then(res => {
                    this.drawChart('#drawOrderDesire', 'line', res.data.filter(x => x.Year == this.currentYear).map(x => x.Month), this.currentYear, res.data.filter(x => x.Year == this.currentYear).map(x => x.OrderDesire), 'Order Desire')
                })
        },
        initChart() {
            this.drawMonthlyEarnings()
            this.drawAnnualEarnings()
            this.drawUserGrowth()
            this.drawOrderDesire()
        },
        loadingDone() {
            this.loading = false
        },
        drawChart(_element, _type, _lables, _label, _data, _title) {
            const ctx = $(_element)
            new Chart(ctx, {
                type: _type,
                data: {
                    labels: _lables,
                    datasets: [{
                        label: _label,
                        data: _data,
                        borderWidth: 1,
                        backgroundColor: Array(_data.length).fill().map(() => this.getRandomColor()),
                        borderColor: Array(_data.length).fill().map(() => this.getRandomColor()),
                        hoverBackgroundColor: Array(_data.length).fill().map(() => this.getRandomColor()),
                        hoverBorderColor: Array(_data.length).fill().map(() => this.getRandomColor()),
                    }]
                },
                options: {
                    scales: {
                        x: { beginAtZero: true },
                        y: { beginAtZero: true }
                    },
                    plugins: {
                        legend: { display: true, position: 'bottom' },
                        title: {
                            display: true,
                            text: _title,
                            padding: { top: 10, bottom: 10 }
                        }
                    },
                    animation: {
                        duration: 2000,
                        easing: 'easeInOutQuad'
                    },
                }
            })
        },
        getRandomColor() {
            const r = Math.floor(Math.random() * 256)
            const g = Math.floor(Math.random() * 256)
            const b = Math.floor(Math.random() * 256)
            const alpha = 0.6
            return `rgba(${r}, ${g}, ${b}, ${alpha})`
        }
    }
}).mount('#app')