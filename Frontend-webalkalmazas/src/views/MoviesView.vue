<template>
  <!-- Page header -->
  <div class="page-header">
    <div class="container">
      <h2 class="font-oswald page-title">
        <i class="bi bi-film me-2 text-accent"></i>Filmek
      </h2>
      <p class="text-secondary small mb-0">
        Adatok forrása:
        <code class="text-light">GET http://localhost:3000/movies</code>
      </p>
    </div>
  </div>

  <div class="container py-5">

    <!-- Loading -->
    <div v-if="loading" class="text-center py-5">
      <div class="spinner-border cine-spinner" role="status">
        <span class="visually-hidden">Betöltés...</span>
      </div>
      <p class="mt-3 text-secondary">Filmek betöltése...</p>
    </div>

    <!-- Error -->
    <div v-else-if="error" class="error-card p-4">
      <h5 class="font-oswald">
        <i class="bi bi-exclamation-triangle-fill me-2 text-accent"></i>
        Nem sikerült csatlakozni az API-hoz
      </h5>
      <p class="text-secondary small mb-1">
        Ellenőrizze, hogy a json-server fut-e:
      </p>
      <code class="d-block mb-3 text-light bg-black px-3 py-2 rounded small">
        npx json-server --watch data.json
      </code>
      <button class="btn btn-accent" @click="fetchMovies">
        <i class="bi bi-arrow-clockwise me-1"></i>Újrapróbálás
      </button>
    </div>

    <!-- Movie grid -->
    <div v-else class="row row-cols-2 row-cols-sm-3 row-cols-md-4 g-4">
      <div class="col" v-for="movie in movies" :key="movie.id">
        <div class="movie-card">
          <div class="poster-wrap">
            <img
              :src="movie.poster"
              :alt="movie.title"
              @error="onImgError"
            />
          </div>
          <div class="movie-body">
            <p class="movie-title" :title="movie.title">{{ movie.title }}</p>
            <button
              class="btn btn-accent w-100 btn-sm"
              @click="showDetails(movie.id)"
            >
              <i class="bi bi-info-circle me-1"></i>Részletek
            </button>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const movies  = ref([])
const loading = ref(false)
const error   = ref(false)

async function fetchMovies() {
  loading.value = true
  error.value   = false

  try {
    const res = await fetch('/api/movies')
    if (!res.ok) throw new Error(`HTTP ${res.status}`)
    movies.value = await res.json()
  } catch (err) {
    console.error('API hiba:', err)
    error.value = true
  } finally {
    loading.value = false
  }
}

function showDetails(id) {
  console.log('Kiválasztott film azonosítója:', id)
}

function onImgError(e) {
  e.target.src = 'https://placehold.co/300x450/1a1a1a/555?text=Nincs+kép'
}

onMounted(() => fetchMovies())
</script>

<style scoped>
.page-header {
  background: linear-gradient(90deg, #0d0d0d, #1a0000);
  padding: 36px 0 28px;
  border-bottom: 1px solid #2a2a2a;
}

.page-title {
  font-size: 2.2rem;
  font-weight: 600;
  letter-spacing: 1px;
}

.text-accent { color: var(--accent); }

/* Spinner */
.cine-spinner {
  width: 3rem;
  height: 3rem;
  color: var(--accent);
}

/* Error */
.error-card {
  background: var(--card-bg);
  border: 1px solid var(--accent);
  border-radius: 10px;
}

/* Movie card */
.movie-card {
  background: var(--card-bg);
  border: 1px solid #2a2a2a;
  border-radius: 10px;
  overflow: hidden;
  height: 100%;
  transition: transform .25s, border-color .25s, box-shadow .25s;
}

.movie-card:hover {
  transform: translateY(-6px);
  border-color: var(--accent);
  box-shadow: 0 12px 30px rgba(229, 9, 20, .25);
}

.poster-wrap {
  aspect-ratio: 2 / 3;
  overflow: hidden;
  background: #111;
}

.poster-wrap img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform .4s;
}

.movie-card:hover .poster-wrap img {
  transform: scale(1.06);
}

.movie-body {
  padding: 12px 14px;
}

.movie-title {
  font-family: 'Oswald', sans-serif;
  font-size: .95rem;
  font-weight: 600;
  color: #fff;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  margin-bottom: 10px;
}
</style>
