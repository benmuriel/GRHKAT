new Vue({
    el: '#app',
    data: {
        item: {
            agent_id: null,
            object_id: null,
            details: null,
            object_name: null,
            agent: null,
            service: null,
            fonction: null
        },
        datas: []
    },
    methods: {
        loadDatas: function () {
            var page = $('#page').text();
            if (page.length == 0) return
            var me = this;
            loaderOpen()
            var url = $('#api-validation-url').attr('href')
            axios.get(url + page)
                .then(response => {
                    me.datas = response.data
                    loaderClose()
                })
        },
        validationForm: function (item) {
            this.item = item;
            Metro.dialog.open('#validationform')
        },
        validate: function () {
            loaderOpen()
            var url = $('#api-validation-url').attr('href')
            axios.post(url, JSON.stringify(this.item), {
                headers: {
                    'Content-Type': 'application/json',
                    'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).then(response => {
                Metro.toast.create(response.data, null, null, "success", { showTop: true })
                loaderClose()
                this.loadDatas();
            }).catch(function (error) {
                Metro.toast.create(error, null, null, "alert", { showTop: true })
                loaderClose()
            })
        }
    },
    mounted: function () {
        this.loadDatas();
    }
});