<template>
  <nav class="navbar navbar-expand-md sticky-top cine-nav">
    <div class="container">
      <!-- Brand -->
      <router-link class="navbar-brand cine-brand" to="/">
        🎬 CineDB
      </router-link>

      <!-- Toggler -->
      <button
        class="navbar-toggler border-0"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#mainNav"
        aria-controls="mainNav"
        aria-expanded="false"
        aria-label="Navigáció megnyitása"
      >
        <i class="bi bi-list text-white fs-4"></i>
      </button>

      <!-- Links -->
      <div class="collapse navbar-collapse" id="mainNav">
        <ul class="navbar-nav ms-auto gap-1">
          <li class="nav-item">
            <router-link class="nav-link cine-link" to="/">
              <i class="bi bi-house-door me-1"></i>Kezdőlap
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link cine-link" to="/movies">
              <i class="bi bi-film me-1"></i>Filmek
            </router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script setup>
</script>

<style scoped>
.cine-nav {
  background-color: var(--nav-bg);
  border-bottom: 2px solid var(--accent);
}

.cine-brand {
  font-family: 'Oswald', sans-serif;
  font-size: 1.6rem;
  font-weight: 700;
  color: var(--accent) !important;
  letter-spacing: 1px;
  text-decoration: none;
}

.cine-link {
  font-family: 'Oswald', sans-serif;
  font-size: 1rem;
  color: #ccc !important;
  letter-spacing: .4px;
  padding: 6px 14px;
  border-radius: 4px;
  text-decoration: none;
  transition: color .2s, background .2s;
}

.cine-link:hover {
  color: #fff !important;
  background: rgba(255,255,255,.06);
}

/* vue-router active classes */
.cine-link.router-link-active,
.cine-link.router-link-exact-active {
  color: var(--accent) !important;
  border-bottom: 2px solid var(--accent);
}
</style>
