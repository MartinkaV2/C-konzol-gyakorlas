<template>
  <div class="container">
    <h1>Filmek</h1>
    <div class="grid">
      <MovieCard
        v-for="movie in movies"
        :key="movie.id"
        :movie="movie"
        @details="showDetails"
      />
    </div>

    <div v-if="loading" class="loading">Betöltés…</div>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
import MovieCard from './components/MovieCard.vue'

export default {
  components: { MovieCard },
  setup() {
    const movies = ref([])
    const loading = ref(false)
    const error = ref('')

    // Alapértelmezett REST endpoint: http://localhost:3000/movies
    const API = 'http://localhost:3000/movies'

    async function load() {
      loading.value = true
      error.value = ''
      try {
        const res = await fetch(API)
        if (!res.ok) throw new Error('Hiba a szerverről')
        const data = await res.json()
        // Elvárt: tömb, ahol minden elem legalább: { id, title, posterUrl }
        movies.value = data
      } catch (e) {
        error.value = 'Nem sikerült betölteni a filmeket.'
        console.error(e)
      } finally {
        loading.value = false
      }
    }

    function showDetails(id) {
      // Példa: konzolra írjuk az id-t, vagy navigálás/alert
      console.log('Részletek id:', id)
      alert('Részletek (id): ' + id)
    }

    onMounted(load)

    return { movies, loading, error, showDetails }
  }
}
</script>
