<script setup>
    import { onMounted, ref, watch } from 'vue';
    import MenuManager from './MenuManager.vue';
    import ItemManager from './ItemManager.vue';

    const defaultPerson = { id: 0, firstName: '', lastName: '' };

    const loading = ref(false);
    const persons = ref(null);
    const selected = ref(defaultPerson);
    const menuManager = ref(null);
    const itemManager = ref(null);

    function resetPerson() {
        defaultPerson.id = 0;
        defaultPerson.firstName = '';
        defaultPerson.lastName = '';
    }

    async function fetchData() {
        resetPerson();
        selected.value = defaultPerson;
        persons.value = null;
        loading.value = true;

        var res = await fetch('person');
        persons.value = await res.json();
        loading.value = false;
    }

    async function savePerson() {
        var res = await fetch('person', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(selected.value)
        });
        await fetchData();
    }

    async function deletePerson() {
        var res = await fetch('person/' + selected.value.id, {
            method: 'DELETE'
        });
        await fetchData();
    }

    watch(selected, (val) => {
        menuManager.value.personId = val.id;
    });

    function menuSelected(val) {
        itemManager.value.menuId = val;
    }

    onMounted(async () => {
        await fetchData();
    });
</script>

<template>
    <div class="row">
        <div class="col">

            <h2>Person Manager</h2>

            <!--<div v-if="loading">Loading...</div>-->
            <div v-if="persons">
                <select class="form-select" v-model="selected">
                    <option :value="defaultPerson">(New)</option>
                    <option v-for="person in persons" :key="person.id" :value="person">{{ person.firstName }} {{ person.lastName }}</option>
                </select>

                <div v-if="selected">
                    <div class="row">
                        <div class="col-2"><label for="firstNameInput" class="form-label">First Name:</label></div>
                        <div class="col">
                            <input class="form-control" id="firstNameInput" type="text" v-model="selected.firstName" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2"><label for="lastNameInput" class="form-label">Last Name:</label></div>
                        <div class="col">
                            <input class="form-control" id="lastNameInput" type="text" v-model="selected.lastName" />
                        </div>
                    </div>
                    <button class="btn btn-primary" @click="savePerson">Save</button>
                    <button class="btn btn-danger" @click="deletePerson" :disabled="!selected.id">Delete</button>
                </div>
            </div>
            <MenuManager ref="menuManager" v-on:selected="menuSelected" />
        </div>
        <div class="col">
            <ItemManager ref="itemManager" />
        </div>
    </div>
</template>


<style>

</style>