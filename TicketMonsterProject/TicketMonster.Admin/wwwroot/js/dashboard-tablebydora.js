const { createApp, ref, onMounted } = Vue;

const App = {
   
    components: { EasyDataTable: window["vue3-easy-data-table"] },
    setup() {
        const headers = ref([
            { text: "Name", value: "Name" },
            { text: "Position", value: "Position" },
            { text: "Office", value: "Office" },
            { text: "Age", value: "Age", sortable: true },
            { text: "Startdate", value: "Startdate" },
            { text: "Salary", value: "Salary" }
        ]);

        const items = ref([]);
        const loading = ref(false);

        const getAll = () => {
            // 在開始載入資料時，設置 loading 為 true
            loading.value = true;
            axios.get('https://raw.githubusercontent.com/flyingtrista/FileStorage/main/datatableData.json')
                .then(res => {
                    console.log(res);
                    if (res.status == 200) {
                        // 成功載入資料後，設置 items，並將 loading 設置為 false
                        var result = res.data.data
                        // result.forEach(element => {
                        //     //每個物件最後加上 Actions: '<button class="btn btn-primary">編輯</button> <button class="btn btn-danger">刪除</button>'
                        //     element.Actions = '<button>編輯</button> <button>刪除</button>';

                        // });
                        items.value = result;
                        console.log('result:')
                        console.log(result);
                    }
                })
                .catch(error => {
                    console.error(error);
                })
                .finally(() => {
                    // 無論成功或失敗，都將 loading 設置為 false
                    loading.value = false;
                });
        };

        // 在 Vue 3 中使用 onMounted 鉤子執行初始化
        onMounted(() => {
            getAll();
        });

        return {
            headers,
            items,
            loading,
        };
    }
    
};

createApp(App).mount("#app");