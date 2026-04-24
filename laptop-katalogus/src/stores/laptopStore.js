import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useLaptopStore = defineStore('laptops', () => {
  const laptops = ref([])

  async function fetchLaptops() {
    try {
      const res = await fetch('http://localhost:3000/laptops')
      const data = await res.json()
      laptops.value = data
    } catch (err) {
      console.error('Hiba a laptopok betöltésekor:', err)
    }
  }

  return { laptops, fetchLaptops }
})