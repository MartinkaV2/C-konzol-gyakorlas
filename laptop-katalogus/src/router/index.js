import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LaptopsView from '../views/LaptopsView.vue'

const routes = [
  { path: '/', name: 'home', component: HomeView },
  { path: '/laptops', name: 'laptops', component: LaptopsView },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router