<template>
  <nav class="border-b border-gray-200 bg-white">
    <div class="max-w-5xl mx-auto px-4 h-14 flex items-center gap-6">
      <span class="font-semibold tracking-wide text-gray-700">🍕 Pizzakatalógus</span>
      <router-link to="/" class="text-sm text-gray-500 hover:text-gray-900 transition-colors">Kezdőlap</router-link>
      <router-link to="/pizzas" class="text-sm text-gray-500 hover:text-gray-900 transition-colors">Pizzák</router-link>
    </div>
  </nav>
</template>