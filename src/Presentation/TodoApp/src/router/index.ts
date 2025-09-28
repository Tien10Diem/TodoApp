import AddContent from '@/Component/AddContent.vue'
import BinPage from '@/Pages/BinPage.vue'
import DetailJob from '@/Pages/DetailJob.vue'
import HomePage from '@/Pages/homePage.vue'
import registerPage from '@/Pages/registerPage.vue'
import { createRouter, createWebHistory } from 'vue-router'

const userid = localStorage.getItem('UserId');

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{
    path: '/login',
    name: 'login',
    component: registerPage,
    props: true
  },
  { path: '/', redirect: '/login' },
  { path: '/home', redirect: `/home/${userid}` },
  {
    path: '/register',
    name: 'register',
    component: registerPage,
  },
  {
    path:'/home/:userid',
    name: 'home',
    component: HomePage,
    props: true,
  },
  {
    path:"/detail/:id",
    name: 'detail',
    component: DetailJob,
    props: true
  },
  {
    path:"/add",
    name: "add",
    component:AddContent,
  },
  {
    path:"/bin/:userid",
    name:"bin",
    component: BinPage,
    props: true
  },
  { path: '/bin', redirect: `/bin/${userid}` },
  ]

})

export default router
