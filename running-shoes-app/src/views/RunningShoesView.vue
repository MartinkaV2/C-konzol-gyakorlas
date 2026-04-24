<template>
  <div class="container mt-4">
    <h1>Futócipők</h1>
    <div class="row">
      <div
        class="col-md-4 mb-3"
        v-for="shoe in store.runningShoes"
        :key="shoe.id"
      >
        <div class="card">
          <img :src="shoe.imageUrl" class="card-img-top" :alt="shoe.brand" />
          <div class="card-body">
            <h5 class="card-title">{{ shoe.brand }}</h5>
            <button class="btn btn-primary" @click="showDetails(shoe.id)">
              Részletek
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue'
import { useRunningShoesStore } from '../stores/runningShoesStore'

const store = useRunningShoesStore()

onMounted(() => {
  store.fetchRunningShoes()
})

function showDetails(id) {
  console.log(id)
}
</script>
