import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useRunningShoesStore = defineStore('runningShoes', () => {
  const runningShoes = ref([])

  async function fetchRunningShoes() {
    const response = await fetch('http://localhost:3000/runningShoes')
    runningShoes.value = await response.json()
  }

  return { runningShoes, fetchRunningShoes }
})
