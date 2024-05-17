<script setup>
    import { ref, watch } from 'vue';

    const defaultMenu = { id: 0, title: '', totalPrice: 0, totalPaid: 0, personId: 0 };
    const defaultItem = { id: 0, name: '', description: '', quantity: 0, unitPrice: 0, totalPrice: 0, menuId: 0 };

    const loading = ref(false);
    const loadingItems = ref(null);
    const personId = ref(null);
    const menus = ref(null);
    const selected = ref(defaultMenu);
    const selectedItem = ref(defaultItem);
    const items = ref(null);

    function resetDefault() {
        defaultMenu.id = 0;
        defaultMenu.title = '';
        defaultMenu.totalPaid = 0;
        defaultMenu.totalPrice = 0;
        defaultMenu.personId = personId.value;
    }

    function resetItem() {
        defaultItem.id = 0;
        defaultItem.name = '';
        defaultItem.description = '';
        defaultItem.quantity = 0;
        defaultItem.unitPrice = 0;
        defaultItem.totalPrice = 0;
        defaultItem.menuId = selected.value ? selected.value.id : 0;
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

    async function fetchItems() {
        if (selected.value) {
            resetItem();
            loadingItems.value = true;
            selectedItem.value = defaultItem;
            var res = await fetch('item?menuId=' + selected.value.id);
            items.value = await res.json();
            loadingItems.value = false;
        }
        else
            items.value = null;
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

    async function saveItem() {
        var res = await fetch('item', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(selectedItem.value)
        });
        await fetchItems();
    }

    async function deleteItem() {
        var res = await fetch('item/' + selectedItem.value.id, {
            method: 'DELETE'
        });
        await fetchItems();
    }

    watch(personId, async (newId, oldId) => {
        await fetchData();
    });

    watch(selected, async () => {
        await fetchItems();
    });

    defineExpose({
        personId
    });
</script>

<template>
    <div class="row">
        <div class="col">
            <h3>Menus</h3>
            <div v-if="loading">Loading...</div>
            <div v-else>
                <select v-model="selected">
                    <option :value="defaultMenu">(New)</option>
                    <option v-for="menu in menus" :key="menu.id" :value="menu">{{ menu.title }}</option>
                </select>
            </div>

            <div v-if="selected">
                <input type="text" name="title" v-model="selected.title" />
                <button @click="saveMenu">Save</button>
                <button @click="deleteMenu" :disabled="!selected.id">Delete</button>

                <h3>Items</h3>
                <select v-model="selectedItem" size="8">
                    <option :value="defaultItem">(New)</option>
                    <option v-for="item in items" :key="item.id" :value="item">{{ item.name }}</option>
                </select>
            </div>

        </div>
        <div v-if="selectedItem" class="col">
            <h3>Details</h3>
            <div class="row">
                <div class="col-2">Name:</div>
                <div class="col">
                    <input type="text" v-model="selectedItem.name" />
                </div>
            </div>
            <div class="row">
                <div class="col-2">Desc:</div>
                <div class="col">
                    <textarea type="text" v-model="selectedItem.description"></textarea>
                </div>
            </div>

            <button @click="saveItem">Save</button>
            <button @click="deleteItem" :disabled="!selectedItem.id">Delete</button>
        </div>
    </div>
</template>


<style>
</style>