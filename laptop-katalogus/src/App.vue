<template>
  <NavBar />
  <main class="max-w-5xl mx-auto px-4 py-8">
    <router-view />
  </main>
</template>

<script setup>
import NavBar from './components/NavBar.vue'
</script>