import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import PizzasView from '../views/PizzasView.vue'

const routes = [
  { path: '/', name: 'home', component: HomeView },
  { path: '/pizzas', name: 'pizzas', component: PizzasView },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router