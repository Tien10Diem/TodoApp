import LoginPage from '@/Pages/registerPage.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{
    path: '/',
    name: 'login',
    component: LoginPage,
  }],
})

export default router
