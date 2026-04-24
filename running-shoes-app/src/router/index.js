import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import RunningShoesView from '../views/RunningShoesView.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: HomeView
    },
    {
      path: '/running-shoes',
      component: RunningShoesView
    }
  ]
})

export default router
