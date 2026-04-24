<template>
  <div class="card">
    <div class="poster-wrap">
      <img :src="movie.posterUrl" :alt="movie.title" class="poster" />
    </div>
    <div class="meta">
      <h3 class="title">{{ movie.title }}</h3>
      <button class="details" @click="$emit('details', movie.id)">Részletek</button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MovieCard',
  props: {
    movie: { type: Object, required: true }
  }
}
</script>
