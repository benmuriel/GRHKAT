new Vue({
    el: '#app',
    data: {
        poste: {
            designation: '',
            id: 0,
            fonction_id: 0,
            structure_id: null,
            structure: null,
            is_politic: false,
            fonction: {
                designation: '',
                id: 0
            }
        },
        departement: {
            id: 0,
            designation: null
        },
        datas: [],
        depts: [],
        fonctions: []
    },
    computed: {
        fullPostename: function () {
            this.poste.fonction_id = this.poste.fonction.id;
            return this.fullname = this.poste.fonction.designation + " " + this.poste.designation;
        }
    },
    methods: {

        changeDepartement: function (id, designation) {
            this.departement.id = id
            this.departement.designation = designation
            $('.str').removeClass('bg-cyan fg-white p-1')
            $('#str_' + id).addClass('bg-cyan fg-white p-1')
            this.loadDatas(null)
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
        loadFonctions: function () {
            var me = this

            var url = $('#api-fonction-url').attr('href') 
            axios.get(url)
                .then(response => {
                    me.fonctions = response.data
                    loaderClose()
                })
        },
        loadDatas: function (state) {
            var search = null;
            var me = this;
            loaderOpen()
            var url = $('#api-posteemploi-url').attr('href') 
            var url = url + me.departement.id
            if (state !== null)
                url += '?state=' + state
            if (search != null)
                url += '&search=' + search

            axios.get(url)
                .then(response => {
                    me.datas = response.data
                    loaderClose()
                    initFilter();
                })
        },
        save: function () {
            loaderOpen()
            var me = this;
            var url = $('#api-posteemploi-url').attr('href') 
            axios.post(url, JSON.stringify(this.poste), {
                headers: {
                    'Content-Type': 'application/json',
                    'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).then(response => { 
                Metro.toast.create('Operation effetuée avec succes !', null, null, "success", { showTop: true })
                me.loadDatas(null)
                //me.datas.unshift(response.data);
            }).catch(function (error) {
                Metro.toast.create(error, null, null, "alert", { showTop: true })
                loaderClose()
            })
        },
        voirPoste: function (id) {
            var url = $('#poste-url').attr('href')
            var win = window.open(url.replace('0', id), '_blank')
            win.focus()
        },
        voirProfilAgent: function (id) {
            var url = $('#profil-url').attr('href')
            var win = window.open(url.replace('0', id), '_blank')
            win.focus()
        },
        deletePoste: function (poste) {
            this.poste = poste;
            Metro.dialog.open('#deleteposteform')
        },
        editPoste: function (poste) {           
            this.poste.id = poste.id;
            this.poste.fonction_id = poste.fonction_id;
            this.poste.designation = poste.designation !== null ? poste.designation : '';
            this.poste.fonction = {
                designation: poste.fonction,
                id: poste.fonction_id
            }
            this.poste.structure_id = this.departement.id;
            this.poste.structure = this.departement.designation;
            Metro.dialog.open('#newposteform')
        },
        newPoste: function () {
            this.poste.id = 0;
            this.poste.fonction_id = 0;
            this.poste.fonction = {
                designation: '',
                id: 0
            };
            this.poste.structure_id = this.departement.id;
            this.poste.structure = this.departement.designation;
            Metro.dialog.open('#newposteform')
        }
    },
    mounted: function () {
        this.loadDepts();
        this.loadFonctions(); 
    }
});