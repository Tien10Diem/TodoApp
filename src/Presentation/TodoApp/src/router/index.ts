import HomePage from '@/Pages/homePage.vue'
import registerPage from '@/Pages/registerPage.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{
    path: '/login',
    name: 'login',
    component: registerPage,
  },
  { path: '/', redirect: '/login' },
  {
    path: '/register',
    name: 'register',
    component: registerPage,
  },
  {
    path:'/home',
    name: 'home',
    component: HomePage
  }
  ]

})

export default router
