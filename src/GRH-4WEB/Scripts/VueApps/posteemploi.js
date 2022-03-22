new Vue({
    el: '#app',
    data: {
        poste: {
            fonction: '',
            id: 0, 
            structure_id: null,
            grade_fonction: '', 
            is_politic: false, 
        },
        state: 'occupied',
        departement: null,
        datas: [],
        depts: [],
        grades: []
    },
    computed: {
        fullPostename: function () {           
            return this.poste.fonction;
        }
    },
    methods: {

        changeDepartement: function (dept) {
            this.departement = dept
            $(".liste_item").removeClass("bg-darkCyan fg-lightGray")
            this.loadDatas(this.state)
            $("#liste_" + this.departement.id).removeClass("bg-lightGray")
            $("#liste_" + this.departement.id).addClass("bg-darkCyan fg-lightGray")
        },
        loadDepts: function () {
            var me = this
            var url = $('#api-structure-url').attr('href')
            console.log(url)
            axios.get(url)
                .then(response => {
                    me.depts = response.data
                    loaderClose()
                    initFilter();
                })
        },
        loadGrades: function () {
            var me = this

            var url = $('#api-grade-url').attr('href')
            axios.get(url)
                .then(response => {
                    me.grades = response.data
                    loaderClose()
                })
        },
        loadDatas: function (state) {
            var search = null;
            var me = this;
            if (!me.departement) return;
            if (state !== undefined)
                me.state = state

            loaderOpen()
            var url = $('#api-posteemploi-url').attr('href')
             url = url + me.departement.id
            axios.get(url, {
                params: {
                    state: me.state,
                    search: search
                }
            })
                .then(response => {
                    me.datas = response.data
                    loaderClose()
                    initFilter();
                })
        },
        save: function () {

            var me = this;
            var url = $('#api-posteemploi-url').attr('href')
            loaderOpen()
            axios.post(url, JSON.stringify(this.poste), {
                headers: {
                    'Content-Type': 'application/json',
                    'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).then(response => { 
                Metro.toast.create("Opération éffectuée avec succès", null, null, "success", { showTop: true })
                loaderClose()
                me.loadDepts();
                me.changeDepartement(response.data)
            }).catch(function (error) {
                Metro.toast.create(error, null, null, "alert", { showTop: true })
                loaderClose()
            })
        },

        deletePoste: function () {
            var me = this;
            var url = $('#api-posteemploi-url').attr('href')
            loaderOpen()
            axios.delete(url + me.poste.id).then(response => {
                loaderClose()

                var url1 = $('#api-structure-url').attr('href') + "/" + me.departement.id
                axios.get(url1)
                    .then(response => {
                        me.changeDepartement(response.data)
                    })

            }).catch(function (error) {
                Metro.toast.create(error, null, null, "alert", { showTop: true })
                loaderClose()
            })
        },
        voirProfilAgent: function (id) {
            var url = $('#profil-url').attr('href')
            var win = window.open(url.replace('0', id), '_blank')
            win.focus()
        },
        deletePosteForm: function (poste) {
            this.poste = poste;
            Metro.dialog.open('#deleteposteform')
        },
        editPoste: function (poste) {
            if(this.departement === null) return
            this.poste = (poste !== null && poste !== undefined ? poste : {
                fonction: '',
                id: 0,
                structure_id: this.departement.id,
                grade_fonction: '',
                is_politic: false});
            Metro.dialog.open('#newposteform')
        }, 
    },
    mounted: function () {
        this.loadDepts();
        this.loadGrades();
    }
});