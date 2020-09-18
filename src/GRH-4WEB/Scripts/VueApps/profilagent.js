new Vue({
    el: '#app',
    data: {
        datas: []
    },
    methods: {
        loadDatas: function () {
            var search = $('#search').text();
            var profil = $('#profil').text();          
            var me = this;
            loaderOpen()
            var url = $('#api-agent-url').attr('href')
            axios.get(url+'?search=' + search + '&profil=' + profil)
                .then(response => {
                    me.datas = response.data
                    loaderClose()
                    initFilter();
                })
        },
        profil: function (agent_id) {
            var url = $('#profil-url').attr('href')
            var win = window.open(url.replace('0', agent_id), '_blank')
            win.focus()
        }
    },
    mounted: function () {
        this.loadDatas();
    }
});