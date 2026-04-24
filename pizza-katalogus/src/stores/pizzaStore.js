import { defineStore } from 'pinia'
import { ref } from 'vue'

export const usePizzaStore = defineStore('pizzas', () => {
  const pizzas = ref([])

  async function fetchPizzas() {
    try {
      const res = await fetch('http://localhost:3000/pizzas')
      const data = await res.json()
      pizzas.value = data
    } catch (err) {
      console.error('Hiba a pizzák betöltésekor:', err)
    }
  }

  return { pizzas, fetchPizzas }
})