
Vue.prototype.$http = axios;
new Vue({
    el: '#app',
    data: {
        current_type_pes: null,
        current_dept: null,
        search: '',
        dept: 0,       
        selectedDepts: [],
        selectedtypePes: [],
        depts: [],
        type_pes: [],
        beneficiaireSelectionList: [],
        selectedBeneficiaires: [],
        datas: [],
        isAllSelected: false,
        filteringObject: {
            filteringtext: '',
            filteredData:[]
        }
    },
    watch: {
        current_type_pes: function (val) {
            this.setCurrentTypePes (val)
        },
        current_dept: function (val) {
            this.setCurrentDept(val)
        }
    },
    computed: {

    },
    methods: {
        // type_pes
        removeTypePesForm: function () {
            if (this.current_type_pes === null) return
            var me = this
            Metro.dialog.create({
                title: "Suppression type de prise en charge",
                content: "<div class='text-center'>Supprimer le type de prise en charge <strong> " + me.current_type_pes.designation + "</strong> de l'engagement ?</div>",
                actions: [
                    {
                        caption: "Supprimer",
                        cls: "js-dialog-close alert",
                        onclick: function () {
                            var url = $('#api-typepes-url').attr('href')
                            var project_id = $('#project_id').val()
                            axios.delete(url, {
                                params: {
                                    project_id: project_id,
                                    id: me.current_type_pes.id
                                }
                            }).then(response => {
                                console.log(response.data)
                                me.loadTypePes();
                                me.loadData();
                            })
                        }
                    },
                    {
                        caption: "Annuler",
                        cls: "js-dialog-close",
                    }
                ]
            });

        },
        addTypePesForm: function () {
            Metro.dialog.open('#addtp')
        },
        submitAddTypePes: function (element) {
            var url = $('#api-typepes-url').attr('href')
            var project_id = $('#project_id').val()
            var me = this
            let data = {
                projet_engagement_id: project_id,
                id: element.id
            }
            loaderOpen()
            axios.post(url, JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json',
                    'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).then(response => {
                loaderClose()
                me.loadTypePes();
            })

        },

        setCurrentTypePes: function (item) {
            this.current_type_pes = item
            this.loadData();
        },



        // detp
        setCurrentDept: function (item) {
            this.current_dept = item;
            this.loadData();
        },
        submitAddDept: function (element) {
            var url = $('#api-departement-url').attr('href')
            var project_id = $('#project_id').val()
            var me = this
            let data = {
                projet_engagement_id: project_id,
                id: element.id
            }
            loaderOpen()
            axios.post(url, JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json',
                    'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).then(response => {
                loaderClose()
                var index = me.depts.findIndex(item => item === element)
                me.depts.splice(index, 1);
                initFilter();
                me.loadSelectedDepts();
            })

        },

        removeDeptForm: function (item) {
            if (this.setCurrentDept === null) return;
            var me = this
            Metro.dialog.create({
                title: "Suppression type de prise en charge",
                content: "<div class='text-center'>Supprimer le departement <strong> " + me.current_dept.designation + "</strong> de l'engagement ?</div>",
                actions: [
                    {
                        caption: "Supprimer",
                        cls: "js-dialog-close alert",
                        onclick: function () {
                            var url = $('#api-departement-url').attr('href')
                            var project_id = $('#project_id').val()
                            axios.delete(url, {
                                params: {
                                    project_id: project_id,
                                    id: me.current_dept.id
                                }
                            }).then(response => {
                                console.log(response.data)
                                me.loadDepts();
                                me.loadData();
                            })
                        }
                    },
                    {
                        caption: "Annuler",
                        cls: "js-dialog-close",
                    }
                ]
            });
        },
        addDeptForm: function () {
            Metro.dialog.open('#adddep')
        },

        // engagement
        removeEngagementForm: function (item) {
            var me = this
            Metro.dialog.create({
                title: "Suppression",
                content: "<div>Supprimer le beneficiaire " + item.beneficiaire.nom_complet + " de l'engagement ?</div>",
                actions: [
                    {
                        caption: "Supprimer",
                        cls: "js-dialog-close alert",
                        onclick: function () {
                            var url = $('#api-engagement-url').attr('href')
                            var project_id = $('#project_id').val()
                            var id = project_id + '_' + item.beneficiaire_id + '_' + item.type_prise_en_charge_id
                            axios.delete(url+'/'+id).then(response => {
                                me.loadData();
                            })  
                        }
                    },
                    {
                        caption: "Annuler",
                        cls: "js-dialog-close",
                    }
                ]
            });
        },


        isSelected: function (benef_id) {
            return this.selectedBeneficiaires.includes(benef_id);
        },

        selectBeneficaire: function (benef_id) {
            var me = this;
            if (!this.isSelected(benef_id))
                this.selectedBeneficiaires.push(benef_id);
            else {
                var index = this.selectedBeneficiaires.findIndex(item => item === benef_id)
                this.selectedBeneficiaires.splice(index, 1);
            }
            this.isAllSelected = false 
        },

        selectAllBeneficiaire: function () {
            this.selectedBeneficiaires.splice(0, this.selectedBeneficiaires.length)
            var me = this;
            if (!this.isAllSelected)
                this.beneficiaireSelectionList.forEach(item => me.selectBeneficaire(item.beneficiaire_id))
            this.isAllSelected = !this.isAllSelected;
        },

        cancelSelection: function () {
            this.selectedBeneficiaires = [];
        },
        submitSelectedBeneficiaires: function () {

            if (this.selectedBeneficiaires.length == 0 || this.current_type_pes === null) return
            
            var selstring = ""
            this.selectedBeneficiaires.forEach(item => selstring += "-" + item);
            
            var project_id = $('#project_id').val()
            var data = { id: project_id, tpid: this.current_type_pes.id, benids: selstring }
            loaderOpen()
            var url = $('#api-engagement-url').attr('href')
            console.log(JSON.stringify(data))
    
            axios.post(url, JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json',
                    'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            })
                .then(response => {
                    loaderClose()
                    initFilter();
                    this.loadData()
                })
        }, 

        beneficiaireSelectionForm: function () {
            this.selectedBeneficiaires = [];
           this.isAllSelected = false;
           this.loadselectionList();
            Metro.dialog.open('#addben')
        },
        loadData: function () {

            var me = this;
            if (me.current_dept === null) return;
            if (me.current_type_pes === null) return;
            var project_id = $('#project_id').val()
            loaderOpen()
            var url = $('#api-engagement-url').attr('href')
            axios.get(url, {
                params: {
                    projet_id: project_id,
                    dept_id: me.current_dept.id,
                    type_pes_id: me.current_type_pes.id
                }
            }) .then(response => {
                    me.datas = response.data
                    me.filteringObject.filteredData = response.data
                    loaderClose()
                    initFilter();
                })
        },
        filterDataFn: function () {
            var filtertext = this.filteringObject.filteringtext;
            this.datas = this.filteringObject.filteredData.filter(function (el) {
                return el.beneficiaire.nom_complet.toString().toLowerCase().includes(
                    filtertext.toString().toLowerCase())
             })

        },

        loadselectionList: function () {
            
            var me = this;
            if (me.current_dept === null || me.current_type_pes === null) return;
            var search = me.search 
            loaderOpen()
            var url = $('#api-eligibilite-url').attr('href') + '?tp=' + me.current_type_pes.id + '&dept=' + me.current_dept.id + '&search=' + search
            axios.get(url)
                .then(response => {
                    me.beneficiaireSelectionList = response.data
                    loaderClose()
                    initFilter();
                })
        },

        loadSelectedDepts: function () {
            var me = this
            var url = $('#api-departement-url').attr('href')
            var project_id = $('#project_id').val()
            axios.get(url, {
                params: {
                    projet_id: project_id
                }
            }).then(response => {
                me.selectedDepts = response.data
            })
        },

        loadDepts: function () {
            var me = this
            var url = $('#api-departement-url').attr('href')
            var me = this

            var project_id = $('#project_id').val()
            Promise.all([axios.get(url), axios.get(url, { params: { projet_id: project_id } })])
                .then(function (results) {

                    me.depts = results[0].data;
                    me.selectedDepts = results[1].data;
                });
        },
        loadTypePes: function () {
            var me = this
            var url = $('#api-typepes-url').attr('href')
            var project_id = $('#project_id').val()
            Promise.all([axios.get(url), axios.get(url, { params: { projet_id: project_id } })])
                .then(function (results) {
                    me.type_pes = results[0].data;
                    me.selectedtypePes = results[1].data;
                });
        },

     
    },
    mounted: function () {
        this.loadTypePes()
        this.loadDepts()
    }
});