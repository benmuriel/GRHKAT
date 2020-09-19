new Vue({
    el: '#app',
    data: {
        datas: [],
        depts: [],
        currentStr:null
    },
    computed: {
        profilAvecEmploi: function () {
            var profil = $('#profil').text();
            return profil === 'avec_emploi'
        }
    },
    methods: {
        loadDatas: function () {
            var search = $('#search').text();
            var profil = $('#profil').text();          
            var me = this;
            loaderOpen()
          
            var url = $('#api-agent-url').attr('href') + (this.currentStr !== null ? this.currentStr.id : '') + '?'
            url = url + (search!==''? 'search=' + search+'&':'') + 'profil=' + profil
            axios.get(url)
                .then(response => {
                    me.datas = response.data
                    loaderClose()
                    initFilter();
                })
        },
        profil: function (agent_id) {
            var url = $('#profil-url').attr('href')
           // console.log(url.replace('0', agent_id)); return
            var win = window.open(url.replace('0', agent_id), '_blank')
            win.focus()
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
        },
    },
    mounted: function () {
        this.loadDepts()
        this.loadDatas();
    }
});