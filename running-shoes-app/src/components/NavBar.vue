<template>
  <nav class="navbar navbar-expand navbar-dark bg-dark px-3">
    <span class="navbar-brand">Futócipő katalógus</span>
    <div class="navbar-nav">
      <RouterLink class="nav-link" to="/">Kezdőlap</RouterLink>
      <RouterLink class="nav-link" to="/running-shoes">Futócipők</RouterLink>
    </div>
  </nav>
</template>

<script setup>
import { RouterLink } from 'vue-router'
</script>
