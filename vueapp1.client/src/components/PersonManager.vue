<script setup>
    import { onMounted, ref } from 'vue';
    import MenuManager from './MenuManager.vue';

    var loading = ref(false);
    var persons = ref(null);
    var selected = ref(null);
    var menu = ref(null);

    async function fetchData() {
        selected.value = null;
        persons.value = null;
        loading.value = true;

        var res = await fetch('person');
        persons.value = await res.json();
        loading.value = false;
    }

    function selectPerson(person) {
        selected.value = person;
        var val = menu.value;
        val.personId = person.id;
    }

    onMounted(async () => {
        await fetchData();
    });
</script>

<template>
    <h2>Person Manager</h2>

    <div v-if="loading">Loading...</div>
    <div v-if="persons">
        <div v-for="person in persons" @click="selectPerson(person)" :class="['row',{ selected: selected === person }]" :key="person.id">
            <div class="col">{{ person.firstName }}</div>
            <div class="col">{{ person.lastName }}</div>
        </div>
    </div>
    <MenuManager ref="menu" />
</template>


<style scoped>

    .selected {
        background-color: #FFFF20;
    }

</style>