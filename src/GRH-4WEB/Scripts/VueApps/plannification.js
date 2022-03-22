new Vue({
    el: '#app',
    data: {
        planning_item: {
            agent_id: null,
            id: null,
            description: null,
            agent: null,
            service: null,
            fonction: null,
            request_for: ''
        },
        state: 'running',
        planning: {},
        typeposition: null,
        departement: null,
        datas: [],
        types: [],
        depts: [],
        error_message: ''

    },
    watch: {
        state: function (oldvalue, newvalue) {
            this.loadData()
        }
    },
    methods: {
        loadTypepositions: function () {
            var me = this
            var url = $('#api-typpos-url').attr('href') + '?require_planning=true'
            axios.get(url)
                .then(response => {
                    me.types = response.data
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
        },
        loadData: function () {
            var me = this;
            loaderOpen()
            var url = $('#api-planning-url').attr('href')
            axios.get(url, {
                params: {
                    type_position_id: me.typeposition ? me.typeposition.id : null,
                    str_id: me.departement ? me.departement.id : null,
                    state: me.state
                }
            }).then(response => {
                me.datas = response.data
                loaderClose()
            })
        },
        editPlanning: function (planning) {
            var me = this
            //if ( me.departement === null || me.departement === undefined) return
            if (planning !== null && planning !== undefined)
                me.planning = planning
            me.error_message = ""
            Metro.dialog.open('#editplanning')
        },
        savePlanning: function () {
            var me = this
            me.planning.planning_validity_periode = me.typeposition.planning_validity_periode;
            me.planning.type_position_id = me.typeposition.id
            me.planning.structure_id = me.departement.id
            me.planning.started_at = $('#started_at').val()
            me.planning.ended_at = $('#ended_at').val()
            console.log(me.planning)
            if (me.planning.planning_validity_periode === 'custom'
                && (me.planning.started_at === '' || me.planning.started_at === undefined || me.planning.ended_at === '' || me.planning.ended_at === undefined)) {
                me.error_message = "Veillez renseigner une période de validité correcte"
                return
            }
            Metro.dialog.close('#editplanning')

        },
        validationForm: function (item) {
            this.item = item;
            Metro.dialog.open('#validationform')
        },

        validate: function () {
            loaderOpen()
            var url = $('#api-validation-url').attr('href')
            this.item.request_for = this.state
            console.log(th)
            axios.post(url, JSON.stringify(this.item), {
                headers: {
                    'Content-Type': 'application/json',
                    'XSRF-TOKEN': $('input:hidden[name="__RequestVerificationToken"]').val()
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
        this.loadDepts()
        this.loadTypepositions()
        this.loadData()
    }
});