new Vue({
    el: '#app',
    data: {
        datas: [],
        currentStr: null,
        currentPositions: [],
        currentProfil: null,
        request: {
            search: '',
            order: 'savedesc',
            limit:100
        }
    },
    computed: {

    },
    methods: {
       
        loadDatas: function () {
            var search = $('#search').text(); 
            var me = this;
            loaderOpen()

            var url = $('#api-agent-url').attr('href') 
            axios.get(url,
                {
                    params: {
                        search: me.request.search,
                        order: me.request.order,
                        limit: me.request.limit
                    }
                })
                .then(response => {
                    me.datas = response.data
                    loaderClose()
                    initFilter();
                })
        },
        showProfil: function (agent_id) {
            var url = $('#profil-url').attr('href')
            // console.log(url.replace('0', agent_id)); return
            // var win = window.open(url.replace('0', agent_id), '_blank')
            // win.focus()
            window.location.href = url.replace('0', agent_id)

        },
        showCurrentPositionList: function (item) {
            var me = this
            this.currentProfil = item
            var url = $('#api-situation-url').attr('href')
            loaderOpen()
            axios.get(url, {
                params: {
                    agent_id: this.currentProfil.agent_id,
                    state:"running"
                }
            })
                .then(response => {
                    me.currentPositions = response.data                  
                    Metro.dialog.open('#lastPositionList')
                    loaderClose()
                })
           
        },
        loadDepts: function () {
            var me = this
            var url = $('#api-structure-url').attr('href')
            axios.get(url)
                .then(response => {
                    me.depts = response.data
                    loaderClose()
                    initFilter();
                })
        }
    },
    mounted: function () {
        this.loadDatas();
        $('#tp_0').removeClass("outline")
    }
});