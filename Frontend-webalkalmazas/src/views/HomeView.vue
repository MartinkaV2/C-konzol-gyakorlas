<template>
  <!-- Hero -->
  <section class="hero">
    <div class="container">
      <h1 class="hero-title font-oswald">
        Üdvözöljük a <span class="text-accent">CineDB</span>-n
      </h1>
      <div class="accent-divider"></div>
      <p class="hero-lead">
        Ez a weblap néhány ajánlott filmről szolgáltat információkat.
        A „Filmek" menüpontban a kiválasztott filmről a „Részletek" gombra
        történő kattintást követően juthat további információkhoz.
      </p>
      <router-link to="/movies" class="btn btn-accent px-4 py-2 mt-3">
        <i class="bi bi-collection-play me-2"></i>Filmek böngészése
      </router-link>
    </div>
  </section>

  <!-- Info cards -->
  <section class="py-5">
    <div class="container">
      <div class="row g-4">
        <div class="col-md-4" v-for="card in infoCards" :key="card.icon">
          <div class="info-card">
            <i :class="['bi', card.icon, 'info-icon']"></i>
            <h5 class="font-oswald fw-bold mb-2">{{ card.title }}</h5>
            <p class="text-secondary mb-0 small">{{ card.text }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
const infoCards = [
  {
    icon: 'bi-camera-reels',
    title: 'Filmek adatbázisa',
    text: 'Böngésszen filmjeink között és fedezze fel a legjobb alkotásokat.',
  },
  {
    icon: 'bi-star-half',
    title: 'Ajánlott filmek',
    text: 'Gondosan válogatott, időtálló alkotások a világ legjobb rendezőitől.',
  },
  {
    icon: 'bi-info-circle',
    title: 'Részletes infók',
    text: 'Minden filmhez részletes adatlap érhető el a Részletek gombra kattintva.',
  },
]
</script>

<style scoped>
.hero {
  background: linear-gradient(135deg, #0d0d0d 60%, #1a0000 100%);
  border-bottom: 1px solid #2a2a2a;
  padding: 80px 0 60px;
}

.hero-title {
  font-size: 3.2rem;
  font-weight: 700;
  letter-spacing: 2px;
  color: #fff;
}

.text-accent { color: var(--accent); }

.hero-lead {
  font-size: 1.05rem;
  color: #aaa;
  max-width: 580px;
}

.info-card {
  background: var(--card-bg);
  border: 1px solid #2a2a2a;
  border-radius: 10px;
  padding: 28px;
  height: 100%;
  transition: border-color .3s;
}

.info-card:hover {
  border-color: var(--accent);
}

.info-icon {
  font-size: 2rem;
  color: var(--accent);
  display: block;
  margin-bottom: 12px;
}
</style>
