import { createRouter, createWebHistory } from 'vue-router'
import HomeView   from '../views/HomeView.vue'
import MoviesView from '../views/MoviesView.vue'

const routes = [
  { path: '/',       name: 'home',   component: HomeView   },
  { path: '/movies', name: 'movies', component: MoviesView },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
