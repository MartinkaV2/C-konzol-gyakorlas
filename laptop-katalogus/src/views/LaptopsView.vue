<template>
  <div>
    <h1 class="text-2xl font-bold mb-6">Laptopok</h1>

    <div v-if="store.laptops.length === 0" class="text-gray-400 italic">
      Nincsenek elérhető laptopok.
    </div>

    <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
      <div
        v-for="laptop in store.laptops"
        :key="laptop.id"
        class="bg-white border border-gray-100 rounded-lg overflow-hidden shadow-sm hover:shadow-md transition-shadow"
      >
        <img
          :src="laptop.imageUrl"
          :alt="laptop.brand + ' ' + laptop.model"
          class="w-full h-48 object-cover"
        />
        <div class="p-4 flex flex-col gap-3">
          <h2 class="text-lg font-semibold text-gray-800">{{ laptop.brand }}</h2>
          <button
            @click="showDetails(laptop.id)"
            class="self-start text-sm border border-gray-300 rounded-md px-4 py-1.5 text-gray-600 hover:bg-gray-100 transition-colors cursor-pointer"
          >
            Részletek
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue'
import { useLaptopStore } from '../stores/laptopStore'

const store = useLaptopStore()

onMounted(() => {
  store.fetchLaptops()
})

function showDetails(id) {
  console.log('Kiválasztott laptop azonosítója:', id)
}
</script>