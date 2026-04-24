<template>
  <div>
    <h1 class="text-2xl font-bold mb-6">Pizzák</h1>

    <div v-if="store.pizzas.length === 0" class="text-gray-400 italic">
      Nincsenek elérhető pizzák.
    </div>

    <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
      <div
        v-for="pizza in store.pizzas"
        :key="pizza.id"
        class="bg-white border border-gray-100 rounded-lg overflow-hidden shadow-sm hover:shadow-md transition-shadow"
      >
        <img
          :src="pizza.imageUrl"
          :alt="pizza.name"
          class="w-full h-48 object-cover"
        />
        <div class="p-4 flex flex-col gap-3">
          <h2 class="text-lg font-semibold text-gray-800">{{ pizza.name }}</h2>
          <button
            @click="showDetails(pizza.id)"
            class="self-start text-sm border border-gray-300 rounded-md px-4 py-1.5 text-gray-600 hover:bg-gray-100 transition-colors cursor-pointer"
          >
            Részletek
          </button>
          <!-- Extra információk a gomb alatt, ha a felhasználó rákattintott -->
          <div v-if="expandedIds.includes(pizza.id)" class="text-sm text-gray-600 bg-gray-50 rounded-md p-3 -mt-1">
            <p><span class="font-medium">Kategória:</span> {{ pizza.category }}</p>
            <p class="mt-1"><span class="font-medium">Ár:</span> {{ pizza.priceHuf }} Ft</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { usePizzaStore } from '../stores/pizzaStore'

const store = usePizzaStore()

onMounted(() => {
  store.fetchPizzas()
})

// Számon tartjuk, mely pizzák részletei látszanak
const expandedIds = ref([])

function showDetails(id) {
  // Konzolra írás a specifikáció szerint
  console.log('Kiválasztott pizza azonosítója:', id)

  // Részletek megjelenítése/elrejtése
  const index = expandedIds.value.indexOf(id)
  if (index === -1) {
    expandedIds.value.push(id)
  } else {
    expandedIds.value.splice(index, 1)
  }
}
</script>