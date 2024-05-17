<script setup>
    import { ref, defineExpose, watch } from 'vue'

    const defaultItem = { id: 0, name: '', description: '', quantity: 0, unitPrice: 0, totalPrice: 0, menuId: 0 };

    const loading = ref(false);
    const menuId = ref(null);
    const selected = ref(defaultItem);
    const items = ref(null);

    function resetDefault() {
        defaultItem.id = 0;
        defaultItem.name = '';
        defaultItem.description = '';
        defaultItem.quantity = 0;
        defaultItem.unitPrice = 0;
        defaultItem.totalPrice = 0;
        defaultItem.menuId = menuId.value;
    }

    async function fetchData() {
        if (selected.value) {
            resetDefault();
            loading.value = true;
            selected.value = defaultItem;
            var res = await fetch('item?menuId=' + menuId.value);
            items.value = await res.json();
            loading.value = false;
        }
        else
            items.value = null;
    }

    async function saveItem() {
        var res = await fetch('item', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(selected.value)
        });
        await fetchData();
    }

    async function deleteItem() {
        var res = await fetch('item/' + selected.value.id, {
            method: 'DELETE'
        });
        await fetchData();
    }

    watch(menuId, async () => {
        await fetchData();
    });

    defineExpose({ menuId });

</script>

<template>

    <h3>Items</h3>
    <!--<div v-if="loading">Loading...</div>-->
    <select v-elseif="items" class="form-select" v-model="selected" size="6">
        <option :value="defaultItem">(New)</option>
        <option v-for="item in items" :key="item.id" :value="item">{{ item.name }}</option>
    </select>

    <div v-if="selected" class="col">
        <h3>Details</h3>
        <div class="row">
            <div class="col-2"><label for="nameInput" class="form-label">Name:</label></div>
            <div class="col">
                <input class="form-control" id="nameInput" type="text" v-model="selected.name" />
            </div>
        </div>
        <div class="row">
            <div class="col-2"><label for="descArea" class="form-label">Desc:</label></div>
            <div class="col">
                <textarea class="form-control" id="descArea" v-model="selected.description"></textarea>
            </div>
        </div>

        <button class="btn btn-primary" @click="saveItem">Save</button>
        <button class="btn btn-danger" @click="deleteItem" :disabled="!selected.id">Delete</button>
    </div>
</template>


<style>
</style>