<script setup>
    import { ref, watch, defineProps, defineEmits } from 'vue';

    const defaultMenu = { id: 0, title: '', totalPrice: 0, totalPaid: 0, personId: 0 };

    var that = this;
    const loading = ref(null);
    const personId = ref(null);
    const menus = ref(null);
    const selected = ref(defaultMenu);
    const props = defineProps(['menuId']);
    const menuId = ref(null);

    function resetDefault() {
        defaultMenu.id = 0;
        defaultMenu.title = '';
        defaultMenu.totalPaid = 0;
        defaultMenu.totalPrice = 0;
        defaultMenu.personId = personId.value;
    }

    async function fetchData() {
        if (personId.value) {
            resetDefault();
            loading.value = true;
            selected.value = defaultMenu;
            var res = await fetch('menu?personId=' + personId.value);
            menus.value = await res.json();
            loading.value = false;
        }
        else
            menus.value = null;
    }

    async function saveMenu() {
        var res = await fetch('menu', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(selected.value)
        });
        await fetchData();
    }

    async function deleteMenu() {
        var res = await fetch('menu/' + selected.value.id, {
            method: 'DELETE'
        });
        await fetchData();
    }


    watch(personId, async (newId, oldId) => {
        await fetchData();
    });

    watch(selected, async (val) => {
        menuId.value = val.id;
    });

    function onSelect(e) {
        return null;
    }

    defineExpose({ personId, menuId });
</script>

<template>
    <div class="row">
        <div class="col">
            <h3>Menus</h3>
            <!--<div v-if="loading">Loading...</div>-->
            <div v-if="menus">
                <select class="form-select" v-model="selected" @change="$emit('selected', selected.id)">
                    <option :value="defaultMenu">(New)</option>
                    <option v-for="menu in menus" :key="menu.id" :value="menu">{{ menu.title }}</option>
                </select>
            </div>

            <div v-if="selected">
                <input class="form-control" type="text" name="title" v-model="selected.title" />
                <button class="btn btn-primary" @click="saveMenu">Save</button>
                <button class="btn btn-danger" @click="deleteMenu" :disabled="!selected.id">Delete</button>

            </div>

        </div>
    </div>
</template>


<style>
</style>