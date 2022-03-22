new Vue({
    el: '#app',
    data: {
        curr_tp: document.getElementById("curr_tp").innerText,
        state: "running",
        item: {
            agent_id: null,
            id: null,
            description: null, 
            agent: null,
            service: null,
            fonction: null,
            request_for:''
        },
        currentProfil: {agent_id:0, nom_complet:''},
         datas: [],
       
        tppos: [],
        currentPositions: [],
        tp_id: null, 
    },
    computed: {
        state_string: function () {
            return this.state === 'running' ? "En cours" :
                this.state === 'comming' ? "A venir" :
                    this.state === 'tovalidate_start' ? 'En attente de validation (Debut)' :
                        this.state === 'tovalidate_end' ? 'En attente de validation (Fin)' :
                            this.state === 'passed'? 'Terminée': '';
        }
    },
    watch: {
        state: function (newvalue, oldvalue) {
            this.loadData();
        }
    }
    , 
    methods: {
        loadTypepositions: function () {
            var me = this
            var url = $('#api-typeposs-url').attr('href')
            axios.get(url)
                .then(response => {
                    me.typespositions = response.data
                })
        }, 
        setCurrentTypePosition: function(item) {
            this.typeposition = item
            this.loadData();
        },
        showCurrentPositionList: function (item) {
            var me = this
            me.currentProfil.agent_id = item.agent_id
            me.currentProfil.nom_complet = item.agent
            var url = $('#api-situation-url').attr('href')
            loaderOpen()
            axios.get(url, {
                params: {
                    agent_id: me.currentProfil.agent_id,
                    state: "running"
                }
            })
                .then(response => {
                    me.currentPositions = response.data
                    Metro.dialog.open('#lastPositionList')
                    loaderClose()
                })

        },
        loadData: function (state) {

            var me = this;
            //if (state !== null && state !== undefined)
            //    me.state = state;
            $(".liste_item").removeClass("primary")
            loaderOpen()
            var url = $('#api-data-url').attr('href')
           
            axios.get(url, {
                params: {
                    type_id: me.curr_tp,
                    state: me.state
                }
            }).then(response => {
                //$("#liste_" + me.typeposition.id).addClass("primary")
                me.datas = response.data
                loaderClose()
                initFilter();
            })
        },
        validationForm: function (item) {
            this.item = item;
            Metro.dialog.open('#validationform')
        },

        validate: function () {
            loaderOpen()
            var url = $('#api-validation-url').attr('href')
            this.item.request_for = this.state
            
            axios.post(url, JSON.stringify(this.item), {
                headers: {
                    'Content-Type': 'application/json',
                    //'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).then(response => {
                console.log(response.data)
                Metro.toast.create(response.data, null, null, "success", { showTop: true })
                loaderClose()
                this.loadData(null);
            }).catch(function (error) {
                Metro.toast.create(error, null, null, "alert", { showTop: true })              
                loaderClose()
            })
        }
    },
    mounted: function () {
        
        this.loadData();
        $('#tp_' + this.curr_tp).removeClass("outline")
    }
});